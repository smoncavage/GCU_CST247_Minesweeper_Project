using CLC_MineSweeper.Models;
using CLC_MineSweeper.Services.Business;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CLC_MineSweeper.Controllers {
    public class LoginController:Controller 
    {    
        // GET: Login
        [HttpGet]
        public IActionResult Login() 
        {
            return View("Login");
        }

        // Check for valid login credentials
        public ActionResult Login(UserModel user)
        {
            SecurityService auth = new SecurityService();

            bool result = auth.Authenticate(user);

            if (result)
            {
                return View("LoginPassed");
            }
            else
            {
                return View("LoginFailed");
            }
        }
    }
}
