using OrderApp.Models.DTO.Form;
using OrderApp.Models.DTO.Order;

namespace OrderApp.Services.Interfaces
{
    public interface IOrderService
    {
        public OrderGetResponseDTO GetOrder(int orderId, bool includeItems);
        public IEnumerable<OrderGetResponseDTO> GetAllOrders(bool includeItems);
        public IEnumerable<OrderItemGetResponseDTO> GetAllOrderItems();
        public IEnumerable<OrderGetResponseDTO> GetFilteredOrders(FormGetFilteredOrdersRequestDTO filters);
        public IEnumerable<OrderGetResponseDTO> GetOrdersByNumberAndProviderId(string orderNumber, int providerId);
        public bool CreateOrder(OrderGetResponseDTO orderData);
        public bool DeleteOrder(int orderId);
        public bool UpdateOrder(OrderGetResponseDTO orderData);
    }
}
