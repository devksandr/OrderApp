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


    }
}
