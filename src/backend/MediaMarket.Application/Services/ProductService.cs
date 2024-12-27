using AutoMapper;
using MediaMarket.Application.Bases;
using MediaMarket.Application.Contracts.Common;
using MediaMarket.Application.Contracts.Repositories;
using MediaMarket.Application.Contracts.Services;
using MediaMarket.Application.DTO.Product;
using MediaMarket.Application.DTO.Request.Product;
using MediaMarket.Application.DTO.Response.Product;
using MediaMarket.Domain.Entities;
using MediaMarket.Domain.Enums;
using MediaMarket.Infrastructure.Prompts;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace MediaMarket.Application.Services
{
    public class ProductService(
        IProductRepository productRepository,
        ICategoryRepository categoryRepository,
        ITagRepository tagRepository,
        IProductDetailRepository productDetailRepository,
        IPreviewRepository previewRepository,
        IGenerativeAIService generativeAIService,
        IImageRepository imageRepository,
        IProductDiscountRepository productDiscountRepository,
        IMapper mapper,
        ILogger<ProductService> logger,
        IUser user,
        ISearchService searchService,
        IEventPublisher eventPublisher
    ) : BaseResponseHandler, IProductService
    {
        private readonly IProductRepository _productRepository = productRepository;
        private readonly ICategoryRepository _categoryRepository = categoryRepository;
        private readonly ITagRepository _tagRepository = tagRepository;
        private readonly IProductDetailRepository _productDetailRepository = productDetailRepository;
        private readonly IPreviewRepository _previewRepository = previewRepository;
        private readonly IGenerativeAIService _generativeAIService = generativeAIService;
        private readonly IImageRepository _imageRepository = imageRepository;
        private readonly IProductDiscountRepository _productDiscountRepository = productDiscountRepository;
        private readonly ISearchService _searchService = searchService;
        private readonly IMapper _mapper = mapper;
        private readonly IUser _user = user;
        private readonly ILogger<ProductService> _logger = logger;
        private readonly IEventPublisher _eventPublisher = eventPublisher;

        public async Task<BaseResponse<CreateProductResponse>> CreateProduct(CreateProductRequest request)
        {
            var categories = await _categoryRepository.FindAllAsync(x => request.CategoryIds.Contains(x.Id));
            var tags = await GetTagsByName(request.Tags);

            var product = new Product()
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Thumbnail = request.Thumbnail,
                Description = request.Description,
                ShortDescription = request.ShortDescription,
                Price = request.Price,
                ProductType = request.Type,
                ProductStatus = ProductStatus.Active,
                ProductContentStatus = ProductContentStatus.Waiting,
                Categories = categories.ToList(),
                Tags = tags,
            };
            product.Slug = await GenerateSlugProduct(product.Name);

            await _productRepository.AddAsync(product);

            var productDetail = new ProductDetail()
            {
                Id = Guid.NewGuid(),
                ProductId = product.Id,
                Version = 1
            };
            await _productDetailRepository.AddAsync(productDetail);

            if (request.Discounts.Count > 0)
            {
                var discountsInsert = new List<ProductDiscount>();
                request.Discounts.ForEach((item) =>
                {
                    if (!DateTime.TryParse(item.TimeRange[0] ?? "", out DateTime startDate) ||
                        !DateTime.TryParse(item.TimeRange[1] ?? "", out DateTime endDate))
                    {
                        return;
                    }

                    discountsInsert.Add(new ProductDiscount()
                    {
                        Id = Guid.NewGuid(),
                        ProductId = product.Id,
                        Type = item.Type,
                        StartDate = startDate,
                        EndDate = endDate,
                        Value = item.Value
                    });
                });

                if (discountsInsert.Count > 0)
                {
                    await _productDiscountRepository.AddRangeAsync(discountsInsert);
                }
            }

            switch (product.ProductType)
            {
                case ProductType.Video:
                    var payloadVideo = new HandleProductVideoContentDTO()
                    {
                        ProductId = product.Id,
                        FileUrl = request.OriginalFiles[0].Url,
                        RangePreview = request.RangeVideoPreview,
                        ProductDetailId = productDetail.Id,
                    };

                    await _eventPublisher.PublishAsync(payloadVideo, "CreateProduct");

                    break;
                case ProductType.Image:
                    var imagesInsert = new List<Image>();
                    var payloadImage = new CheckProductImageContentDTO()
                    {
                        ProductId = product.Id,
                        PreviewsUrl = request.PreviewImages,
                        ProductDetailId = productDetail.Id,
                    };
                    foreach (var file in request.OriginalFiles)
                    {
                        payloadImage.ImagesUrl.Add(file.Url);
                        imagesInsert.Add(new Image()
                        {
                            Id = Guid.NewGuid(),
                            ProductId = product.Id,
                            ImageUrl = file.Url,
                            OrderIndex = 9999
                        });
                    }
                    await _imageRepository.AddRangeAsync(imagesInsert);
                    await _eventPublisher.PublishAsync(payloadImage, "CreateProduct");
                    break;
            }

            await _previewRepository.SaveChangesAsync();

            await _searchService.IndexProductAsync(product);

            return Success(_mapper.Map<CreateProductResponse>(product));
        }

        private async Task<string> GenerateSlugProduct(string productName)
        {
            string slugOriginal = productName.ToSlug();
            while (true)
            {
                var newSlug = slugOriginal + "-" + string.Empty.GenerateRandomString(5);
                bool isExistSlug = await _productRepository.IsExistAsync(x => x.Slug == newSlug);
                if (!isExistSlug)
                {
                    return newSlug;
                }
            }
        }

        private async Task<List<Tag>> GetTagsByName(List<string> tagsName)
        {
            var result = new List<Tag>();

            var tags = new List<Tag>();
            foreach (var tagName in tagsName)
            {
                tags.Add(new Tag()
                {
                    Id = Guid.NewGuid(),
                    Name = tagName,
                    Slug = tagName.ToSlug(),
                });
            }

            var tagsSlug = tags.Select(x => x.Slug).ToList();
            var tagsExist = await _tagRepository.FindAllAsync(x => tagsSlug.Contains(x.Slug));
            foreach (var tagExist in tagsExist)
            {
                result.Add(tagExist);
                tags.Remove(tags.Find(x => x.Slug == tagExist.Slug)!);
            }

            await _tagRepository.AddRangeAsync(tags);
            await _tagRepository.SaveChangesAsync();

            result.AddRange(tags);
            return result;
        }

        public async Task<BaseResponse<IEnumerable<ProductHomePageResponse>>> GetListProductsHomePageByCategory(Guid categoryId)
        {
            var products = await _categoryRepository.GetProductsByCategoryId(categoryId);
            return Success(_mapper.Map<IEnumerable<ProductHomePageResponse>>(products));
        }

        public async Task<BaseResponse<ProductDetailResponse>> GetProductDetail(string slug)
        {
            var product = await _productRepository.GetProductActiveWithRelationship(slug);
            return Success(product);
        }

        public async Task<BaseResponse<PaginatedResult<ProductUserResponse>>> GetMyProductsPaginated(GetProductListRequest request)
        {
            var result = await _productRepository.GetListProductsForUser(_user.Id, request.ProductType, request.Name, request.Page, request.PageSize);
            return Success(result);
        }

        public async Task<BaseResponse<IEnumerable<ProductLatestResponse>>> GetMyLatestProducts()
        {
            var products = await _productRepository.GetProductsLatest(_user.Id);
            return Success(_mapper.Map<IEnumerable<ProductLatestResponse>>(products));
        }

        public async Task<BaseResponse<ProductCheckoutResponse>> GetProductCheckoutInfo(ProductCheckoutRequest request)
        {
            var product = await _productRepository.FindAsync(x => x.Slug == request.Slug, ["Product"]);
            return Success(new ProductCheckoutResponse()
            {
                Id = product.Id,
                Name = product.Name,
                Slug = product.Slug,
                Price = product.Price,
                Thumbnail = product.Thumbnail,
                ShortDescription = product.ShortDescription,
            });
        }

        public async Task<BaseResponse<IEnumerable<ProductLatestResponse>>> GetLatestProductsOfUser(LatestProductOfUserRequest request)
        {
            var products = await _productRepository.GetProductsLatest(request.userId);
            return Success(_mapper.Map<IEnumerable<ProductLatestResponse>>(products));
        }

        public async Task<BaseResponse<EnhanceInformationResponse>> EnhanceInformation(EnhanceInformationRequest request)
        {
            var replaceContents = new Dictionary<string, string>()
            {
                { "PRODUCT_NAME", request.Name },
                { "SHORT_DESCRIPTION", request.ShortDescription },
                { "DESCRIPTION", request.Description },
                { "CONTENT_TYPE", request.ProductType == ProductType.Video ? "Video" : "Image" },
            };
            EnhanceInformationResponse result = null;
            int maxAttempts = 5;
            int attempt = 0;

            while (attempt < maxAttempts)
            {
                try
                {
                    result = await _generativeAIService.GenerateContentAsync<EnhanceInformationResponse>(typeof(ProductPrompt.EnhanceInformation).ReflectedType.Name, replaceContents);
                }
                catch (Exception ex)
                {
                    attempt++;

                    if (attempt >= maxAttempts)
                    {
                        throw new Exception("Có lỗi xảy ra");
                    }
                }
            }

            _logger.LogDebug(result.Description);
            result.Description = result.Description.Replace("\n", "<br>");
            string pattern = @"(<br\s*/?>\s*)+";
            result.Description = Regex.Replace(result.Description, pattern, "<br>");

            return Success(result);
        }

        public async Task<BaseResponse<object>> UpdateProductContentStatus(UpdateContentStatusRequest request)
        {
            var product = await _productRepository.FindByIdAsync(request.Id);
            product.ProductContentStatus = request.Status;
            await _productRepository.SaveChangesAsync();

            return Success<object>(new { Message = "Product content status updated successfully" });
        }

        public async Task<BaseResponse<object>> UpdateProductDetailFile(UpdateProductDetailRequest request)
        {
            var productDetail = await _productDetailRepository.FindByIdAsync(request.ProductDetailId);
            productDetail.FileUrl = request.FileUrl;
            await _productRepository.SaveChangesAsync();

            return Success<object>((new { Message = "Product detail updated successfully" }));
        }

        public async Task<BaseResponse<object>> CreateProductPreview(CreateProductPreviewRequest request)
        {
            var product = await _productRepository.FindByIdAsync(request.ProductId);
            var previews = new Preview()
            {
                Id = Guid.NewGuid(),
                ProductId = product.Id,
                PreviewInfo = JsonSerializer.Serialize(request.PreviewUrls)
            };

            await _previewRepository.AddAsync(previews);
            await _productRepository.SaveChangesAsync();

            return Success<object>((new { Message = "Product preview created successfully" }));
        }
    }
}
