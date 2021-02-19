using System;
using System.Collections.Generic;
using System.Linq;

namespace Minesweaper.BoardInfo
{
    public class Board
    {
        private Dictionary<string, Square> board;
        private List<string> guesses;
        private readonly int xSize, ySize;

        public bool IsFinished { get; private set; }

        /// <summary>
        /// Creates board of given (X x Y) size.
        /// </summary>
        /// <param name="x">Number of Columns</param>
        /// <param name="y">Number of Rows</param>
        public Board(int x, int y)
        {
            board = new Dictionary<string, Square>();
            guesses = new List<string>();
            xSize = x; ySize = y;
            IsFinished = false;

            for (int i = 0; i < xSize; i++)
            {
                for (int j = 0; j < ySize; j++)
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

        public string Guess(string guess)
        {
            if (!board.ContainsKey(guess))
                return "Guess is not valid.";
            if (guesses.Contains(guess))
                return "You already guessed that!";

            guesses.Add(guess);
            if (guesses.Count > 5) IsFinished = true;
            return $"Your guess was a {board[guess]}";
        }

        public override string ToString()
        {
            string output = "";
            for (int i = -1; i < ySize; i++)
            {
                for (int j = -1; j < xSize; j++)
                {
                    output += GetSquareAsString(j, i);
                }
                output += "\n";
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

        /// <summary>
        /// Helper method to get a string representation of an individual board space
        /// </summary>
        /// <param name="x">x-coordinate of square</param>
        /// <param name="y">y-coordinate of square</param>
        /// <returns>string representation of ( x, y ) value</returns>
        private string GetSquareAsString(int x, int y)
        {
            string output = "";

            if (x < 0)
            {
                string rowNum = (y < 0) ? " " : $"{y + 1}";
                while (rowNum.Length < ySize.ToString().Length)
                {
                    rowNum = " " + rowNum;
                }
                output += $"{rowNum} ";
            }
            else if (y < 0)
            {
                output += $"| {GetLetterIndex(x)} ";
            } 
            else
            {
                string pos = $"{GetLetterIndex(x)}{y + 1}";

                if (guesses.Contains(pos))
                {
                    switch (board[pos])
                    {
                        case Square.Empty:
                            output += "|   ";
                            break;
                        case Square.Mine:
                            output += "| * ";
                            break;
                    }
                }
                else
                {
                    output += "| - ";
                }
            }
            return output;
        }
    }
}
