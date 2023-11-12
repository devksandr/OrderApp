using OrderApp.Database;
using OrderApp.Models.DTO.Form;
using OrderApp.Models.DTO.Order;
using OrderApp.Models.DTO.Provider;
using OrderApp.Services.Interfaces;

namespace OrderApp.Services
{
    public class ProviderService : IProviderService
    {
        private readonly ApplicationContext _db;

        public ProviderService(ApplicationContext db)
        {
            _db = db;
        }

        public IEnumerable<ProviderGetResponseDTO> GetAllProviders()
        {
            var providersDTO = new List<ProviderGetResponseDTO>();

            foreach (var p in _db.Providers)
            {
                var providerDTO = new ProviderGetResponseDTO
                {
                    Id = p.Id,
                    Name = p.Name
                };
                providersDTO.Add(providerDTO);
            }

            return providersDTO;
        }

        public ProviderGetResponseDTO GetProvider(int providerId)
        {
            var provider = _db.Providers.Find(providerId);

            var providerDTO = new ProviderGetResponseDTO
            {
                Id = provider.Id,
                Name = provider.Name
            };

            return providerDTO;
        }
    }
}
