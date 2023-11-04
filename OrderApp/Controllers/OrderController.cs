using Microsoft.AspNetCore.Mvc;

namespace OrderApp.Controllers
{
    public class OrderController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
