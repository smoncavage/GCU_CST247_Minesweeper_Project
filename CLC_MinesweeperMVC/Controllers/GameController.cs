using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Drawing;
using CLC_MinesweeperMVC.Models;
using System.Web.UI.WebControls;
using System.Diagnostics;
using NLog;

namespace CLC_MinesweeperMVC.Controllers {
    public class GameController:Controller {
        private static NLog.Logger logger = LogManager.GetLogger("myAppLoggerRules");
        public static int Difficulty;
        private static GameModel game;
        public static Stopwatch watch = new Stopwatch();
        private int count=0;
        // GET: Button
        public ActionResult Index(string radioButton) {
            count++;
            logger.Info("Entered Game Controller Index.");
            watch.Start();
            Difficulty=Int32.Parse(radioButton);
            game=new GameModel(Difficulty);
            ViewBag.message=Difficulty;
            game.Initialize(Difficulty);
            if(count<2){
                Index(radioButton);
            }
            return View("_BoardPage", game.myBoard);
        }

        // GET: Game
        public ActionResult Game() {
            return View("_BoardPage");
        }

        [HttpGet]
        public PartialViewResult OnButtonClick(string BoardButtons) {
            logger.Info("Button "+ BoardButtons +" was clicked in Game.");
            game.OnButtonClick(Int32.Parse(BoardButtons));
            ViewBag.ButtonList=game.buttons;
            return PartialView("_BoardPage", game.myBoard);
        }

        public PartialViewResult BoardPage() {
            return PartialView("_BoardPage", game); ;
        }

        public string OpenPopup() {
            logger.Info("Game has ended.");
            watch.Stop();
            if(game.isWon) {
                return "<h2> You Have Won in "+ watch.Elapsed +"! Congratulations!<h2>";
            }
            else {
                return "<h2> You Have Hit a Mine! You Lose.<h2>";
            }
        }

    }
}