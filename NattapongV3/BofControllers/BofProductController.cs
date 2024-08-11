using Microsoft.AspNetCore.Mvc;
using NattapongV3.Common;
using DataAccess.Interface;
using Course.Core.Models;
using System.Text.Json;
using DataAccess.Entities;
using Course.Core.Common;

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
        public async Task<IActionResult> Index()
        {
            ViewBag.Types = await productDa.GetTypeDdl();
            return View();
        }
        public async Task<IActionResult> GetById(int id)
        {
            var data = await productDa.GetById(id);

            return Json(data);
        }
        public async Task<IActionResult> GetDt(GetProductDtReq req)
        {
            var data = await productDa.GetDt(req);

            return Json(data);
        }
        public async Task<IActionResult> GetImageDt(GetProductImageDtReq req)
        {
            if (req.ProductId == 0)
                return Json(new GetProductImageDtRes { });

            var data = await productDa.GetImageDt(req);

            return Json(data);
        }
        public async Task<IActionResult> GetNextRanking(int typeId)
        {
            var data = await productDa.GetNextRanking(typeId);

            return Json(data);
        }
        public async Task<IActionResult> Save(Product data, List<IFormFile> files)
        {
            BaseRes res = new();
            try
            {
                List<ProductImage> images = new();

                if (data.Id == 0)
                {
                    if (files.Any())
                        images = await UploadFile(files, 1);

                    data.CreateBy = User.Identity.Name;
                    data.CreateDate = DateTime.Now;

                    await productDa.Insert(data, images);
                }
                else
                {
                    var rankings = JsonSerializer.Deserialize<List<ProductImage>>(data.Rankings);

                    if (files.Any())
                    {
                        int startRanking = rankings.Any() ? rankings.Max(x => x.Ranking) + 1 : 1;
                        images = await UploadFile(files, startRanking);
                    }

                    data.UpdateBy = data.CreateBy = User.Identity.Name;
                    data.UpdateDate = data.CreateDate = DateTime.Now;

                    await productDa.Update(data, images, rankings);
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
            await DeleteAllFile(id);

            await productDa.Delete(id, User.Identity.Name);

            return Json(true);
        }
        public async Task<IActionResult> DeleteImage(int id)
        {
            await DeleteFile(id);

            await productDa.DeleteImage(id, User.Identity.Name);

            return Json(true);
        }











        private async Task DeleteFile(int id)
        {
            var o = await productDa.GetImageById(id);

            if (!string.IsNullOrEmpty(o.Image))
                fileHelper.Delete(UploadFolder.Product, o.Image);
        }
        private async Task<List<ProductImage>> UploadFile(List<IFormFile> files, int startRanking)
        {
            List<ProductImage> images = new();

            int i = startRanking;
            foreach (var f in files)
            {
                images.Add(new ProductImage
                {
                    Image = await fileHelper.Upload(f, UploadFolder.Product),
                    Ranking = i
                });
                i++;
            }
            return images;
        }
        private async Task DeleteAllFile(int id)
        {
            var images = await productDa.GetImage(id);

            if (images.Any())
            {
                foreach (var image in images)
                {
                    fileHelper.Delete(UploadFolder.Product, image.Image);
                }
            }
        }
    }
}
