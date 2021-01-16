using CLC_MinesweeperMVC.Models;
using CLC_MinesweeperMVC.Services.Business;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ActionResult = System.Web.Mvc.ActionResult;
using Controller = System.Web.Mvc.Controller;
using HttpGetAttribute = System.Web.Mvc.HttpGetAttribute;

namespace CLC_MinesweeperMVC.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        [HttpGet]
        public ActionResult Login() {
            return View("Login");
        }

        // Check for valid login credentials
        public ActionResult Login(UserModel user) {
            SecurityService auth = new SecurityService();

            bool result = auth.Authenticate(user);

            if(result) {
                return View("LoginPassed");
            }
            else {
                return View("LoginFailed");
            }
        }
    }
}