using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectFourCTG
{
    //class to play the game turn-based
    class PlayConnect
    {
        BoardState board;
        Players playerOne = null;
        Players playerTwo = null;
        public PlayConnect()
        {
            Start();
        }
        private void Start()
        {
            GameSetUp();
            PlayGame();
        }
        private void Rematch()
        {
            if (Yes("Would you like to have a rematch?"))
            {
                Console.Clear();
                Start();
            }
            else Environment.Exit(0);
        }
        public void PlayGame()
        {
            Console.WriteLine("Pres any key to start playing");
            Console.ReadKey();
            Console.Clear();

            //player one



            for (int i = 0; i < 9; i++)
            {
                board.DrawBoard(playerOne.GetCharPlayer(), playerTwo.GetCharPlayer(), playerOne.GetNamePlayer(), playerTwo.GetNamePlayer(), 1);
                int moveX = playerOne.MakeAMove(1, board.boardSize);
                while (!board.spotIsEmpty(moveX))
                    moveX = playerOne.MakeAMove(1, board.boardSize);
                board.UpdateBoard(moveX, playerOne.GetCharPlayer());
                if (board.winner == true)
                {
                    //Console.SetCursorPosition(0, 6 + board.boardSize);
                    Console.Clear();
                    board.DrawBoard(playerOne.GetCharPlayer(), playerTwo.GetCharPlayer(), playerOne.GetNamePlayer(), playerTwo.GetNamePlayer(), 1);
                    Console.WriteLine($"Congratulations to {playerOne.GetNamePlayer()}, you have won the game!");
                    Rematch();
                }
                if (board.Full())
                {
                    //Console.SetCursorPosition(0, 6 + board.boardSize);
                    Console.Clear();
                    board.DrawBoard(playerOne.GetCharPlayer(), playerTwo.GetCharPlayer(), playerOne.GetNamePlayer(), playerTwo.GetNamePlayer(), 1);
                    Console.WriteLine("The board is full!");
                    Rematch();
                }
                //player two
                board.DrawBoard(playerOne.GetCharPlayer(), playerTwo.GetCharPlayer(), playerOne.GetNamePlayer(), playerTwo.GetNamePlayer(), 2);

                moveX = playerTwo.MakeAMove(1, board.boardSize);
                while (!board.spotIsEmpty(moveX))
                    moveX = playerTwo.MakeAMove(1, board.boardSize);

                board.UpdateBoard(moveX, playerTwo.GetCharPlayer());
                if (board.winner == true)
                {
                    //Console.SetCursorPosition(0, 6+board.boardSize);
                    Console.Clear();
                    board.DrawBoard(playerOne.GetCharPlayer(), playerTwo.GetCharPlayer(), playerOne.GetNamePlayer(), playerTwo.GetNamePlayer(), 2);
                    Console.WriteLine($"Congratulations to {playerTwo.GetNamePlayer()}, you have won the game!");
                    Rematch();
                }
                if (board.Full())
                {
                    //Console.SetCursorPosition(0, 6 + board.boardSize);
                    Console.Clear();
                    board.DrawBoard(playerOne.GetCharPlayer(), playerTwo.GetCharPlayer(), playerOne.GetNamePlayer(), playerTwo.GetNamePlayer(), 2);
                    Console.WriteLine("The board is full!");
                    Rematch();
                }
            }
        }
        private void GameSetUp()
        {
            if (playerOne == null)
                playerOne = InstantiatePlayer("one");
            if (playerTwo == null)
                playerTwo = InstantiatePlayer("two");
            board = instantiateBoard();
            board.EmptyBoard();
            Console.WriteLine("Alright, everything has been set up!");
        }
        private BoardState instantiateBoard()
        {
            BoardState board = new BoardState();
            bool loop = true;
            while (loop)
            {
                int boardSize;
                Console.WriteLine("Enter a number. This will be the x and y size of the board.");
                Console.WriteLine("The board can not be bigger than 100 or smaller than 3");
                if (int.TryParse(Console.ReadLine(), out boardSize))
                {
                    if (boardSize <= 100 && boardSize > 2)
                    {
                        board.boardSize = boardSize;
                        board.EmptyBoard();
                        Console.WriteLine($"you selected {board.boardSize} to be the size of your board");
                        loop = false;
                    }
                }
                else Console.WriteLine("Only numbers between 3 and 100 are allowed");
            } //loop for boardsize
            loop = true;
            while (loop)
            {
                int inARowToWin;
                Console.WriteLine("Enter a number. This will be the amount you need to connect to win.");
                Console.WriteLine($"The amount cannot be bigger than the boardsize: {board.boardSize}");
                if (int.TryParse(Console.ReadLine(), out inARowToWin))
                {
                    if (inARowToWin <= board.boardSize && inARowToWin > 2)
                    {
                        board.inARowToWin = inARowToWin;
                        Console.WriteLine($"You will need to connect {board.inARowToWin} to be able to win the game");
                        loop = false;
                    }
                }
                else Console.WriteLine($"Only numbers between 3 and {board.boardSize} are allowed");
            }

            return board;
        }
        private Players InstantiatePlayer(string whichPlayer)
        {
            Players player;
            if (Yes($"Will player {whichPlayer} be controlled by a CPU?"))
            {
                player = new CPU();
                Console.WriteLine(": Player one will be a CPU!");
            }
            else
            {
                player = new Real();
                Console.WriteLine(": Player {0} will be controlled by a real person!", whichPlayer);
            }
            Console.WriteLine("Insert the name of player {0} below:", whichPlayer);
            player.SetPlayerName(Console.ReadLine());
            Console.WriteLine("Welcome {0}! ", player.GetNamePlayer());

            bool check = true;
            while (check)
            {

                Console.WriteLine($"Enter the symbol which player {whichPlayer} will use. | - are not allowed");
                char playerChar = Console.ReadKey().KeyChar;
                if (playerChar == '|' || playerChar == '-' || playerChar == ' ')
                {
                    Console.WriteLine(": | - are not allowed");
                }
                //else if(playerOne.GetCharPlayer() != null && playerChar == playerOne.GetCharPlayer())//hier nog een check voor als de tweede speler hetzelfde symbool neemt als speler 1
                else
                {
                    player.SetplayerChar(playerChar);
                    Console.WriteLine($": Player {whichPlayer} will be using '{player.GetCharPlayer()}' as his character");
                    check = false;
                }
            }


            return player;
        }
        private bool Yes(string yesNoQuestion)
        {
            while (true)
            {
                Console.WriteLine(yesNoQuestion);
                Console.WriteLine("Y/N");
                char answer = Console.ReadKey().KeyChar;
                if (answer == 'Y' || answer == 'y')
                    return true;
                else if (answer == 'N' || answer == 'n')
                    return false;
                else
                {
                    Console.WriteLine(": Accepted input is only y,Y,n,N");
                }
            }
        }
    }
}
