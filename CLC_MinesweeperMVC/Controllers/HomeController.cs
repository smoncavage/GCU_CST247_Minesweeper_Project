using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Recipe_Shop.Controllers {
    [Authorize]
    public class HomeController:Controller {

        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new AuthorizeAttribute());
        }
        [AllowAnonymous]
        public ActionResult Index() {
            return View();
        }
        [AllowAnonymous]
        public ActionResult Login() {
            return View("~/Views/Login/Login.cshtml");
        }
        [AllowAnonymous]
        public ActionResult About() {
            ViewBag.Message="Your application description page.";

            return View();
        }
        [AllowAnonymous]
        public ActionResult Contact() {
            ViewBag.Message="Your contact page.";

            return View();
        }
        [AllowAnonymous]
        public ActionResult Registration()
        {
            return View("~/Views/User/Registration.cshtml");
        }
    }
}