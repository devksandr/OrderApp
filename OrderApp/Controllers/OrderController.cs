using Microsoft.AspNetCore.Mvc;
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
        public IActionResult GetDataToCreate(string orderId)
        {
            /*
            string dataToModal = "controller test data";
            var createOrderData = _orderService.GetDataToCreate(orderId);
            return PartialView("_CreateOrUpdateOrderForm", dataToModal);
            */
            return null;
        }
    }
}
