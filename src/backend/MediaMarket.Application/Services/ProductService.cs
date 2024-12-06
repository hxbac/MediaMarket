﻿using AutoMapper;
using MediaMarket.Application.Bases;
using MediaMarket.Application.Contracts.Common;
using MediaMarket.Application.Contracts.Repositories;
using MediaMarket.Application.Contracts.Services;
using MediaMarket.Application.DTO.Request.Product;
using MediaMarket.Application.DTO.Response.Product;
using MediaMarket.Domain.Entities;
using MediaMarket.Domain.Enums;

namespace MediaMarket.Application.Services
{
    public class ProductService(
        IProductRepository productRepository,
        ICategoryRepository categoryRepository,
        ITagRepository tagRepository,
        IProductDetailRepository productDetailRepository,
        IPreviewRepository previewRepository,
        IVideoSolutionRepository videoSolutionRepository,
        IMapper mapper,
        IUser user
    ) : BaseResponseHandler, IProductService
    {
        private readonly IProductRepository _productRepository = productRepository;
        private readonly ICategoryRepository _categoryRepository = categoryRepository;
        private readonly ITagRepository _tagRepository = tagRepository;
        private readonly IProductDetailRepository _productDetailRepository = productDetailRepository;
        private readonly IPreviewRepository _previewRepository = previewRepository;
        private readonly IVideoSolutionRepository _videoSolutionRepository = videoSolutionRepository;
        private readonly IMapper _mapper = mapper;
        private readonly IUser _user = user;

        public async Task<BaseResponse<CreateProductResponse>> CreateProduct(CreateProductRequest request, Guid UserIdCreate)
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
                ProductStatus = request.Status,
                ProductContentStatus = ProductContentStatus.Waiting,
                Categories = categories.ToList(),
                Tags = tags,
            };
            product.Slug = await GenerateSlugProduct(product.Name);

            await _productRepository.AddAsync(product);

            //Todo: Generate product preview into a job
            await _productDetailRepository.AddAsync(new ProductDetail()
            {
                Id = Guid.NewGuid(),
                ProductId = product.Id,
                FileUrl = request.OriginalFiles[0].Url,
                Version = 1
            });

            await _videoSolutionRepository.AddAsync(new VideoSolution()
            {
                Id = Guid.NewGuid(),
                FileUrl = request.OriginalFiles[0].Url,
                Product = product,
            });

            await _previewRepository.SaveChangesAsync();

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
            var product = await _productRepository.FindAsync(x => x.Slug == request.Slug);
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
    }
}
