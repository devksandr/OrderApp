using OrderApp.Database;
using OrderApp.Models.DTO.Form;
using OrderApp.Models.DTO.Order;
using OrderApp.Models.Entities;
using OrderApp.Services.Interfaces;
using System.Linq;

namespace OrderApp.Services
{
    public class OrderService : IOrderService
    {
        private readonly ApplicationContext _db;

        public OrderService(ApplicationContext db)
        {
            _db = db;
        }

        public OrderGetResponseDTO GetOrder(int orderId, bool includeItems)
            => CreateOrdersDTO(new List<Order> { _db.Orders.Find(orderId) }, includeItems).First();

        public IEnumerable<OrderGetResponseDTO> GetAllOrders(bool includeItems) 
            => CreateOrdersDTO(_db.Orders.ToList(), includeItems);

        public IEnumerable<OrderItemGetResponseDTO> GetAllOrderItems()
        {
            var orderItemsDTO = new List<OrderItemGetResponseDTO>();

            foreach (var oi in _db.OrderItems)
            {
                var orderItemDTO = new OrderItemGetResponseDTO
                {
                    Id = oi.Id,
                    Name = oi.Name,
                    Quantity = oi.Quantity,
                    Unit = oi.Unit
                };
                orderItemsDTO.Add(orderItemDTO);
            }

            return orderItemsDTO;
        }

        public IEnumerable<OrderGetResponseDTO> GetFilteredOrders(FormGetFilteredOrdersRequestDTO filters)
        {
            var ordersDTO = new List<OrderGetResponseDTO>();
            var filteredOrders = _db.Orders.ToList().Where(o => 
                    o.Date >= filters.OrderDateStart && 
                    o.Date <= filters.OrderDateEnd &&
                    (filters.OrderNumbers == null || filters.OrderNumbers.Contains(o.Number)) &&
                    (filters.OrderProviderIds == null || filters.OrderProviderIds.Contains(o.ProviderId)) &&
                    (filters.OrderItemNames == null || !filters.OrderItemNames.Except(_db.OrderItems.Where(oi => oi.OrderId == o.Id).Select(oi => oi.Name)).Any()) &&
                    (filters.OrderItemUnits == null || !filters.OrderItemUnits.Except(_db.OrderItems.Where(oi => oi.OrderId == o.Id).Select(oi => oi.Unit)).Any()) &&
                    (filters.ProviderNames == null || filters.ProviderNames.Contains(_db.Providers.Find(o.ProviderId).Name)))
                .ToList();

            return CreateOrdersDTO(filteredOrders, false);
        }

        private IEnumerable<OrderGetResponseDTO> CreateOrdersDTO(List<Order> orders, bool includeItems)
        {
            var ordersDTO = new List<OrderGetResponseDTO>();
            foreach (var o in orders)
            {
                var orderItemsDTO = new List<OrderItemGetResponseDTO>();
                if (includeItems)
                {
                    foreach (var oi in _db.OrderItems.Where(oi => oi.OrderId == o.Id))
                    {
                        var orderItemDTO = new OrderItemGetResponseDTO
                        {
                            Id = oi.OrderId,
                            Name = oi.Name,
                            Quantity = oi.Quantity,
                            Unit = oi.Unit
                        };
                        orderItemsDTO.Add(orderItemDTO);
                    }
                }

                var orderDTO = new OrderGetResponseDTO
                {
                    Id = o.Id,
                    Number = o.Number,
                    Date = o.Date,
                    ProviderId = o.ProviderId,
                    Items = orderItemsDTO
                };
                ordersDTO.Add(orderDTO);
            }
            return ordersDTO;
        }
    }
}
