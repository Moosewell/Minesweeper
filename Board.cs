using System;
using System.Collections.Generic;

namespace Minesweeper
{
    struct Board
    {
        public Tile[,] matrix;

        public void CreateBoard()
        {
            matrix = new Tile[10,10];
            for (int row = 0; row < 10; ++row)
            {
                for (int col = 0; col < 10; ++col)
                {
                    matrix[row, col].symbol = "X";
                }
            }
        }
        
        public void DrawBoard()
        {
            Console.WriteLine("    A B C D E F G H I J\n  +--------------------");
            for(var col = 0; col < matrix.GetLength(1); col++)
            {
                Console.Write(col + " |");
                for(var row = 0; row < matrix.GetLength(0); row++)
                {
                    Console.Write(" " + matrix[row,col].symbol);
                }
                Console.WriteLine();
            }
            Console.Write("\n");
        }

        public void GameEndScreen()
        {
            for(var col = 0; col < matrix.GetLength(1); col++)
            {
                for(var row = 0; row < matrix.GetLength(0); row++)
                {
                    matrix[row,col].symbol = ".";
                    if(matrix[row,col].neighbouringMines > 0)
                    {
                        matrix[row,col].symbol = matrix[row,col].neighbouringMines.ToString();
                    }
                    if(matrix[row,col].mine)
                    {
                        matrix[row,col].symbol = "m";
                    }
                    if(matrix[row,col].mine && matrix[row,col].flagged)
                    {
                        matrix[row,col].symbol = "ɯ";
                    }
                    if(!matrix[row,col].mine && matrix[row,col].flagged)
                    {
                        matrix[row,col].symbol = "Ⅎ";
                    }
                }
            }
            matrix[Commands.LatestInputRow, Commands.LatestInputCol].symbol = "M";
        }
        public void PlaceMines()
        {
            for(var col = 0; col < matrix.GetLength(1); col++)
            {
                for(var row = 0; row < matrix.GetLength(0); row++)
                {
                    matrix[row,col].mine = Helper.BoobyTrapped(row,col);
                }
            }
        }
        public void StoreMines()
        {
            for(var col = 0; col < matrix.GetLength(1); col++)
            {
                for(var row = 0; row < matrix.GetLength(0); row++)
                {
                    GatherMines(row,col);
                }
            }
        }
        public void GatherMines(int row, int col)
        {
            for(var i = col -1; i <= col + 1; i++)
            {
                for(var j = row -1; j <= row +1; j++)
                {
                    if (!(j == row && i == col))
                    {
                        if(j >= 0 && j <= 9 && i >= 0 && i <= 9)
                        {
                            if(matrix[j,i].mine == true)
                            {
                                matrix[row,col].neighbouringMines++;
                            }
                        }
                    }
                }
            }
        }
        public void SweepNeighbours(int row, int col)
        {
            for(var i = col -1; i <= col + 1; i++)
            {
                for(var j = row -1; j <= row +1; j++)
                {
                    if (!(j == row && i == col))
                    {
                        if(j >= 0 && j <= 9 && i >= 0 && i <= 9)
                        {
                            if(!matrix[j,i].mine)
                            {
                                matrix[j,i].sweepedByNeighbour = true;
                                matrix[j,i] = Commands.Sweep(matrix[j,i]);
                                if(matrix[j,i].neighbouringMines == 0 && !matrix[j,i].hasSweepedNeighbours)
                                {
                                    matrix[j,i].hasSweepedNeighbours = true;
                                    SweepNeighbours(j,i);
                                }
                            }
                        }
                    }
                }
            }
        }
        public void VictoryCheck()
        {
            int sweepedAmount = 0;
            for(var col = 0; col < Program.board.matrix.GetLength(1); col++)
            {
                for(var row = 0; row < Program.board.matrix.GetLength(0); row++)
                {
                    if(Program.board.matrix[row,col].sweeped)
                    {
                        sweepedAmount++;
                    }
                }
            }
            if(sweepedAmount >= 90)
            {
                Program.GameEnd(0);
            }
        }
    }
}