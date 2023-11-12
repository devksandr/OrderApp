namespace OrderApp.Models.ValidateModels
{
    public class OrderValidateModel
    {
        public required string Status { get; set; }
        public required List<string> FormErrors { get; set;}
    }
}
