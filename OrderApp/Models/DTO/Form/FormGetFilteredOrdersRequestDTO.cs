namespace OrderApp.Models.DTO.Form
{
    public class FormGetFilteredOrdersRequestDTO
    {
        public DateTime OrderDateStart { get; set; }
        public DateTime OrderDateEnd { get; set; }
        public required List<string> OrderNumbers { get; set; }

        public required List<string> OrderItemNames { get; set; }
        public required List<string> OrderItemUnits { get; set; }

        public required List<string> ProviderNames { get; set; }
    }
}
