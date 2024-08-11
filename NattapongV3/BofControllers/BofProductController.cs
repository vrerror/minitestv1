using Microsoft.AspNetCore.Mvc;
using NattapongV3.Common;
using DataAccess.Interface;

namespace MiniWeb.BofControllers
{
    public class BofProductController : Controller
    {
        private readonly FileHelper fileHelper;
        private readonly IProductDa productDa;

        public BofProductController(FileHelper fileHelper, IProductDa productDa)
        {
            this.fileHelper = fileHelper;
            this.productDa = productDa;
        }

        //public IActionResult Index()
        //{
        //    return View();
        //}

        public async Task<IActionResult> Index()
        {
            ViewBag.Types = await productDa.GetTypePd();
            return View();
        }

        public async Task<IActionResult> GetById(int id)
        {
            var data = await productDa.GetById(id);

            return Json(data);
        }





    }
}
