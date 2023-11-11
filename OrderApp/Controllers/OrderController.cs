using Microsoft.AspNetCore.Mvc;
using OrderApp.Models.DTO.Order;
using OrderApp.Services.Interfaces;

namespace OrderApp.Controllers
{
    public class OrderController : Controller
    {
        IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public void Create(OrderGetResponseDTO orderData)
        {
            //var result = _orderService.CreateOrder(orderData);
        }
        [HttpPut]
        public void Update(OrderGetResponseDTO orderData)
        {
            //var filteredOrders = _orderService.GetFilteredOrders(filters);
            //var orderRows = _formService.ConvertOrdersToOrderRows(filteredOrders);
            //return PartialView("_OrdersTable", orderRows);
        }
    }
}
