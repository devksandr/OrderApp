namespace OrderApp.Models.DTO.Form
{
    public class FormGetDataToCreateOrUpdateOrderResponseDTO
    {
        public required string FormTitle { get; set; }
        public required IEnumerable<string> ProviderNames { get; set; }
    }
}
