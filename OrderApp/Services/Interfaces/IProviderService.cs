using OrderApp.Models.DTO.Order;
using OrderApp.Models.DTO.Provider;

namespace OrderApp.Services.Interfaces
{
    public interface IProviderService
    {
        public IEnumerable<ProviderGetResponseDTO> GetAllProviders();
        public ProviderGetResponseDTO GetProvider(int providerId);
    }
}
