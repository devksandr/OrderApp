namespace OrderApp.Models.DTO.Form
{
    public class FormGetOrderRowResponseDTO
    {
        public int Id { get; set; }
        public required string Number { get; set; }
        public DateTime Date { get; set; }
    }
}
