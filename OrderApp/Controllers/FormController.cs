using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using OrderApp.Models.Entities;
using OrderApp.Services.Interfaces;

namespace OrderApp.Controllers
{
    public class FormController : Controller
    {
        IFormService _formService;

        public FormController(IFormService formService)
        {
            _formService = formService;
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
    }
}
