using OrderApp.Models.DTO.Form;
using OrderApp.Models.DTO.Order;

namespace OrderApp.Services.Interfaces
{
    public interface IOrderService
    {
        public OrderDTO GetOrder(int orderId, bool includeItems);
        public IEnumerable<OrderDTO> GetAllOrders(bool includeItems);
        public IEnumerable<OrderItemDTO> GetAllOrderItems();
        public IEnumerable<OrderDTO> GetFilteredOrders(FormGetFilteredOrdersRequestDTO filters);
        public IEnumerable<OrderDTO> GetOrdersByNumberAndProviderId(string orderNumber, int providerId);
        public bool CreateOrder(OrderDTO orderData);
        public bool DeleteOrder(int orderId);
        public bool UpdateOrder(OrderDTO orderData);
    }
}
