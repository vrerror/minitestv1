using BusinessLogic.Bl;
using Course.Core.Models;
using DataAccess.Da;
using DataAccess.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using NattapongV3.Common;
using NattapongV3.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")] //[controller] เปลี่ยน [controller]/[action] ลบอัน[ApiController] มีค่าเท่ากัน
    [ApiController] // ต้องใส่เสมอ
    public class CategoryController : ControllerBase
    {
        private readonly CategoryDa categoryDa;
        private readonly CategoryBl categoryBl;

        public CategoryController(CategoryDa categoryDa, CategoryBl categoryBl)
        {

            this.categoryDa = categoryDa;
            this.categoryBl = categoryBl;
        }

        [HttpGet] //คล้ายกับ http post แต่อันนี้ของ get.Json
        [Route("test")]
        public string GetTest()
        {
            return "Hello";
        }

        [HttpGet]
        [Route("GetAll")]
        public List<Category> GetAll()
        {
            var a = categoryDa.GetAllA();
            return a;
        }

        //การใช้ post ต้องรีบค่าเป็น model


        [HttpPost]
        [Route("insert")]
        public async Task<string> Insert(InsertCategoryReq dataReq)
        {

            var o = categoryBl.InsertApi(dataReq);

            return o.ToString();

            //ไป CategoryBl ต่อ
            //var a = categoryDa.GetByName(dataReq.Name);
            //if (a == null)
            //{
            //    var data = new Category
            //    {
            //        Name = dataReq.Name,
            //        CreateBy = "top",
            //        CreateDate = DateTime.Now,
            //        IsActive = true,
            //    };
            //    categoryDa.Insert(data);
            //    return "Success";

            //}
            //return "Fail";





            //var x =  categoryDa.IsExist(dataReq.Name);
            //if (!x)
            //{
            //    var data = new Category
            //    {
            //        Name = dataReq.Name,
            //        CreateBy = "top",
            //        CreateDate = DateTime.Now,
            //        IsActive = true,
            //    };

            //    categoryDa.Insert(data);
            //    return "success";
            //}
            //return "Fail";
        }


        [HttpPost]
        [Route("Update")]
        public ResApi Update(UpdateCategoryReq id)
        {
            //throw บังคับให้ error
            // การบ้าน ให้ทำหน้าบ้าน ให้นำไปแปะหน้าบ้านใน insert update 

            if (id.id == 0)
                throw new Exception("ID is Required");

            if (string.IsNullOrEmpty(id.Name))
                throw new Exception("Name is Required");

            if (string.IsNullOrEmpty(id.User))
                throw new Exception("User is Required");


            try // ถ้าใน tryมี error จะเด้งไปใน catch จะผ่านไปได้ 
            {
                var y = categoryDa.GetByName(id.Name);
                if (y == null || (y.Id == id.id))
                {
                    var data = new Category
                    {
                        Id = id.id,
                        Name = id.Name,
                        UpdateBy = id.User,
                        IsDelete = id.IsActive
                    };
                    categoryDa.Update(data);
                    return new ResApi { success = true, message = "Success"};

                }
                else
                {
                    //throw new Exception();
                    return new ResApi { success = false, message = id.Name+" Duplicate" };
                }

            }
            catch (Exception e)
            {
                return new ResApi { success = false, message = e.Message }; 
            }

            finally
            {
                // การใช้ finally ไว้สำหรับ หลังจากที่ติดอะไรปัญหาใน finally ยังไงก็ต้องทำใน finally แต่อาจจะทำให้เราไม่เห้น
            }


        }


        //[HttpPost]
        //[Route("Update")]
        //public string Update(UpdateCategoryReq id)
        //{
        //    try // ถ้าใน tryมี error จะเด้งไปใน catch จะผ่านไปได้ 
        //    {
        //        var y = categoryDa.GetByName(id.Name);
        //        if (y.Name == null || (y.Id == id.id))
        //        {

        //            var data = new Category
        //            {
        //                Id = id.id,
        //                Name = id.Name,
        //                UpdateBy = id.User,
        //                IsDelete = id.IsActive
        //            };
        //            categoryDa.Update(data);
        //            return "Success";
        //        }
        //    }
        //    catch (Exception e)
        //    {

        //    }
        //    return "duplicate Data";

        //}







        [HttpDelete]
        [Route("Delete")]
        public void Delete(UpdateCategoryReq id)
        {
            var data = new Category
            {
                Id = id.id,
                Name = id.Name,
            };
            categoryDa.Delete(data.Id, data.Name);
        }




    }
}
