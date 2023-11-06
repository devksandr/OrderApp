namespace OrderApp.Models.DTO.Order
{
    public class OrderGetResponseDTO
    {
        public int Id { get; set; }
        public string Number { get; set; } = string.Empty;
        public DateTime Date { get; set; } = DateTime.Today;
        public int ProviderId { get; set; }
        public IEnumerable<OrderItemGetResponseDTO>? Items { get; set; } = new List<OrderItemGetResponseDTO>();
    }
}
