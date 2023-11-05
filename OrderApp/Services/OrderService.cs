using OrderApp.Database;
using OrderApp.Models.DTO.Form;
using OrderApp.Models.DTO.Order;
using OrderApp.Services.Interfaces;

namespace OrderApp.Services
{
    public class OrderService : IOrderService
    {
        private readonly ApplicationContext _db;

        public OrderService(ApplicationContext db)
        {
            _db = db;
        }

        public IEnumerable<OrderGetResponseDTO> GetAllOrders(bool includeItems)
        {
            var ordersDTO = new List<OrderGetResponseDTO>();
            foreach (var o in _db.Orders)
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
    }
}
