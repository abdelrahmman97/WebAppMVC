using Microsoft.AspNetCore.Mvc;

namespace WebAppMVC.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
