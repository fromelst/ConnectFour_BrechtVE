using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectFourCTG
{
    //model class
    //implement state of the board, hold information about the connect 4 game and where each checker is at a given time
    //holds the 2d-array and winstates
    class BoardState
    {
        public bool winner = false;
        public int inARowToWin;
        public int boardSize;
        private char[,] boardGrid;
        public BoardState()
        {
            //boardSize = giveBoardSize;
            //boardGrid = new char[boardSize, boardSize];
            //DrawBoard('j', 'k');
        }
        public bool Full()
        {
            for (int i = 0; i < boardSize; i++)
            {
                if (boardGrid[0, i] == ' ')
                {
                    return false;
                }
            }
            return true;
        }
        private void CheckWin(char playerChar, int xPos, int yPos, int inARowToWin)
        {

            int inArow = 1;
            ///////////////
            //win X
            //First check right side
            for (int i = 1; i < inARowToWin; i++)
            {
                if (xPos + i >= boardSize)
                    break;
                if (boardGrid[yPos, xPos + i] == playerChar)//naar rechts
                    inArow++;
                else break;
            }
            //left side
            for (int i = 1; i < inARowToWin; i++)
            {
                if (xPos - i < 0)
                    break;
                if (boardGrid[yPos, xPos - i] == playerChar)//naar links
                    inArow++;
                else break;
            }
            if (inArow >= inARowToWin) //needs to be a function
                winner = true;
            else inArow = 1;
            ///////////////

            ///////////////
            //win Y
            for (int i = 1; i < inARowToWin; i++)
            {
                if (yPos + i >= boardSize)
                    break;
                if (boardGrid[yPos + i, xPos] == playerChar)//naar onder
                    inArow++;
                else break;
            }
            if (inArow >= inARowToWin)
                winner = true;
            else inArow = 1;
            ///////////////

            ///////////////
            //win downLeftUp \ upRightDown
            //Downwards to the right
            for (int i = 1; i < inARowToWin; i++)
            {
                if (yPos + i >= boardSize || xPos + i >= boardSize)
                    break;
                if (boardGrid[yPos + i, xPos + i] == playerChar)//naar rechts
                    inArow++;
                else break;
            }
            //Upwards to the left
            for (int i = 1; i < inARowToWin; i++)
            {
                if (yPos - i < 0 || xPos - i < 0)
                    break;
                if (boardGrid[yPos - i, xPos - i] == playerChar)//naar links
                    inArow++;
                else break;
            }
            if (inArow >= inARowToWin) //needs to be a function
                winner = true;
            else inArow = 1;
            ///////////////

            ///////////////
            //win downRightUp / upLeftDown
            //DownWards to the left
            for (int i = 1; i < inARowToWin; i++)
            {
                if (yPos + i >= boardSize || xPos - i < 0)
                    break;
                if (boardGrid[yPos + i, xPos - i] == playerChar)//naar rechts
                    inArow++;
                else break;
            }
            //Upwards to the right
            for (int i = 1; i < inARowToWin; i++)
            {
                if (yPos - i < 0 || xPos + i >= boardSize)
                    break;
                if (boardGrid[yPos - i, xPos + i] == playerChar)//naar links
                    inArow++;
                else break;
            }
            if (inArow >= inARowToWin) //needs to be a function
                winner = true;
            else inArow = 1;
            ///////////////
        }

        public bool spotIsEmpty(int posX)
        {
            //Console.WriteLine($"posY + posX: {getPosY(posX)} {posX}");
            //Console.ReadKey();
            if (getPosY(posX) == -1)
                return false;
            if (boardGrid[getPosY(posX), posX] == ' ')
            {
                return true;
            }
            return false;
        }

        private int getPosY(int posX)
        {
            for (int i = boardSize - 1; i >= 0; i--)
            {
                if (boardGrid[i, posX] == ' ')
                {
                    return i;
                }
            }
            return -1;
        }

        public void UpdateBoard(int posX, char playerChar)
        {
            int posY = getPosY(posX);
            boardGrid[posY, posX] = playerChar;
            //int posY = -1;
            //for (int i = boardSize-1; i >= 1; i++)
            //{
            //    if(boardGrid[i,posX-1] == ' ')
            //    {

            //        posY = i;
            //        break;
            //    }
            //}
            CheckWin(playerChar, posX, posY, inARowToWin);
        }

        public void EmptyBoard()
        {
            char[,] boardToBe = new char[boardSize, boardSize];
            boardGrid = boardToBe;
            for (int i = 0; i < boardSize; i++)
            {
                for (int j = 0; j < boardSize; j++)
                {
                    boardGrid[i, j] = ' ';
                }
            }
        }

        public void DrawBoard(char playerOneChar, char playerTwoChar, string playerOneName, string playerTwoName, int playerPlaying)
        {
            Console.Clear();
            Console.SetCursorPosition(0, 0);
            char outline = '|';
            for (int i = 0; i < boardSize; i++)
            {
                Console.Write("\n");
                for (int j = 0; j < boardSize; j++)
                {
                    if (j == 0)
                        Console.Write(outline);
                    Console.Write(boardGrid[i, j]);
                    if (j == boardSize - 1)
                        Console.Write(outline);
                }
            }
            Console.Write("\n");
            for (int i = 0; i < boardSize + 2; i++)
            {
                Console.Write('-');
            }
            Console.Write("\n");
            if (playerPlaying == 1)
            {
                Console.WriteLine($">{playerOneName}: {playerOneChar}");
                Console.WriteLine($"{playerTwoName}: {playerTwoChar}");
            }
            if (playerPlaying == 2)
            {
                Console.WriteLine($"{playerOneName}: {playerOneChar}");
                Console.WriteLine($">{playerTwoName}: {playerTwoChar}");
            }
            Console.WriteLine("Use the left and right arrow keys to move around");
            Console.WriteLine("Press enter to drop your character");
        }
    }
}
