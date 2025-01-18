using Microsoft.AspNetCore.Mvc;

namespace TaskSimulation6.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
