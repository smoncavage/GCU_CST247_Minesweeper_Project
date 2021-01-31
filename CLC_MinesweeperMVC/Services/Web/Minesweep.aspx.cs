using CLC_MinesweeperMVC.Models;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CLC_MinesweeperMVC.Services.Web {
    public partial class WebForm2:System.Web.UI.Page{
        public static Stopwatch watch = new Stopwatch();
        static BoardModel myBoard;
        static bool isWon;
        public static int Difficulty = 1;
        public Button[,] btnGrid = new Button[Difficulty*10, Difficulty*10];
        public List<CellModel> buttons = new List<CellModel>();
        public int brdSize;
        

        protected void Page_Load(object sender, EventArgs e) {
            isWon=false;
            PopulateGrid();
            watch.Start();
        }

        public void SetDifficulty(int diff) {
            Difficulty=diff;
        }

        public void PopulateGrid() {
            //Fill the Panel with Buttons And Create Game Board to Match
            int buttonSize = 50;
            //ButtonPanel.Width=buttonSize*(Difficulty*10);
            //ButtonPanel.Height=ButtonPanel.Width;
            Button[,] butnGrid = new Button[Difficulty*10, Difficulty*10];
            btnGrid=butnGrid;
            myBoard=new BoardModel(Difficulty*10, Difficulty);
            myBoard.InitializeGrid();
            myBoard.SetupBombs();
            myBoard.CalculateLiveNeighbors();
            int count = 0;
            for(int rw = 0; rw<Difficulty*10; rw++) {
                for(int cl = 0; cl<Difficulty*10; cl++) {
                    btnGrid[rw, cl]=new Button {

                        //make them square
                        Width=buttonSize,
                        Height=buttonSize,
                    };
                    btnGrid[rw, cl].ID=(count++).ToString();
                    btnGrid[rw, cl].Click+=Grid_Button_Click; //Same click event for each button
                    ButtonPanel.ContentTemplateContainer.Controls.Add(btnGrid[rw, cl]);
                    PlaceHolder1.Controls.Add(btnGrid[rw, cl]);
                    //btnGrid[rw, cl].Location=new Point(buttonSize*rw, buttonSize*cl);
                }
            }
        }

        public void UpdateButtonLabels() {
            int count = 0;
            int mines = 0;
            for(int cl = 0; cl<Difficulty*10; cl++) {
                for(int rw = 0; rw<Difficulty*10; rw++) {
                    if(myBoard.grid[rw, cl].visited==true) {
                        count++;
                        if(myBoard.grid[rw, cl].liveNeighbors!=0) {//&&btnGrid[rw, cl].Image!=flg) {
                            btnGrid[rw, cl].Text=myBoard.grid[rw, cl].liveNeighbors.ToString();
                        }
                        btnGrid[rw, cl].BackColor=Color.Red;
                        btnGrid[rw, cl].Enabled=false;
                    }
                    if(myBoard.grid[rw, cl].live) {
                        mines++;
                    }
                }
            }
            if(count==((Difficulty*10)*(Difficulty*10))) {
                isWon=true;
                //MessageBox.Show("You Won! Length of play was: "+watch.Elapsed);
            }
            if(count==(((Difficulty*10)*(Difficulty*10))-mines)) {
                for(int cl = 0; cl<Difficulty*10; cl++) {
                    for(int rw = 0; rw<Difficulty*10; rw++) {
                        if(myBoard.grid[rw, cl].live) {
                            myBoard.grid[rw, cl].visited=true;
                            //btnGrid[rw, cl].Image=flg;
                            isWon=true;
                        }
                    }
                }
            }
            if(isWon) {
                watch.Stop();
                ShowAll();
                //MessageBox.Show("You Won! Length of play was: "+watch.Elapsed);
                //MessageBox.Show("You Won! Length of play was: "+watch.Elapsed);
                // Form1 form1 = new Form1();
                //form1.Show();
            }
        }

        public void Grid_Button_Click(object sender, EventArgs e) {
           
            for(int rw = 0; rw<Difficulty*10; rw++) {
                for(int cl = 0; cl<Difficulty*10; cl++) {
                    if((sender as Button).Equals(btnGrid[rw, cl])) {
                        if(e.ToString()!=null) {
                            if(btnGrid[rw, cl]==null) { //Should be btnGrid[rw,cl].Image==null
                                ///btnGrid[rw, cl].Image=flg;
                                btnGrid[rw, cl].Enabled=true;
                                myBoard.grid[rw, cl].visited=true;
                            }
                            else {
                                //btnGrid[rw, cl].Image.Dispose();
                                myBoard.grid[rw, cl].visited=false;
                            }
                        }
                        else {
                            if(myBoard.grid[rw, cl].live) {
                                //btnGrid[rw, cl].Image=bmb;
                                watch.Stop();
                                ShowAll();
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
                    }
                }
            }
            if(isWon) {
                watch.Stop();
                ShowAll();
                //MessageBox.Show("You Won! Length of play was: "+watch.Elapsed);
                //MessageBox.Show("You Won! Length of play was: "+watch.Elapsed);
                // Form1 form1 = new Form1();
                //form1.Show();
            }
            UpdateButtonLabels();
            //ButtonPanel.Update();
            //Set Background of clicked button to a different color
            (sender as Button).BackColor=Color.DarkGray;

        }
        private void ShowAll() {
            for(int cl = 0; cl<Difficulty*10; cl++) {
                for(int rw = 0; rw<Difficulty*10; rw++) {
                    if(myBoard.grid[rw, cl].live) {//&&btnGrid[rw, cl].Image!=flg) {
                        //btnGrid[rw, cl].Image=bmb;
                    }
                    if(myBoard.grid[rw, cl].liveNeighbors>0) {
                        btnGrid[rw, cl].Text=myBoard.grid[rw, cl].liveNeighbors.ToString();
                    }
                }
            }
        }
    }
}