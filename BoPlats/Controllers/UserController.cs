using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BoPlats.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication; //login
using Microsoft.AspNetCore.Authentication.Cookies;  //login
using System.Security.Claims;

namespace BoPlats.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        
        [HttpPost]
        public async Task<IActionResult> Index(UserLogIn userInfo, string returnUrl = null)
        {           
           //kontroller användarnamn
            bool userOk = checkUser(userInfo);
            
            if(userOk== true)
            {
                // Allt stämmer, logga in användaren
                var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
                identity.AddClaim(new Claim(ClaimTypes.Name, userInfo.Username));
                
                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme, 
                    new ClaimsPrincipal(identity));

                
                if (returnUrl != null)
                    return Redirect(returnUrl);
                else
                    return RedirectToAction("Index", "Home");
            }

            ViewBag.ErrorMessage = "Log in failed";

            return View();

        }


        private bool checkUser(UserLogIn userInfo)
        {   // ändra t databas ist för hårdkodat

            if(userInfo.Username == "admin" && userInfo.Password == "admin")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<IActionResult> SignOut()
        {

            await HttpContext.SignOutAsync(
                CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }

    }
}
