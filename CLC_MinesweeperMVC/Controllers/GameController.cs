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
            return View("MineSweep",myBoard);
        }
        // GET: Game
        public ActionResult Game() {
            return View("MineSweep");
        }
        [HttpPost]
        public ActionResult Grid_Button_Click() {
            for(int rw = 0; rw<Difficulty*10; rw++) {
                for(int cl = 0; cl<Difficulty*10; cl++) {
                    //if((sender as Button).Equals(grid[rw, cl])) {
                    if(myBoard.grid[rw, cl].Visited) {
                        if(myBoard.grid[rw, cl].live) {
                            //grid[rw, cl].Image=flg;
                            myBoard.grid[rw, cl].visited=true;
                        }
                        else {
                            //grid[rw, cl].Image.Dispose();
                            myBoard.grid[rw, cl].visited=false;
                        }
                    }
                    else {
                        if(myBoard.grid[rw, cl].live) {
                            //grid[rw, cl].Image=bmb;
                            myBoard.ShowAll();
                            //MessageBox.Show("You hit a Mine! Length of play was: "+watch.Elapsed);
                            // Form1 form1 = new Form1();
                            //form1.Show();
                        }
                        else {
                            if(myBoard.grid[rw, cl].liveNeighbors==0&&!myBoard.grid[rw, cl].live) {
                                //myBoard.FloodFill(rw, cl,1);
                                //FloodShow(rw, cl);
                                myBoard.CheckSurround(rw, cl);
                            }
                            myBoard.grid[rw, cl].visited=true;

                        }
                    }
                    // }
                }
            }
            if(myBoard.inPlay) {
                myBoard.ShowAll();
                //MessageBox.Show("You Won! Length of play was: "+watch.Elapsed);
            }
            myBoard.updateButtonLabels();

            //Set Background of clicked button to a different color
            //(sender as Button).BackColor=Color.AliceBlue;
            return PartialView("_BoardPage", myBoard);
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
            return View(myBoard);
        }

        public ActionResult BoardPage() {
            return PartialView(myBoard);
        }
    }
}