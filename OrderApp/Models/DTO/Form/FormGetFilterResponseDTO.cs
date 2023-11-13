namespace OrderApp.Models.DTO.Form
{
    public class FormGetFilterResponseDTO
    {
        public required IEnumerable<string> OrderNumbers;

        public required IEnumerable<string> OrderItemNames;
        public required IEnumerable<string> OrderItemUnits;

        public required IEnumerable<string> ProviderNames;
    }
}
