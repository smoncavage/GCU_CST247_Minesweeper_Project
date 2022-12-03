﻿namespace Recipe_Shop.Models
{
    public class CellModel
    {
        public int row = -1;
        public int col = -1;
        public bool visited = false;
        public int liveNeighbors = 0;
        public bool live = false;
        public bool isFlagged { get; set; }
        public int countValue { get; set; }

        public CellModel()
        {

        }

        public CellModel(int _row, int _col, bool _visited, int _liveNeighbors, bool live, int cntVal)
        {

        }

        public int Row
        {
            get => row;
            set => row = value;
        }

        public int Col
        {
            get => col;
            set => col = value;
        }

        public bool Visited
        {
            get => visited;
            set => visited = value;
        }

        public int LiveNeighbors
        {
            get => liveNeighbors;
            set => liveNeighbors = value;
        }

        public bool Live
        {
            get => live;
            set => live = true;
        }

        public object Image { get; internal set; }
    }
}