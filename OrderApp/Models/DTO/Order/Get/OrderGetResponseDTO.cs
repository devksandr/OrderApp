namespace OrderApp.Models.DTO.Order
{
    public class OrderGetResponseDTO
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public DateTime Date { get; set; }
        public int ProviderId { get; set; }
        public IEnumerable<OrderItemGetResponseDTO>? Items { get; set; }
    }
}
