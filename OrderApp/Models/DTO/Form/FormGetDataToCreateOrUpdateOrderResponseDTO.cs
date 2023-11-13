using OrderApp.Models.DTO.Order;
using OrderApp.Models.DTO.Provider;

namespace OrderApp.Models.DTO.Form
{
    public class FormGetDataToCreateOrUpdateOrderResponseDTO
    {
        public required string FormTitle { get; set; }
        public required IEnumerable<ProviderDTO> Providers { get; set; }
        public required OrderDTO OrderData { get; set; }

    }
}
