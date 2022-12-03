using NLog;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using ActionResult = System.Web.Mvc.ActionResult;
using Controller = System.Web.Mvc.Controller;
using HttpGetAttribute = System.Web.Mvc.HttpGetAttribute;
using HttpPostAttribute = System.Web.Mvc.HttpPostAttribute;
using ValidateAntiForgeryTokenAttribute = System.Web.Mvc.ValidateAntiForgeryTokenAttribute;

namespace Recipe_Shop.Controllers
{

    public class LoginController : Controller
    {
        // Logger decleration 
        private static Logger logger = LogManager.GetLogger("myAppLoggerRules");

        // GET: Login
        [AllowAnonymous]
        [HttpGet]
        public ActionResult Login()
        {
            return View("~/Views/Login/Login.cshtml");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        // Check for valid login credentials
        public ActionResult Login(User user)
        {
            // this action is for handling post (login)
            if (ModelState.IsValid)
            { // this is check validity
                using (MyDBEntities dc = new MyDBEntities())
                {
                    var v = dc.Users.Where(a => a.USERNAME.Equals(user.USERNAME) && a.PASSWORD.Equals(user.PASSWORD)).FirstOrDefault();
                    if (v != null)
                    {
                        FormsAuthentication.SetAuthCookie(v.USERNAME, false);
                        Session["Id"] = v.Id.ToString();
                        Session["USERNAME"] = v.USERNAME.ToString();
                        return RedirectToAction("Dashboard");
                    }
                }
            }
            logger.Info(user + " has logged in.");
            ModelState.AddModelError("", "Invalid Username or Password");
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
                logger.Info("Attempted protected page entry. Re-routed user.");
                return RedirectToAction("~/Views/Login/Login.cshtml");
            }
        }

        [AllowAnonymous]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View("~/Views/Home/About.cshtml");
        }
        [AllowAnonymous]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View("~/Views/Home/Contact.cshtml");
        }
        public ActionResult Index()
        {
            return View("~/Views/Home/Index.cshtml");
        }
        [AllowAnonymous]
        public ActionResult Registration()
        {
            return View("~/Views/User/Registration.cshtml");
        }
    }
}