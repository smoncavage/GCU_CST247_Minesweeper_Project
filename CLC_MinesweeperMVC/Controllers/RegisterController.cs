using CLC_MinesweeperMVC.Models;
using CLC_MinesweeperMVC.Services.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CLC_MinesweeperMVC.Controllers{
    public class RegisterController : Controller{
        // GET: Register
        public ActionResult Register(){
            return View();
        }

        public ActionResult Registration(UserModel user) {
            SecurityService auth = new SecurityService();

            bool result = auth.Authenticate(user);

            if(result) {
                return View("RegistrationPassed");
            }
            else {
                return View("RegistrationFailed");
            }
        }
    }
}