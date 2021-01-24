using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CLC_MinesweeperMVC.Models;

namespace CLC_MinesweeperMVC.Controllers{
    public class GameController : Controller{

        BoardModel myBoard = new BoardModel();
        public static int Difficulty = 1;
        private static List<CellModel> buttons = new List<CellModel>();
        public  bool isWon;
        public CellModel[,] btnGrid = new CellModel[Difficulty*10, Difficulty*10];

        // GET: Button
        public ActionResult Index() {
            ViewBag.message=Difficulty;
            /*for(int cl = 0; cl<Difficulty*10; cl++) {
                for(int rw = 0; rw<Difficulty*10; rw++) {
                    buttons.Add(new CellModel());
                }
            }*/
            myBoard.SetupBombs();
            buttons = myBoard.ConvertGridtoList();
            return View("MineSweep", buttons);
        }
        // GET: Game
        public ActionResult Game() {
            return View("MineSweep");
        }

        [HttpPost]
        public ActionResult OnButtonClick(int place) {
            ViewBag.message=Difficulty;
            //int count = -1;
            buttons[place].visited=true;
            int count = 0;
            for(int cl=0; cl<Difficulty*10; cl++) {
                for(int rw = 0; rw<Difficulty*10; rw++) {
                    if(buttons[place].col==cl&&buttons[place].row==rw) {
                        myBoard.grid[rw, cl].visited=true;
                        myBoard.CheckSurround(rw, cl);
                    }
                }
            }
            for(int cl = 0; cl<Difficulty*10; cl++) {
                for(int rw = 0; rw<Difficulty*10; rw++) {
                    if(myBoard.grid[rw, cl].visited&&buttons[count].col==cl-1&&buttons[count].row==rw-1) {
                        buttons[count].visited=true;
                        count++;
                    }
                }
            }
            /*List<CellModel> updateList = new List<CellModel>();
            updateList=myBoard.ConvertGridtoList();
            for(int cels = 0; cels<updateList.Count; cels++) {
                if(updateList[cels].visited) {
                    buttons[cels].visited=true;
                }
           
            }*/
            return View("MineSweep", buttons);
        }

    }
}