﻿using DataAccess.Interface;
using Microsoft.AspNetCore.Mvc;
using NattapongV3.Common;
using Course.Core.Models;
using DataAccess.Entities;
using Course.Core.Common;

namespace MiniWeb.BofControllers
{
    public class BofCategoryController : Controller
    {
        private readonly FileHelper fileHelper;
        private readonly ICategoryDa categoryDa;

        public BofCategoryController(FileHelper fileHelper, ICategoryDa categoryDa)
        {
            this.fileHelper = fileHelper;
            this.categoryDa = categoryDa;
        }


        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GetCatByID(int id)
        {
            var data = await categoryDa.GetById(id);
            return Json(data);
        }

        public async Task<IActionResult> GetAll(GetCategoryDtReq req)
        {
            var data = await categoryDa.GetAll(req);
            return Json(data);
        }

        public async Task<IActionResult> Save(ProductType data, IFormFile file1)
        {
            BaseRes res = new();
            try
            {

                if (data.Id == 0)
                {
                    var x = categoryDa.GetByName(data.Name);
                    if(x != null && (x.Name== data.Name))
                    {
                        throw new ArgumentException("Duplicate Name");
                    };

                    if (file1 == null)
                        throw new ArgumentException("Image is required.");

                    data.Image = await fileHelper.Upload(file1, UploadFolder.Category);
                    data.CreateBy = User.Identity.Name;
                    data.CreateDate = DateTime.Now;

                    await categoryDa.Insert(data);
                }
                else
                {

                    if (file1 != null)
                    {
                        data.Image = await fileHelper.Upload(file1, UploadFolder.Category);
                        await DeleteFile(data.Id);
                    }

                    data.UpdateBy = User.Identity.Name;
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
