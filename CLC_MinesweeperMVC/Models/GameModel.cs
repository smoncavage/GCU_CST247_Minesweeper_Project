using System.Collections.Generic;
using System.Diagnostics;
using System.Web.UI.WebControls;

namespace Recipe_Shop.Models
{
    public class GameModel
    {
        public ImageMap flg = new ImageMap();
        public ImageMap bmb = new ImageMap();
        public static int Difficulty;
        public static int brdSize;
        public List<CellModel> buttons = new List<CellModel>();
        public bool isWon = false;
        public CellModel[,] btnGrid = new CellModel[Difficulty * 7, Difficulty * 7];
        public BoardModel myBoard = new BoardModel(brdSize, Difficulty);
        public static Stopwatch watch = new Stopwatch();

        public GameModel(int difficulty)
        {
            Difficulty = difficulty;
            flg.ImageUrl = "~/Images/checkered_flag.bmp";
            bmb.ImageUrl = "~/Images/bomb.bmp";
            brdSize = Difficulty * 7;
            if (!myBoard.InPlay || myBoard == null)
            {
                myBoard = new BoardModel(brdSize, Difficulty);
            }
            Initialize(difficulty);
        }

        public BoardModel Initialize(int difficulty)
        {
            Difficulty = difficulty;
            flg.ImageUrl = "~/Images/checkered_flag.bmp";
            bmb.ImageUrl = "~/Images/bomb.bmp";
            brdSize = Difficulty * 7;
            if (!myBoard.InPlay)
            {
                myBoard = new BoardModel(brdSize, Difficulty);
            }
            buttons = myBoard.ConvertGridtoList();
            return myBoard;
        }

        public BoardModel OnButtonClick(int BoardButtons)
        {

            if (!myBoard.inPlay)
            {
                myBoard = new BoardModel(brdSize, Difficulty);
            }
            buttons = myBoard.ConvertGridtoList();
            int cnt = BoardButtons;
            for (int rw = 0; rw < brdSize; rw++)
            {
                for (int cl = 0; cl < brdSize; cl++)
                {
                    if (myBoard.grid[rw, cl].countValue == buttons[cnt].countValue)
                    {
                        myBoard.grid[rw, cl].visited = true;
                        if (myBoard.grid[rw, cl].live)
                        {
                            myBoard.grid[rw, cl].Image = bmb;
                            isWon = true;
                            myBoard.inPlay = false;
                            //MessageBox.Show("You hit a Mine! Length of play was: "+watch.Elapsed);
                        }
                        else
                        {
                            if (myBoard.grid[rw, cl].liveNeighbors == 0 && !myBoard.grid[rw, cl].live)
                            {
                                myBoard.grid[rw, cl].Image = flg;
                                isWon = false;
                                watch.Stop();
                                myBoard.CheckSurround(rw, cl);
                            }
                            myBoard.grid[rw, cl].visited = true;
                        }
                    }

                }
            }
            buttons = myBoard.ConvertGridtoList();
            if (isWon)
            {
                watch.Stop();
                myBoard.ShowAll();
                //MessageBox.Show("You Won! Length of play was: "+watch.Elapsed);
            }
            myBoard.UpdateButtonLabels();
            return myBoard;
        }
    }
}