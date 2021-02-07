using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Drawing;
using CLC_MinesweeperMVC.Models;
using System.Web.UI.WebControls;

namespace CLC_MinesweeperMVC.Controllers{
    public class GameController : Controller{
        Bitmap flg = new Bitmap("C:\\Git_Reps\\GCU_CST247_CLC_Project\\CLC_MinesweeperMVC\\Images\\checkered_flag.bmp");
        Bitmap bmb = new Bitmap("C:\\Git_Reps\\GCU_CST247_CLC_Project\\CLC_MinesweeperMVC\\Images\\bomb.bmp");
        BoardModel myBoard = new BoardModel();
        public static int Difficulty = 1;
        private static List<CellModel> buttons = new List<CellModel>();
        public  bool isWon;
        public CellModel[,] btnGrid = new CellModel[Difficulty*10, Difficulty*10];

        // GET: Button
        public ActionResult Index() {
            ViewBag.message=Difficulty;
            myBoard.SetupBombs();
            buttons = myBoard.ConvertGridtoList();
            return PartialView("_BoardPage",myBoard);
        }
        // GET: Game
        public ActionResult Game() {
            return PartialView("_BoardPage");
        }
        
        [HttpPost]
        public ActionResult OnButtonClick(string BoardButtons) {
            //var buttonValue = ButtonName.;
            int cnt = int.Parse(BoardButtons);
            ViewBag.message=Difficulty;
            
            for(int rw = 0; rw<Difficulty*10; rw++) {
                for(int cl = 0; cl<Difficulty*10; cl++) {
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
            if(isWon) {
                myBoard.ShowAll();
                //MessageBox.Show("You Won! Length of play was: "+watch.Elapsed);
            }
            myBoard.updateButtonLabels();
            //(sender as Button).BackColor=Color.AliceBlue;
            return PartialView("_BoardPage", myBoard);
        }

        public ActionResult BoardPage() {
            return PartialView(myBoard);
        }
    }
}