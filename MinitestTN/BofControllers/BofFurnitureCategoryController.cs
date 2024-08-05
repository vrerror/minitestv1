using Course.Core.Common;
using Course.Core.Models;
using DataAccess.Da;
using DataAccess.Enities;
using DataAccess.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MinitestTN.Common;
using MySqlX.XDevAPI;
using System;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MinitestTN.BofControllers
{
    //[Authorize]
    public class BofFurnitureCategoryController : Controller
    {
        
        private readonly IFurnitureCategoryDa categoryDa;
        private readonly FileHelper fileHelper;
      

        public BofFurnitureCategoryController(IFurnitureCategoryDa categoryDa, FileHelper fileHelper)
        {
            this.categoryDa = categoryDa;
            this.fileHelper = fileHelper;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GetAll(GetCategoryDTReq req)
        {
            var data = await categoryDa.GetAll(req);
            return Json(data);
        }

        public async Task<IActionResult> GetCatByID(int id)
        {
            var data = await categoryDa.GetById(id);
            return Json(data);
        }

        public async Task<IActionResult> Save(FurnitureCategory data, IFormFile file1)
        {
            BaseRes res = new();
            try
            {
                if (data.Id == 0)
                {
                    if (file1 == null)
                        throw new ArgumentException("Image is required.");

                    data.Image = await fileHelper.Upload(file1, UploadFolder.Category);
                    data.CreateBy = "Nattapong";
                    data.CreateDate = DateTime.Now;

                    categoryDa.Insert(data);
                }
                else
                {
                    if (file1 != null)
                    {
                        data.Image = await fileHelper.Upload(file1, UploadFolder.Category);

                        await DeleteFile(data.Id);
                    }

                    data.UpdateBy = "Nattapong";
                    data.UpdateDate = DateTime.Now;
                    await categoryDa.Update(data);
                }

                res.Success = true;
            }
            catch (ArgumentException ex)
            {
                res.Message = ex.Message;
            }
            catch (Exception ex)
            {
                res.Message = $"ERROR: {ex.Message}";
            }
            return Json(res);
        }

        public async Task<IActionResult> Delete(int id)
        {
            await DeleteFile(id);

            await categoryDa.Delete(id, User.Identity.Name);

            return Json(true);
        }

        private async Task DeleteFile(int id)
        {
            var o = await categoryDa.GetById(id);

            if (!string.IsNullOrEmpty(o.Image))
                fileHelper.Delete(UploadFolder.Category, o.Image);
        }

        public async Task<IActionResult> GetNextRanking()
        {
            var data = await categoryDa.GetNextRanking();

            return Json(data);
        }

    }
}






















       