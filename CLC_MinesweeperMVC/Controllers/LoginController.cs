using CLC_MinesweeperMVC.Models;
using CLC_MinesweeperMVC.Services.Business;
using Microsoft.AspNetCore.Mvc;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using ActionResult = System.Web.Mvc.ActionResult;
using Controller = System.Web.Mvc.Controller;
using HttpGetAttribute = System.Web.Mvc.HttpGetAttribute;
using HttpPostAttribute = System.Web.Mvc.HttpPostAttribute;

namespace CLC_MinesweeperMVC.Controllers
{
    public class LoginController : Controller
    {
        // Logger decleration 
        private static Logger logger = LogManager.GetLogger("myAppLoggerRules");

        // GET: Login
        [HttpGet]
        public ActionResult Login()
        {
            return View("~/Views/Home/Login.cshtml");
        }

        [HttpPost]
        // Check for valid login credentials
        public ActionResult Login(User user)
        {
            // this action is for handling post (login)
            if (ModelState.IsValid) // this is check validity
            {
                using (MyDBEntities dc = new MyDBEntities())
                {
                    var v = dc.Users.Where(a => a.USERNAME.Equals(user.USERNAME) && a.PASSWORD.Equals(user.PASSWORD)).FirstOrDefault();
                    if (v != null)
                    {
                        Session["Id"] = v.Id.ToString();
                        Session["USERNAME"] = v.USERNAME.ToString();
                        return RedirectToAction("Dashboard");
                    }
                }
            }
            return View("~/Views/Login/LoginFailed.cshtml");
        }

        public ActionResult Dashboard()
        {
            if (Session["Id"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("~/Views/Home/Login.cshtml");
            }
        }

        /*
        // Handeler for Logging in to website
        public ActionResult Login(User model)
        {

            logger.Info("Entering LoginController.Login()"); // Logs 

            try
            {
                // Validate the Form POST
                if (!ModelState.IsValid)
                    return View("~/Views/Home/Login.cshtml");

                // Call Security Service for authentication 
                SecurityService auth = new SecurityService();
                bool result = auth.Authenticate(model); // Run authentication return true or false
                logger.Info("Parameters are:" + new JavaScriptSerializer().Serialize(model)); // Logs
                
                // If result is a valid user return the home page with model user else return Login page
                if (result)
                {
                    logger.Info("Exit LoginController.Login() with Login passing"); // Logs
                    return View("~/Views/Login/LoginPassed.cshtml", model);
                }
                else
                {
                    logger.Info("Exit LoginController.Login() with Login failing");
                    return View("~/Views/Login/LoginFailed.cshtml");
                }
            }
            catch (Exception e)
            {
                logger.Error("Exception LoginController.Login()" + e.Message);
                return View("~/Views/Login/LoginFailed.cshtml");
            }
        }
        */


    }
}