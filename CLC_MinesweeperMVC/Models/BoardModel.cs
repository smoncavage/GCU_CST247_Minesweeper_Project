using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CLC_MinesweeperMVC.Models {
    public class BoardModel {
        public int difficulty;
        public int size;
        public int mine;
        public int[] rwRound = { -1, -1, -1, 0, 1, 1, 1, 0 };
        public int[] clRound = { -1, 0, 1, -1, 1, 0, -1, 1 };
        public bool inPlay;
        public CellModel[,] grid = null;

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

        //Empty Constructor
        public BoardModel() : this(10, 1) { //Set Default Size to 10x10

        }

        //Constructor using Difficulty to set board params
        public BoardModel(int isize = 10, int idifficulty = 1) {
            size=isize;
            difficulty=idifficulty;
            SetupLiveNeighbors();
        }
        public CellModel[,] InitializeGrid() {
            this.grid=new CellModel[this.Size, this.Size];
            for(int iOuter = 0; iOuter<this.Size; iOuter++) {
                for(int jInner = 0; jInner<this.Size; jInner++) {
                    this.grid[iOuter, jInner]=new CellModel();
                }
            }
            return this.grid;
        }
        //Set bomb cells
        public CellModel[,] SetupLiveNeighbors() {
            Random rand = new Random(10);
            int totalsize;
            grid=InitializeGrid(); //*This is Necessary* otherwise will recieve Null Object Exception
            totalsize=this.Size*this.Size;
            mine=totalsize/(8-this.difficulty);
            for(int iRand = mine; iRand>0; iRand--) {
                int randwth = rand.Next(this.Size+1);
                int randlth = rand.Next(this.Size+1);
                //Validate random #'s
                if((randwth>=0)&&(randwth<Size)&&(randlth>=0)&&(randlth<Size)&&!grid[randlth, randwth].live) {
                    grid[randlth, randwth].live=true;
                }
                else {
                    //print only for debugging
                    //this.grid[randwth, randlth].live=false;
                }
            }
            CalculateLiveNeighbors();
            return this.grid;
        }

        //set neighboring bomb cells to value
        public Array CalculateLiveNeighbors() {
            for(int iOuter = 0; iOuter<Size; iOuter++) {
                for(int jInner = 0; jInner<Size; jInner++) {
                    try {
                        this.grid[iOuter, jInner].liveNeighbors=
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
            if((iRow<0)||(iRow>this.Size)||(iCol<0)&&(iCol>this.Size)) {
                count=0;
            }

            try {
                if(this.grid[iRow, iCol].live==true)
                    count++; // Incriment count
            }
            catch {
                // Display error or such
                //Console.Out.WriteLine("Error on \"NON Live\" Cell. ");
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
        public bool IsSquareSafe(int x, int y) {
            if(x>-1&&x<Size&&y>-1&&y<Size)
                return true;
            return false;
        }
    }
}