using Course.Core.Common;
using DataAccess.Da;
using DataAccess.Entities;
using Microsoft.AspNetCore.Mvc;
using NattapongV3.Models;
using System.Diagnostics;

namespace NattapongV3.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration configuration;
        private readonly UserDa userDa;

        public HomeController(ILogger<HomeController> logger, IConfiguration configuration, UserDa userDa)
        {
            _logger = logger;
            this.configuration = configuration;
            this.userDa = userDa;
        }

        //เดิมถ้าไม่ได้ทำ login microsoft 11/5/2024 เปลี่ยนรูปด้านล่าง
        //public IActionResult Index()
        //{
        //    return View();
        //}

        public async Task<IActionResult> Index()
        {
            //แค่สั่ง user 1 ชุดแล้ว comment ไว้ก่อน 11 / 5 / 2024
            //var a = new ApplicationUser
            //{
            //    FirstName = "Nattapong",
            //    LastName = "Chanthananon",
            //    UserName = "Top",
            //    Password = "1234",
            //    CreateBy = "Top",
            //    CreateDate = DateTime.Now,
            //    IsActive = true,
            //    PhoneNumber = "0897915256",
            //    Email = "Chanthananon@gmail.com",
            //};

            //await userDa.Insert(a);



            return View();
        }



        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
