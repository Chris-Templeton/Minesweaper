using System;
using System.Collections.Generic;
using System.Linq;

namespace Minesweaper
{
    /// <summary>
    /// Individual space or square of board. Denotes whether space is a mine or is empty.
    /// </summary>
    public enum Square
    {
        Mine, Empty
    }

    /// <summary>
    /// Difficulty numbers correspond to what percentage of the board will be mines.
    /// </summary>
    public enum Difficulty
    {
        Easy = 10, Medium = 20, Hard = 30
    }

    public class Board
    {
        private Dictionary<string, Square> board = new Dictionary<string, Square>();

        /// <summary>
        /// Creates board of given (X x Y) size.
        /// </summary>
        /// <param name="x">Number of Columns</param>
        /// <param name="y">Number of Rows</param>
        public Board(int x, int y)
        {
            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < y; j++)
                {

                    board.Add($"{GetLetterIndex(i)}{j + 1}", Square.Empty);

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
                int randomKey = rand.Next(0, keys.Count);
                if (!board[keys[randomKey]].Equals(Square.Mine))
                {
                    board[keys[randomKey]] = Square.Mine;
                    numMines++;
                }
            }
        }

        public override string ToString()
        {
            string output = "";
            foreach (string key in board.Keys)
            {
                output += $"{key}:{board[key]}\n";
            }
            return output;
        }

        /// <summary>
        /// Helper method to get column name based on integer. 0 = A ... 25 = Z, 26 = AA ...
        /// </summary>
        /// <param name="i">Integer input</param>
        /// <returns>Letter (or group of letters) base on input.</returns>
        private string GetLetterIndex(int i)
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
    }
}
