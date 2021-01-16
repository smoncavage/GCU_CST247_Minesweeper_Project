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
using HttpPostAttribute = System.Web.Mvc.HttpPostAttribute;

namespace CLC_MinesweeperMVC.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        [HttpGet]
        public ActionResult Login() {
            return View("Login");
        }

        [HttpPost]
        // Check for valid login credentials
        public ActionResult Login(User user) {
            //SecurityService auth = new SecurityService();
            //return auth.Authenticate(user) ? View("LoginPassed") : View("LoginFailed");

            // this action is for handle post (login)
            if(ModelState.IsValid) // this is check validity
            {
                using(MyDBEntities dc = new MyDBEntities()) {
                    var v = dc.Users.Where(a => a.USERNAME.Equals(user.USERNAME)&&a.PASSWORD.Equals(user.PASSWORD)).FirstOrDefault();
                    if(v!=null) {
                        
                        return View("LoginPassed");
                    }
                }
            }
            return View("LoginFailed");
        }
    }
}