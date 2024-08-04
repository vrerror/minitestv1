using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TST.Web.BofControllers
{
    //[Authorize]
    public class BofHomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
