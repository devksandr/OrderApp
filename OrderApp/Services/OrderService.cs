using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
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

        public bool CreateOrder(OrderGetResponseDTO orderData)
        {
            using (var transaction = _db.Database.BeginTransaction())
            {
                try
                {
                    var order = new Order 
                    { 
                        Number = orderData.Number, 
                        Date = orderData.Date, 
                        ProviderId = orderData.ProviderId 
                    };

                    _db.Orders.Add(order);
                    _db.SaveChanges();

                    var orderItems = new List<OrderItem>();
                    foreach (var orderItemData in orderData.Items)
                    {
                        var orderItem = MakeOrderItemByDTO(orderItemData, order.Id);
                        orderItems.Add(orderItem);
                    }

                    _db.OrderItems.AddRange(orderItems);
                    _db.SaveChanges();

                    transaction.Commit();
                    return true;
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    return false;
                }
            }
        }

        public bool DeleteOrder(int orderId)
        {
            using (var transaction = _db.Database.BeginTransaction())
            {
                try
                {
                    var orderItems = _db.OrderItems.Where(oi => oi.OrderId == orderId).ToList();
                    foreach (var orderItem in orderItems)
                    {
                        _db.OrderItems.Remove(orderItem);
                    }

                    var order = new Order { Id = orderId };
                    _db.Orders.Remove(order);   
                    _db.SaveChanges();

                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                    return false;
                }
            }

            return true;
        }

        public bool UpdateOrder(OrderGetResponseDTO orderData)
        {
            using (var transaction = _db.Database.BeginTransaction())
            {
                try
                {
                    var order = _db.Orders.Find(orderData.Id);
                    order.Number = orderData.Number;
                    order.Date = orderData.Date;
                    order.ProviderId = orderData.ProviderId;
                    _db.SaveChanges();

                    var deletedOrderItemsId = _db.OrderItems.Where(oi => oi.OrderId == orderData.Id).Select(oi => oi.Id).ToList();
                    foreach (var orderItemData in orderData.Items)
                    {
                        deletedOrderItemsId.RemoveAll(oiId => oiId == orderItemData.Id);
                        var orderItem = orderItemData.Id > 0 ? _db.OrderItems.Find(orderItemData.Id) : MakeOrderItemByDTO(orderItemData, orderData.Id);
                        orderItem.Name = orderItemData.Name;
                        orderItem.Quantity = orderItemData.Quantity;
                        orderItem.Unit = orderItemData.Unit;

                        if (orderItemData.Id <= 0)
                        {
                            _db.OrderItems.Add(orderItem);
                        }
                    }
                    _db.SaveChanges();

                    foreach (var orderItemIdToDelete in deletedOrderItemsId)
                    {
                        var orderItem = new OrderItem { Id = orderItemIdToDelete };
                        _db.OrderItems.Remove(orderItem);
                    }
                    _db.SaveChanges();

                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                    return false;
                }
            }

            return true;
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
                            Id = oi.Id,
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

        private OrderItem MakeOrderItemByDTO(OrderItemGetResponseDTO orderItemDTO, int orderId)
        {
            return new OrderItem
            {
                OrderId = orderId,
                Name = orderItemDTO.Name,
                Quantity = orderItemDTO.Quantity,
                Unit = orderItemDTO.Unit
            };
        }
    }
}
