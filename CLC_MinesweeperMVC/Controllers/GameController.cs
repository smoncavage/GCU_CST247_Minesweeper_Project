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
        public ImageMap flg = new ImageMap();
        public ImageMap bmb = new ImageMap();
        public static int Difficulty = 1;
        public static int brdSize = 7;
        private static List<CellModel> buttons = new List<CellModel>();
        public bool isWon = false;
        public CellModel[,] btnGrid = new CellModel[Difficulty*7, Difficulty*7];
        BoardModel myBoard = new BoardModel(brdSize, Difficulty);

        // GET: Button
        public ActionResult Index(string radioButton) {
            flg.ImageUrl="~/Images/checkered_flag.bmp";
            bmb.ImageUrl="~/Images/bomb.bmp";
            Difficulty=Int32.Parse(radioButton);
            ViewBag.message=Difficulty;
            brdSize=Difficulty*7;
            myBoard=new BoardModel(brdSize, Difficulty);
            buttons=myBoard.ConvertGridtoList();
            ViewBag.ButtonList=buttons;
            return PartialView("_BoardPage", myBoard);
        }

        // GET: Game
        public ActionResult Game() {
            return View("_BoardPage");
        }

        [HttpPost]
        public PartialViewResult OnButtonClick(string BoardButtons) {
            //var buttonValue = ButtonName.;
            int cnt = int.Parse(BoardButtons);
            ViewBag.message=Difficulty;
            for(int rw = 0; rw<brdSize; rw++) {
                for(int cl = 0; cl<brdSize; cl++) {
                    //if((sender as Button).Equals(grid[rw, cl])) {
                    if(myBoard.grid[rw, cl].countValue==buttons[cnt].countValue) {

                        if(myBoard.grid[rw, cl].live) {
                            myBoard.grid[rw, cl].Image=bmb;
                            isWon=true;
                            //MessageBox.Show("You hit a Mine! Length of play was: "+watch.Elapsed);
                        }
                        else {
                            if(myBoard.grid[rw, cl].liveNeighbors==0&&!myBoard.grid[rw, cl].live) {
                                isWon=false;
                                //myBoard.FloodFill(rw, cl,1);
                                //FloodShow(rw, cl);
                                myBoard.CheckSurround(rw, cl);
                            }
                            myBoard.grid[rw, cl].visited=true;
                        }
                    }

                }
            }
            buttons=myBoard.ConvertGridtoList();
            ViewBag.ButtonList=buttons;
            if(isWon) {
                myBoard.ShowAll();
                //MessageBox.Show("You Won! Length of play was: "+watch.Elapsed);
            }
            myBoard.UpdateButtonLabels();
            //(sender as Button).BackColor=Color.AliceBlue;
            return PartialView("_BoardPage", myBoard);
        }


        public ActionResult BoardPage() {
            return View(myBoard);
        }



    }
}