using AutoMapper;
using MediaMarket.Application.Bases;
using MediaMarket.Application.Contracts.Services;
using MediaMarket.Application.DTO.Product;
using MediaMarket.Application.DTO.Response.Product;
using MediaMarket.Domain.Entities;
using Microsoft.Extensions.Logging;
using Nest;

namespace MediaMarket.Infrastructure.Search
{
    public class ElasticSearchService(
        IElasticClient elasticClient,
        IMapper mapper,
        ILogger<ElasticSearchService> logger
    ) : BaseResponseHandler, ISearchService
    {
        private readonly IElasticClient _elasticClient = elasticClient;
        private readonly ILogger<ElasticSearchService> _logger = logger;
        private readonly IMapper _mapper = mapper;

        public async Task<BaseResponse<List<SearchProductResponse>>> GetProductsSearchResult(string keyword)
        {
            var searchResponse = await _elasticClient.SearchAsync<ProductDTO>(s => s
                .Query(q => q
                    .Bool(b => b
                        .Should(
                            m => m.Match(mt => mt
                                .Field(f => f.Name)
                                .Query(keyword)
                                .Fuzziness(Fuzziness.Auto)
                            ),
                            m => m.Match(mt => mt
                                .Field(f => f.ShortDescription)
                                .Query(keyword)
                                .Fuzziness(Fuzziness.Auto)
                            ),
                            m => m.Match(mt => mt
                                .Field(f => f.Description)
                                .Query(keyword)
                                .Fuzziness(Fuzziness.Auto)
                            )
                        )
                    )
                )
            );

            if (searchResponse.IsValid && searchResponse.Documents.Count > 0)
            {
                return Success(_mapper.Map<List<SearchProductResponse>>(searchResponse.Documents.ToList()));
            }

            return Success(new List<SearchProductResponse>());
        }

        public async Task IndexProductAsync(Product product)
        {
            var data = _mapper.Map<ProductDTO>(product);

            var response = await _elasticClient.IndexDocumentAsync(data);
            if (!response.IsValid)
            {
                _logger.LogError($"Failed to index document: {response.OriginalException.Message}");
                throw new Exception($"Failed to index document: {response.OriginalException.Message}");
            }

            _logger.LogInformation("Index product successfully");
        }
    }
}
