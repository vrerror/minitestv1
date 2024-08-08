using Microsoft.AspNetCore.Mvc;

namespace MiniWeb.BofControllers
{
    public class BofProductController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
