using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CLC_MinesweeperMVC.Controllers
{
    public class SettingsController : Controller
    {
        // GET: Difficulty
        public ActionResult Index()
        {
            return PartialView("_Difficulty");
        }

        [HttpPost]
        // Difficulty paramater being passed to index()
        public ActionResult OnSelectDifficulty(string radioButton)
        {
            int Difficulty = Int32.Parse(radioButton);
            GameController.Difficulty = Difficulty;

            var controller = DependencyResolver.Current.GetService<GameController>();
            controller.ControllerContext = new ControllerContext(this.Request.RequestContext, controller);
            
            return PartialView("~/Views/Game/_BoardPage.cshtml");
        }
    }
}