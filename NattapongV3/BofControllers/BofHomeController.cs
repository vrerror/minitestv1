using Microsoft.AspNetCore.Mvc;

namespace MiniWeb.BofControllers
{
    public class BofHomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
