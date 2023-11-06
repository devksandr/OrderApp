using OrderApp.Database;
using OrderApp.Models.DTO.Form;
using OrderApp.Models.DTO.Order;
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
            List<FormGetOrderRowResponseDTO> GetOrderRows()
            {
                var ordersDTO = _orderService.GetAllOrders(false);
                var orderRows = new List<FormGetOrderRowResponseDTO>();
                foreach (var o in ordersDTO)
                {
                    var orderRow = new FormGetOrderRowResponseDTO
                    {
                        Id = o.Id,
                        Number = o.Number,
                        Date = o.Date,
                    };
                    orderRows.Add(orderRow);
                }
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
        {
            var orderRows = new List<FormGetOrderRowResponseDTO>();
            foreach (var o in orders)
            {
                var orderRow = new FormGetOrderRowResponseDTO
                {
                    Id = o.Id,
                    Number = o.Number,
                    Date = o.Date,
                };
                orderRows.Add(orderRow);
            }
            return orderRows;
        }

        public FormGetDataToCreateOrUpdateOrderResponseDTO GetDataToCreateOrUpdateOrder(string orderId)
        {
            var providersDTO = _providerService.GetAllProviders();

            var createOrder = string.IsNullOrEmpty(orderId);
            var orderForm = new FormGetDataToCreateOrUpdateOrderResponseDTO
            {
                FormTitle = createOrder ? "Create order form" : "Update order form",
                ProviderNames = providersDTO.Select(p => p.Name)
            };

            return orderForm;
        }
    }
}
