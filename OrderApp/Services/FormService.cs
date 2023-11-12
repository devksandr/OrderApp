using OrderApp.Database;
using OrderApp.Models.DTO.Form;
using OrderApp.Models.DTO.Order;
using OrderApp.Models.Entities;
using OrderApp.Services.Interfaces;

namespace OrderApp.Services
{
    public class FormService : IFormService
    {
        private readonly ApplicationContext _db;
        private readonly IOrderService _orderService;
        private readonly IProviderService _providerService;

        public FormService(ApplicationContext db, IOrderService orderService, IProviderService providerService)
        {
            _db = db;
            _orderService = orderService;
            _providerService = providerService;
        }

        public FormGetMainPageDataResponseDTO GetDataToShowMainPage()
        {
            IEnumerable<FormGetOrderRowResponseDTO> GetOrderRows()
            {
                var ordersDTO = _orderService.GetAllOrders(false);
                var orderRows = MakeOrderRowsByOrders(ordersDTO);
                return orderRows;
            }

            FormGetFilterResponseDTO GetOrderFiltersData()
            {
                var ordersDTO = _orderService.GetAllOrders(false);
                var orderItemsDTO = _orderService.GetAllOrderItems();
                var providersDTO = _providerService.GetAllProviders();

                var filters = new FormGetFilterResponseDTO
                {
                    OrderNumbers = ordersDTO.Select(o => o.Number).Distinct(),
                    OrderDates = ordersDTO.Select(o => o.Date).Distinct(),
                    OrderProviderIds = ordersDTO.Select(o => o.ProviderId).Distinct(),
                    OrderItemNames = orderItemsDTO.Select(oi  => oi.Name).Distinct(),
                    OrderItemUnits = orderItemsDTO.Select(oi => oi.Unit).Distinct(),
                    ProviderNames = providersDTO.Select(p => p.Name).Distinct()
                };
                return filters;
            }

            var mainPageData = new FormGetMainPageDataResponseDTO
            {
                OrderRows = GetOrderRows(),
                Filter = GetOrderFiltersData()
            };

            return mainPageData;
        }

        public IEnumerable<FormGetOrderRowResponseDTO> ConvertOrdersToOrderRows(IEnumerable<OrderGetResponseDTO> orders) 
            => MakeOrderRowsByOrders(orders);

        public FormGetDataToCreateOrUpdateOrderResponseDTO GetDataToCreateOrUpdateOrder(string orderId)
        {
            var createOrder = string.IsNullOrEmpty(orderId);
            var orderForm = new FormGetDataToCreateOrUpdateOrderResponseDTO
            {
                FormTitle = createOrder ? "Create order form" : "Update order form",
                Providers = _providerService.GetAllProviders(),
                OrderData = createOrder ? new OrderGetResponseDTO() : _orderService.GetOrder(Convert.ToInt32(orderId), true)
            };
            return orderForm;
        }


        private IEnumerable<FormGetOrderRowResponseDTO> MakeOrderRowsByOrders(IEnumerable<OrderGetResponseDTO> orders)
        {
            var orderRows = new List<FormGetOrderRowResponseDTO>();
            foreach (var o in orders)
            {
                var orderRow = MakeFormOrderRowDTO(o);
                orderRows.Add(orderRow);
            }
            return orderRows;
        }

        private FormGetOrderRowResponseDTO MakeFormOrderRowDTO(OrderGetResponseDTO orderDTO)
        {
            var providerName = _providerService.GetProvider(orderDTO.ProviderId).Name;
            return new FormGetOrderRowResponseDTO
            {
                Id = orderDTO.Id,
                Number = orderDTO.Number,
                Date = orderDTO.Date,
                ProviderName = providerName
            };
        }
    }
}
