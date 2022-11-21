using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectFourCTG
{
    abstract class Players
    {
        char playerChar;
        string playerName;
        bool cpu;

        public void SetPlayerName(string giveName)
        {
            playerName = giveName;
        }
        public void Setcpu(bool isCPUOne)
        {
            cpu = isCPUOne;
        }
        public void SetplayerChar(char givePlayerChar)
        {
            playerChar = givePlayerChar;
        }

        public string GetNamePlayer() { return playerName; }
        public bool Getcpu() { return cpu; }
        public char GetCharPlayer() { return playerChar; }
        ConsoleKeyInfo cki;
        public virtual int MakeAMove(int minBoundary, int maxBoundary)
        {

            int positionX = minBoundary;
            int positionY = 0;
            Console.SetCursorPosition(positionX, positionY);

            while (cki.Key != ConsoleKey.Enter)
            {
                var ch = Console.ReadKey(false).Key;
                switch (ch)
                {
                    case ConsoleKey.LeftArrow:
                        if (positionX != minBoundary)
                            positionX--;
                        else positionX = maxBoundary;
                        Console.SetCursorPosition(positionX, positionY);
                        break;
                    case ConsoleKey.RightArrow:
                        if (positionX != maxBoundary)
                            positionX++;
                        else positionX = minBoundary;
                        Console.SetCursorPosition(positionX, positionY);
                        break;
                    case ConsoleKey.Enter:
                        return positionX - 1;
                    default:
                        Console.SetCursorPosition(positionX, positionY);
                        break;
                }
            }
            return positionX - 1;
        }
    }
}
