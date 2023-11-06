using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using OrderApp.Models.DTO.Form;
using OrderApp.Models.DTO.Order;
using OrderApp.Models.Entities;
using OrderApp.Services;
using OrderApp.Services.Interfaces;

namespace OrderApp.Controllers
{
    public class FormController : Controller
    {
        IFormService _formService;
        IOrderService _orderService;

        public FormController(IFormService formService, IOrderService orderService)
        {
            _formService = formService;
            _orderService = orderService;
        }

        public IActionResult GetDataToShowMainPage()
        {
            var mainPageData = _formService.GetDataToShowMainPage();
            return View(mainPageData);
        }

        [HttpPost]
        public IActionResult GetDataToCreateOrder(string orderId)
        {
            /*
            var createdOrderData = _formService.GetDataToCreateOrder(orderId);
            return PartialView("_CreateOrUpdateOrderForm", createdOrderData);
            */
            return null;
        }

        [HttpPost]
        public IActionResult GetFilteredOrders(FormGetFilteredOrdersRequestDTO filters)
        {
            var filteredOrders = _orderService.GetFilteredOrders(filters);
            var orderRows = _formService.ConvertOrdersToOrderRows(filteredOrders);
            return PartialView("_OrdersTable", orderRows);
        }
    }
}
