using AutoMapper;
using MediaMarket.Application.Bases;
using MediaMarket.Application.Contracts.Repositories;
using MediaMarket.Application.Contracts.Services;
using MediaMarket.Application.DTO.Request.Product;
using MediaMarket.Application.DTO.Response.Product;
using MediaMarket.Application.Utils;
using MediaMarket.Domain.Entities;
using MediaMarket.Domain.Enums;

namespace MediaMarket.Application.Services
{
    public class ProductService(
        IProductRepository _productRepository,
        ICategoryRepository _categoryRepository,
        ITagRepository _tagRepository,
        IProductDetailRepository _productDetailRepository,
        IPreviewRepository _previewRepository,
        IVideoSolutionRepository _videoSolutionRepository,
        IMapper _mapper
    ) : BaseResponseHandler, IProductService
    {
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
                SellerId = UserIdCreate,
                ProductType = request.Type,
                ProductStatus = request.Status,
                ProductContentStatus = ProductContentStatus.Waiting,
                Categories = (ICollection<Category>)categories,
                Tags = tags,
            };

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
                    Slug = Helper.StringToSlug(tagName)
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

        public async Task<BaseResponse<ProductDetailResponse>> GetProductDetail(Guid productId)
        {
            var product = await _productRepository.GetProductActiveWithRelationship(productId);
            return Success(product);
        }
    }
}
