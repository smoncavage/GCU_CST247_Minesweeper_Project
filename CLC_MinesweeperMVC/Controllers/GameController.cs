using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CLC_MinesweeperMVC.Models;

namespace CLC_MinesweeperMVC.Controllers{
    public class GameController : Controller{

        static BoardModel myBoard;
        public static int Difficulty = 1;
        static List<ButtonModel> buttons = new List<ButtonModel>();
        public  bool isWon;
        public ButtonModel[,] btnGrid = new ButtonModel[Difficulty*10, Difficulty*10];

        // GET: Button
        public ActionResult Index() {
            ViewBag.message=Difficulty;
            buttons.Add(new ButtonModel(true));
            buttons.Add(new ButtonModel(true));
            myBoard.populateGrid();
            return View("MineSweep", buttons);
        }
        // GET: Game
        public ActionResult Game() {
            return View("MineSweep");
        }

        [HttpPost]
        public ActionResult OnButtonClick(string mine) {
            ViewBag.message=Difficulty;

            if(mine=="1") {
                if(buttons[0].State) {
                    buttons[0].State=false;
                }
                else {
                    buttons[0].State=true;
                }
            }
            if(mine=="2") {
                if(buttons[1].State) {
                    buttons[1].State=false;
                }
                else {
                    buttons[1].State=true;
                }
            }
            return View("MineSweep", buttons);
        }

    }
}