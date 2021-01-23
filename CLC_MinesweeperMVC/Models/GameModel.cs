using AngleSharp.Html.Dom.Events;
using EO.Base.UI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace CLC_MinesweeperMVC.Models {
    public class GameModel {
        System.Drawing.Image flg = System.Drawing.Image.FromFile(@"C:\Git_Reps\GCU_CST247_CLC_Project\CLC_MinesweeperMVC\Images\bomb.bmp");
        System.Drawing.Image bmb = System.Drawing.Image.FromFile(@"C:\Git_Reps\GCU_CST247_CLC_Project\CLC_MinesweeperMVC\Images\checkered_flag.bmp");
        public static Stopwatch watch = new Stopwatch();
        static BoardModel myBoard;
        static bool isWon;
        public static int Difficulty = 1;
        public Button[,] btnGrid = new Button[Difficulty*10, Difficulty*10];
        public GameModel(int diff) {
            //InitializeComponent();
            isWon=false;
            Difficulty=diff;
            populateGrid();
            watch.Start();
        }

        public void populateGrid() {
            //Fill the Panel with Buttons And Create Game Board to Match
            int buttonSize = 32;
           // panel1.Width=buttonSize*(Difficulty*10);
           // panel1.Height=panel1.Width;
            Button[,] butnGrid = new Button[Difficulty*10, Difficulty*10];
            btnGrid=butnGrid;
            myBoard=new BoardModel(Difficulty*10, Difficulty);
            myBoard.InitializeGrid();
            myBoard.SetupLiveNeighbors();
            myBoard.CalculateLiveNeighbors();
            for(int rw = 0; rw<Difficulty*10; rw++) {
                for(int cl = 0; cl<Difficulty*10; cl++) {
                    btnGrid[rw, cl]=new Button();

                    //make them square
                    btnGrid[rw, cl].Width=buttonSize;
                    btnGrid[rw, cl].Height=buttonSize;

              //      btnGrid[rw, cl].MouseUp+=Grid_Button_Click; //Same click event for each button
              //     panel1.Controls.Add(btnGrid[rw, cl]);
              //      btnGrid[rw, cl].Location=new Point(buttonSize*rw, buttonSize*cl);

                }
            }
        }

        public void updateButtonLabels() {
            int count = 0;
            int mines = 0;
            for(int cl = 0; cl<Difficulty*10; cl++) {
                for(int rw = 0; rw<Difficulty*10; rw++) {
                    if(myBoard.grid[rw, cl].visited==true) {
                        count++;
                        if(myBoard.grid[rw, cl].liveNeighbors!=0){//&&btnGrid[rw, cl].Image!=flg) {
                            btnGrid[rw, cl].Text=myBoard.grid[rw, cl].liveNeighbors.ToString();
                        }
                        btnGrid[rw, cl].BackColor=Color.AliceBlue;
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

        public void Grid_Button_Click(object sender, MouseEventArgs e) {
            for(int rw = 0; rw<Difficulty*10; rw++) {
                for(int cl = 0; cl<Difficulty*10; cl++) {
                    if((sender as Button).Equals(btnGrid[rw, cl])) {
                        if(e.Button==EO.Base.UI.MouseButtons.Right) {
                            if(btnGrid[rw, cl].ID!=null) {
                                //btnGrid[rw, cl].Image=flg;
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
            updateButtonLabels();

            //Set Background of clicked button to a different color
            (sender as Button).BackColor=Color.AliceBlue;

        }
        private void ShowAll() {
            for(int cl = 0; cl<Difficulty*10; cl++) {
                for(int rw = 0; rw<Difficulty*10; rw++) {
                    if(myBoard.grid[rw, cl].live){//&&btnGrid[rw, cl].Image!=flg) {
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