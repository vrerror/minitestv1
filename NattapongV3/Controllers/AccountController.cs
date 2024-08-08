using DataAccess.Da;
using DataAccess.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NattapongV3.Models;
using static Dapper.SqlMapper;


namespace NattapongV3.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly IConfiguration configuration;
        private readonly UserDa userDa;

        public AccountController(SignInManager<ApplicationUser> signInManager, IConfiguration configuration, UserDa userDa)
        {
            this.signInManager = signInManager;
            this.configuration = configuration;
            this.userDa = userDa;
        }

        [AllowAnonymous]  //ถ้าใส่คำสั่งนี้ยกเว้นสิทธิที่ต้อง ใส่ user ถึงจะใช้ได้
        public IActionResult Login()
        {

            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(LoginModel data)
        {
            //var passwordHasher = new PasswordHasher<LoginModel>();
            //var Password = passwordHasher.HashPassword(data, data.Password);




            var result = await signInManager.PasswordSignInAsync(data.UserName,data.Password, data.RememberMe, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }
            

            
            return View();
        }



        [AllowAnonymous]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }


        [AllowAnonymous]
        public async Task<IActionResult> Register()
        {
            //await userDa.Insert(data);


            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Register(ApplicationUser data)
        {

            data.PhoneNumber = "123456789";
            data.CreateBy = User.Identity.Name;
            data.CreateDate = DateTime.Now;
            data.IsActive = true;

            // ต้องไปเช็ค IsActive กับ IsDelete


            await userDa.Insert(data);


            return View();
        }



    }
}
