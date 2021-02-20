using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace CLC_MinesweeperMVC.Models {
    public class BoardModel {
        
        public int difficulty;
        public int size;
        public int mine;
        public int totalsize;

        public int[] rwRound = { -1, -1, -1, 0, 1, 1, 1, 0 };
        public int[] clRound = { -1, 0, 1, -1, 1, 0, -1, 1 };
        public bool inPlay;
        public bool btnState;
        public CellModel[,] grid;
        public List<CellModel> gridList = new List<CellModel>();
        public Stopwatch watch = new Stopwatch();

        //Set/Get Values for Board params
        public int Size {
            get => size;
            set => size=value;
        }

        public int Difficulty {
            get => difficulty;
            set => difficulty=value;
        }
        public int Mine {
            get => mine;
            set => mine=value;
        }

        public bool InPlay {
            get => inPlay;
            set => inPlay=false;
        }
        /*
                //Empty Constructor
                public BoardModel(bool state) : this(10, 1) { //Set Default Size to 10x10

                }
        */
        public BoardModel() {

        }

        //Constructor using Difficulty to set board params
        public BoardModel(int isize, int idifficulty) {
            Size=isize;
            difficulty=idifficulty;
            totalsize=Size*Size;
            SetupBombs();
            inPlay = true;
        }
        public CellModel[,] InitializeGrid() {
            int count=0;
            grid=new CellModel[Size, Size];
            for(int iOuter = 0; iOuter<Size; iOuter++) {
                for(int jInner = 0; jInner<Size; jInner++) {
                    grid[iOuter, jInner]=new CellModel {
                        countValue=count++
                    };
                    //gridList.Add(grid[iOuter, jInner]);
                }
            }
            return grid;
        }
        //Set bomb cells
        public CellModel[,] SetupBombs() {
            Random rand = new Random();
            
            grid=InitializeGrid(); //*This is Necessary* otherwise will recieve Null Object Exception
            //mine=totalsize/(8-difficulty);
            mine=size;
            for(int iRand = mine; iRand>0; iRand--) {
                int randwth = rand.Next(Size+1);
                int randlth = rand.Next(Size+1);
                //Validate random #'s
                if((randwth>=0)&&(randwth<Size)&&(randlth>=0)&&(randlth<Size)&&!grid[randlth, randwth].live) {
                    grid[randlth, randwth].live=true;
                }
                else {
                    //print only for debugging
                    //grid[randwth, randlth].live=false;
                }
            }
            CalculateLiveNeighbors();
            return grid;
        }

        //set neighboring bomb cells to value
        public Array CalculateLiveNeighbors() {
            for(int iOuter = 0; iOuter<Size; iOuter++) {
                for(int jInner = 0; jInner<Size; jInner++) {
                    try {
                        grid[iOuter, jInner].liveNeighbors=
                            LiveNeighbor(iOuter-1, jInner-1)+ //Upper Left Cell
                            LiveNeighbor(iOuter-1, jInner)+   //Left Cell
                            LiveNeighbor(iOuter-1, jInner+1)+ //Lower Left
                            LiveNeighbor(iOuter, jInner-1)+   //LowerCell
                            LiveNeighbor(iOuter+1, jInner+1)+ //Lower Right Cell
                            LiveNeighbor(iOuter+1, jInner)+   //Right Cell
                            LiveNeighbor(iOuter+1, jInner-1)+ //Upper Right Cell
                            LiveNeighbor(iOuter, jInner+1);   //Upper Cell
                    }
                    catch {
                        //Display Error
                        Console.Out.WriteLine("Unexpected Result during Neighbor Sets. ");
                        //MessageBox.Show("Unexpected Result during Neighbor Sets. ");
                    }
                }
            }

            return grid;
        }
        //Count Nieghboring Cells for nearby bombs
        private int LiveNeighbor(int iRow, int iCol) {
            int count = 0;
            // Validate data
            if(!IsSquareSafe(iRow, iCol)) {
                count=0;
            }
            else{
                try {
                    if(grid[iRow, iCol].live==true)
                        count++; // Incriment count
                }
                catch(Exception e) {
                    // Display error or such
                    Console.Out.WriteLine("Error on \"NON Live\" Cell. " + e.Data);
                }
            }
            return count;
        }
        //recursive algorithm to iterate through open cells next to each other 
        public void CheckSurround(int rw, int cl) {
            //int count = 1;
            try {
                if(grid[rw, cl].liveNeighbors==0) {
                    if(IsSquareSafe(rw-1, cl-1)) {
                        if(grid[rw-1, cl-1].live==false&&grid[rw-1, cl-1].visited==false) {
                            grid[rw-1, cl-1].visited=true;
                            CheckSurround(rw-1, cl-1);
                        }
                    }
                    if(IsSquareSafe(rw-1, cl)) {
                        if(grid[rw-1, cl].live==false&&grid[rw-1, cl].visited==false) {
                            grid[rw-1, cl].visited=true;
                            CheckSurround(rw-1, cl);
                        }
                    }
                    if(IsSquareSafe(rw-1, cl+1)) {
                        if(grid[rw-1, cl+1].live==false&&grid[rw-1, cl+1].visited==false) {
                            grid[rw-1, cl+1].visited=true;
                            CheckSurround(rw-1, cl+1);
                        }
                    }
                    if(IsSquareSafe(rw, cl+1)) {
                        if(grid[rw, cl+1].live==false&&grid[rw, cl+1].visited==false) {
                            grid[rw, cl+1].visited=true;
                            CheckSurround(rw, cl+1);
                        }
                    }
                    if(IsSquareSafe(rw+1, cl+1)) {
                        if(grid[rw+1, cl+1].live==false&&grid[rw+1, cl+1].visited==false) {
                            grid[rw+1, cl+1].visited=true;
                            CheckSurround(rw+1, cl+1);
                        }
                    }
                    if(IsSquareSafe(rw+1, cl)) {
                        if(grid[rw+1, cl].live==false&&grid[rw+1, cl].visited==false) {
                            grid[rw+1, cl].visited=true;
                            CheckSurround(rw+1, cl);
                        }
                    }
                    if(IsSquareSafe(rw+1, cl-1)) {
                        if(grid[rw+1, cl-1].live==false&&grid[rw+1, cl-1].visited==false) {
                            grid[rw+1, cl-1].visited=true;
                            CheckSurround(rw+1, cl-1);
                        }
                    }
                    if(IsSquareSafe(rw, cl-1)) {
                        if(grid[rw, cl-1].live==false&&grid[rw, cl-1].visited==false) {
                            grid[rw, cl-1].visited=true;
                            CheckSurround(rw, cl-1);
                        }
                    }
                }
            }
            catch(InsufficientExecutionStackException) {
                Console.WriteLine("Ran out of stack Memory 3.");
            }
        }

        //Check if Cell is within the Grid
        public bool IsSquareSafe(int rw, int cl) {
            if(rw>-1&&rw<Size&&cl>-1&&cl<Size)
                return true;
            return false;
        }

        public void UpdateButtonLabels() {
            int count = 0;
            int mines = 0;
            for(int rw = 0; rw<Size; rw++) {
                for(int cl = 0; cl<Size; cl++) {
                    if(grid[rw, cl].visited==true) {
                        count++;
                        if(grid[rw, cl].liveNeighbors!=0) {//&&btnGrid[rw, cl].Image!=flg) {
                            grid[rw, cl].liveNeighbors.ToString();
                        }
                    }
                    if(grid[rw, cl].live) {
                        mines++;
                    }
                }
            }
            if(count==((Size)*(Size))) {
                inPlay=false;
                //MessageBox.Show("You Won! Length of play was: "+watch.Elapsed);
            }
            else if(count==(((Size)*(Size))-mines)) {
                for(int rw = 0; rw<Size; rw++) {
                    for(int cl = 0; cl<Size; cl++) {
                        if(grid[rw, cl].live) {
                            grid[rw, cl].visited=true;
                            //btnGrid[rw, cl].Image=flg;
                            inPlay=false;
                        }
                    }
                }
            }
            else {
                inPlay=true;
            }
            if(!inPlay) {
                ShowAll();
                //MessageBox.Show("You Won! Length of play was: "+watch.Elapsed);
            }
        }
        public void Grid_Button_Click(object sender, EventArgs e) {

            for(int rw = 0; rw<Size; rw++) {
                for(int cl = 0; cl<Size; cl++) {
                    if((sender as Button).Equals(grid[rw, cl])) {
                        if(e.ToString()!=null) {
                            if(grid[rw, cl].Image==null) { //Should be grid[rw,cl].Image==null
                                ///grid[rw, cl].Image=flg;
                                //grid[rw, cl].Enabled=true;
                                grid[rw, cl].visited=true;
                            }
                            else {
                                //grid[rw, cl].Image.Dispose();
                                grid[rw, cl].visited=false;
                            }
                        }
                        else {
                            if(grid[rw, cl].live) {
                                //watch.Stop();
                                ShowAll();
                                //MessageBox.Show("You hit a Mine! Length of play was: "+watch.Elapsed);
                                // Form1 form1 = new Form1();
                                //form1.Show();
                            }
                            else {
                                if(grid[rw, cl].liveNeighbors==0&&!grid[rw, cl].live) {
                                    //myBoard.FloodFill(rw, cl,1);
                                    //FloodShow(rw, cl);
                                    CheckSurround(rw, cl);
                                }
                                grid[rw, cl].visited=true;
                                (sender as Button).Text=grid[rw, cl].liveNeighbors.ToString();
                            }
                        }
                    }
                }
            }
            if(!inPlay) {
                //watch.Stop();
                ShowAll();
                //MessageBox.Show("You Won! Length of play was: "+watch.Elapsed);
                //MessageBox.Show("You Won! Length of play was: "+watch.Elapsed);
                // Form1 form1 = new Form1();
                //form1.Show();
            }
            //updateButtonLabels();
            //ButtonPanel.Update();
            //Set Background of clicked button to a different color
            (sender as Button).BackColor=Color.Red;

        }

        
        public void ShowAll() {
            for(int rw = 0; rw<Size; rw++) {
                for(int cl = 0; cl<Size; cl++) {
                    if(grid[rw, cl].live) {//&&grid[rw, cl].Image!=flg) {
                        //btnGrid[rw, cl].Image=bmb;
                    }
                    if(grid[rw, cl].liveNeighbors>0) {
                        grid[rw, cl].liveNeighbors.ToString();
                    }
                }
            }
        }

        public List<CellModel> ConvertGridtoList() {
            foreach(CellModel cell in grid) {
                cell.countValue=gridList.Count();
                gridList.Add(cell);
            }
            return gridList;
        }
    }
}