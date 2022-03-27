using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Recipe_Shop.Controllers {
    public class DashboardController:Controller {
        // GET: Dashboard
        public ActionResult Index() {
            return View();
        }
    }
}