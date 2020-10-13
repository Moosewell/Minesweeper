using System;


namespace Minesweeper
{
    class Program
    {
        static public int flagAmount = 0;
        static public Board board = new Board();
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Helper.Initialize(args);
            board.CreateBoard();
            board.PlaceMines();
            board.StoreMines();
            board.DrawBoard();
            do{
            PlayerAction();
            board.VictoryCheck();
            board.DrawBoard();
            } while (true);
        }
        public static void PlayerAction()
        {
            string input;
            do
            { 
                Console.Write("> ");
                input = Console.ReadLine().ToLower();
                if (Console.IsInputRedirected)
                {
                    Console.WriteLine(input);
                }
            } while (Commands.UserInput(input));
        }
        public static int GameEnd(int returnValue)
        {
            switch(returnValue)
            {
                case (0):
                board.DrawBoard();
                Console.WriteLine("WELL DONE!");
                System.Environment.Exit(returnValue);
                break;
                
                case (1):
                board.GameEndScreen();
                board.DrawBoard();
                Console.WriteLine("GAME OVER");
                System.Environment.Exit(returnValue);
                break;
                
                case (2):
                System.Environment.Exit(returnValue);
                break;
            }
            return returnValue;
        }
    }
}

