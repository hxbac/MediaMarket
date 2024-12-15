using MediaMarket.Application.DTO.GenerativeContentResponse.Product;

namespace MediaMarket.Application.Contracts.Services
{
    public interface IContentGeneratorService
    {
        Task<EnhanceProductInformationDTO> GenerateProductContent();
    }
}
