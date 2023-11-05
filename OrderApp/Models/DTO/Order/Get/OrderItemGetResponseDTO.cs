namespace OrderApp.Models.DTO.Order
{
    public class OrderItemGetResponseDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Quantity { get; set; }
        public string Unit { get; set; }
    }
}
