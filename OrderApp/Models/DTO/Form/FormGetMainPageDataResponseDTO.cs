using OrderApp.Models.DTO.Form;
using OrderApp.Models.DTO.Order;

namespace OrderApp.Models.DTO.Form
{
    public class FormGetMainPageDataResponseDTO
    {
        public required IEnumerable<FormGetOrderRowResponseDTO> OrderRows { get; set; }
        public required FormGetFilterResponseDTO Filter { get; set; }
    }
}