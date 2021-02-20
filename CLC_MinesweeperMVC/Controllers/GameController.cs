using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Drawing;
using CLC_MinesweeperMVC.Models;
using System.Web.UI.WebControls;

namespace CLC_MinesweeperMVC.Controllers {
    public class GameController:Controller {
        public static int Difficulty = 1;
        private GameModel game = new GameModel();
        // GET: Button
        public ActionResult Index(string radioButton) {
            Difficulty=Int32.Parse(radioButton);
            ViewBag.message=Difficulty;
            game.Initialize(Difficulty);
            return PartialView("_BoardPage", game.myBoard);
        }

        // GET: Game
        public ActionResult Game() {
            return View("_BoardPage");
        }

        [HttpPost]
        public PartialViewResult OnButtonClick(string BoardButtons) {
            game.OnButtonClick(BoardButtons);
            ViewBag.ButtonList=game.buttons;
            return PartialView("_BoardPage", game.myBoard);
        }

        public PartialViewResult BoardPage() {
            return PartialView("_BoardPage", game); ;
        }



    }
}