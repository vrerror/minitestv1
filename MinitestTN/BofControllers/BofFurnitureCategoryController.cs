using Course.Core.Models;
using DataAccess.Da;
using DataAccess.Enities;
using DataAccess.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MinitestTN.Common;
using System;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MinitestTN.BofControllers
{
    //[Authorize]
    public class BofFurnitureCategoryController : Controller
    {
        private readonly IConfiguration configuration;
        private readonly IFurnitureCategoryDa icategoryDa;
        private readonly FileHelper fileHelper;
        private readonly string folderName = "Category";

        //private readonly string UserLogin;

        public BofFurnitureCategoryController(IConfiguration configuration, IFurnitureCategoryDa icategoryDa, FileHelper fileHelper)
        {
            //UserLogin = configuration.GetValue<String>("UserLogin");
            this.icategoryDa = icategoryDa;
            this.fileHelper = fileHelper;
        }
        public IActionResult Index()
        {
            return View();
        }
   

        public IActionResult GatAll()
        {
            var category = icategoryDa.GetAll();
            return Json(category);
        }

        public IActionResult GatCatByID(int id)
        {
            var category = icategoryDa.GetById(id);
            return Json(category);
        }
        public async Task<IActionResult> UpdateCat(FurnitureCategory data, IFormFile file1)
        {
            ResponseMessage res = new ResponseMessage();
            try
            {
                if (file1 != null)
                {
                    if (data.Image != null)
                        fileHelper.Delete(folderName, data.Image);

                    data.Image = await fileHelper.Upload(file1, folderName);

                }

                data.UpdateBy = User.Identity.Name;
                data.UpdateDate = DateTime.Now;

                //data.Image = await fileHelper.Upload(file1, folderName);

                icategoryDa.update(data);

            }
            catch (Exception ex)
            {
                res.Message = $"ERROR: {ex.Message}";
            }
            return Json(res);
        }
        public async Task<IActionResult> InsertCat(FurnitureCategory data, IFormFile file1)
        {

            var x = icategoryDa.GetByName(data.Name);
            if (x == null)
            {

                if (file1 != null)
                {
                    if (data.Image != null)
                        fileHelper.Delete(folderName, data.Image);

                    data.Image = await fileHelper.Upload(file1, folderName);

                }

                data.CreateBy = User.Identity.Name;
                data.CreateDate = DateTime.Now;

                icategoryDa.insert(data);
                //categoryDa.Insert(data);

                return Json("Success.");
            }
            else
            {
                return Json("Failed.");
            }

            //data.Image = await fileHelper.Upload(file1, folderName);

            //data.CreateDate = DateTime.Now;
            //data.CreateBy = User.Identity.Name;


            //categoryDa.insert(data);

            return Json(true);
        }
        public IActionResult DeleteCat(int id)
        {
            ResponseMessage res = new ResponseMessage();
            try
            {
                //var data = bLCategory.DeleteCategory(id, User.Identity.Name);
                var data = icategoryDa.Delete(id, User.Identity.Name);

                fileHelper.Delete(folderName, data.Image);
            }
            catch (Exception ex)
            {
                res.Message = "Error : " + ex.Message;
            }

            return Json(res);
        }

        public async Task<IActionResult> GetDataCatDT(getCategoryDTReq req)
        {

            var data = await icategoryDa.getDataCatDT(req);

            return Json(data);
        }
    }
}
