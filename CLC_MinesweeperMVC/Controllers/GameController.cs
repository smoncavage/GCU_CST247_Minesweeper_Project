using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CLC_MinesweeperMVC.Models;

namespace CLC_MinesweeperMVC.Controllers{
    public class GameController : Controller{

        BoardModel myBoard;
        public static int Difficulty = 1;
        private static List<BoardModel> buttons = new List<BoardModel>();
        public  bool isWon;
        public ButtonModel[,] btnGrid = new ButtonModel[Difficulty*10, Difficulty*10];

        // GET: Button
        public ActionResult Index() {
            ViewBag.message=Difficulty;
            for(int cl = 0; cl<Difficulty*10; cl++) {
                for(int rw = 0; rw<Difficulty*10; rw++) {
                    buttons.Add(new BoardModel(true));
                }
            }
            //myBoard.populateGrid();
            return View("MineSweep", buttons);
        }
        // GET: Game
        public ActionResult Game() {
            return View("MineSweep");
        }

        [HttpPost]
        public ActionResult OnButtonClick(string mine) {
            ViewBag.message=Difficulty;
            int count = -1;
            for(int cl=0; cl<Difficulty*10; cl++) {
                for(int rw = 0; rw<Difficulty*10; rw++) {
                    count++;
                    if(mine==(1+count).ToString()) {
                        if(buttons[count].btnState) {
                            buttons[count].btnState=false;
                        }
                        else {
                            buttons[count].btnState=true;
                        }
                    }
                }
            }
            return View("MineSweep", buttons);
        }

    }
}