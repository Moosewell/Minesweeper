using System;


namespace Minesweeper
{
    struct Commands
    {
        static int row;
        static int col;
        static public int LatestInputRow;
        static public int LatestInputCol;
        public static bool UserInput(string input)
        {
            if(input.Length == 4)
            {
                switch (input[0], input[1], input[2], input[3])
                {
                    case var (_, v, _, _) when v != ' ':
                        Console.WriteLine("syntax error");
                    return true;

                    case var (x, _, y, z) 
                    when (x == 114 || x == 102) && y >= 'a' && y <= 'j' && z >= 48 && z <= 57:
                        row = input[2] -97;
                        col = int.Parse(input[3].ToString());
                        if(CheckIfAllowed(input[0]))
                        { 
                            ExecuteCmd(input[0]);
                            return false;
                        }
                        else
                        {
                            Console.WriteLine("not allowed");
                            return true;
                        }

                    case var (x, _, y, z) 
                    when (x >= 'a' && x <= 'ö' && y >= 'a' && y <= 'j' && z >= 48 && z <= 57):
                    Console.WriteLine("unknown command");
                            return true;

                    default:
                    Console.WriteLine("syntax error");
                    return true;
                }
            }
            else if (input.Length == 1)
            {
                if(input == "q")
                {
                Program.GameEnd(2);
                return true;
                }
                else if (input[0] >= 'a' && input[0] <= 'ö')
                {
                    Console.WriteLine("unknown command");
                    return true;
                }
                else
                {
                    Console.WriteLine("syntax error");
                    return true;
                }
            }
            else
            {
                Console.WriteLine("syntax error");
                return true;
            }
        }
        static public bool CheckIfAllowed(char command)
        {
            if(command == 'f' && Program.flagAmount >= 25 && Program.board.matrix[row,col].flagged == false)
            {
                return false;
            }
            else if (Program.board.matrix[row,col].sweeped == true)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        static public void ExecuteCmd(char command)
        {
            if(command == 'r')
            {
                    LatestInputRow = row;
                    LatestInputCol = col;
                    Program.board.matrix[row,col] = Sweep(Program.board.matrix[row,col]);
            }
            else
            {
                if(Program.board.matrix[row,col].flagged == false)
                {
                    Program.board.matrix[row,col].flagged = true;
                    Program.board.matrix[row,col].symbol = "F";
                    Program.flagAmount++;
                }
                else
                {
                    Program.board.matrix[row,col].flagged = false;
                    Program.board.matrix[row,col].symbol = "X";
                    Program.flagAmount--;
                }
            }
        }

        static public Tile Sweep(Tile inputMatrix)
        {
            if(inputMatrix.mine == true)
            {
                Program.GameEnd(1);
                return inputMatrix;
            }
            if(inputMatrix.flagged == true)
            {
                inputMatrix.flagged = false;
                Program.flagAmount--;
            }
            if(inputMatrix.sweeped == false)
            {
                inputMatrix.sweeped = true;
            }
            if(inputMatrix.neighbouringMines > 0)
            {
                inputMatrix.symbol = inputMatrix.neighbouringMines.ToString();
            }
            else
            {
                inputMatrix.symbol = ".";
                if(!inputMatrix.sweepedByNeighbour)
                {
                    Program.board.SweepNeighbours(row, col);
                }
            }
            return inputMatrix;
        }
    }
}