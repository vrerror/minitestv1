using MinitestTN.Common;
using DataAccess.Da;
using DataAccess.Enities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MinitestTN.BofControllers
{
    //[Authorize]
    public class BofFurnitureProductController : Controller
    {

        private readonly IConfiguration configuration;

        //private readonly string UserLogin;
        private readonly FurnitureProductDa productDa;
        private readonly FileHelper fileHelper;
        private readonly string folderName = "Products";

        public BofFurnitureProductController(IConfiguration configuration, FurnitureProductDa productDa, FileHelper fileHelper)
        {
            //UserLogin = configuration.GetValue<String>("UserLogin");
            this.productDa = productDa;
            this.fileHelper = fileHelper;
        }
        public IActionResult Index()
        {
            return View();
        }



        public IActionResult GatAll(string name)
        {
            var Products = productDa.GetAll(name);
            return Json(Products);
        }

        public IActionResult GatProByID(int id)
        {
            var Products = productDa.GetById(id);
            return Json(Products);
        }
        public async Task<IActionResult> UpdatePro(FurnitureProduct data, IFormFile file1)
        {
            if (file1 != null)
            {
                if (data.Image != null)
                    fileHelper.Delete(folderName, data.Image);

            }

            data.UpdateBy = User.Identity.Name;
            data.UpdateDate = DateTime.Now;
            data.Image = await fileHelper.Upload(file1, folderName);

            productDa.update(data);

            return Json(true);
        }
        public async Task<IActionResult> InsertPro(FurnitureProduct data, IFormFile file1)
        {
            data.Image = await fileHelper.Upload(file1, folderName);

            data.CreateDate = DateTime.Now;
            data.CreateBy = User.Identity.Name;

            productDa.insert(data);

            return Json(true);
        }
        public IActionResult DeletePro(int id)
        {
            var dataDelete = productDa.Delete(id, User.Identity.Name);


            fileHelper.Delete(folderName, dataDelete.Image);

            return Json(true);
        }

        public IActionResult getProducts(string name)
        {
            var Products = productDa.getProducts(name);
            return Json(Products);
        }
    }
}
