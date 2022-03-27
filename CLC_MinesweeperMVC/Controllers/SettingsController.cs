using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Recipe_Shop.Controllers{
    public class SettingsController : Controller{
        private static NLog.Logger logger = LogManager.GetLogger("myAppLoggerRules");

        // GET: Difficulty
        public ActionResult Index()
        {
            return PartialView("_Difficulty");
        }

        [HttpPost]
        // Difficulty paramater being passed to index()
        public ActionResult OnSelectDifficulty(string radioButton){
            logger.Info("User has entered the Difficulty page.");
            int Difficulty = Int32.Parse(radioButton);
            GameController.Difficulty = Difficulty;
            logger.Info("User has selected level " + Difficulty +" difficulty.");
            var controller = DependencyResolver.Current.GetService<GameController>();
            controller.ControllerContext = new ControllerContext(this.Request.RequestContext, controller);
            
            return PartialView("~/Views/Game/_BoardPage.cshtml");
        }
    }
}