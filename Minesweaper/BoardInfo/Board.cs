using System;
using System.Collections.Generic;
using System.Linq;

namespace Minesweaper.BoardInfo
{
    public class Board
    {
        private Dictionary<string, Square> board;
        public readonly int xSize, ySize;

        /// <summary>
        /// Creates board of given (X x Y) size.
        /// </summary>
        /// <param name="x">Number of Columns</param>
        /// <param name="y">Number of Rows</param>
        public Board(int x, int y)
        {
            board = new Dictionary<string, Square>();
            xSize = x; ySize = y;

            for (int i = 0; i < xSize; i++)
            {
                for (int j = 0; j < ySize; j++)
                {
                    board.Add($"{GetLetterOfInt(i)}{j + 1}", Square.Empty);
                }
            }
        }

        /// <summary>
        /// Poopulates board based off difficulty.
        /// </summary>
        /// <param name="diff">Difficulty of board.</param>
        public void Populate(Difficulty diff)
        {
            int numMines = 0;
            int totalMinesNeeded = (int)diff * board.Count / 100;
            List<string> keys = Enumerable.ToList(board.Keys);
            Random rand = new Random();

            while (numMines < totalMinesNeeded)
            {
                int randomX = rand.Next(0, xSize);
                int randomY = rand.Next(0, ySize);
                string randomSquare = $"{GetLetterOfInt(randomX)}{randomY+1}";
                if (!board[randomSquare].Equals(Square.Mine))
                {
                    board[randomSquare] = Square.Mine;
                    AddOneToSurrounding(randomX, randomY);
                    numMines++;
                }
            }
        }

        /// <summary>
        /// Helper class that increments all of the squares around a mine by one.
        /// </summary>
        /// <param name="x">x coordinate of mine</param>
        /// <param name="y">y coordinate of mine</param>
        private void AddOneToSurrounding(int x, int y)
        {
            for (int i = x - 1; i <= x + 1; i++)
            {
                for (int j = y - 1; j <= y + 1; j++)
                {
                    if (i < 0 || i >= xSize || j < 0 || j >= ySize) continue;
                    string square = $"{GetLetterOfInt(i)}{j + 1}";
                    switch (board[square])
                    {
                        case Square.Mine:
                            break;
                        default:
                            board[square]++;
                            break;
                    }
                }
            }
        }

        /// <summary>
        /// Helper method to get column name based on integer. 0 = A ... 25 = Z, 26 = AA ...
        /// </summary>
        /// <param name="i">Integer input</param>
        /// <returns>Letter (or group of letters) base on input.</returns>
        public static string GetLetterOfInt(int i)
        {
            string letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string output = "";

            do
            {
                if (output.Length > 0) i -= letters.Length;
                output += letters[i % letters.Length];
            } while (i >= letters.Length);

            return output;
        }

        public Square GetValue(string square)
        {
            return board[square];
        }

        public bool Contains(string square)
        {
            return board.ContainsKey(square);
        }

        //TODO: delete this override - used only for testing class.
        public override string ToString()
        {
            string output = "";
            foreach (string key in board.Keys)
            {
                output += $"{key}:{board[key]}\n";
            }
            return output;
        }
    }
}
