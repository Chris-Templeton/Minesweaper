using Minesweaper.BoardInfo;
using Minesweaper.Helpers;
using System;
using System.Collections.Generic;

namespace Minesweaper
{
    public class Minesweaper
    {
        private IUIHelper uiHelper;
        private Board board;
        private List<string> guesses;
        private CLIBoardWriter boardWriter;
        private bool hasHitMine;

        private bool isFinished
        {
            get
            {
                return hasHitMine || board.xSize * board.ySize - board.totalMines - guesses.Count == 0;
            }
        }

        public Minesweaper(int boardSizeX, int boardSizeY, Difficulty diff, IUIHelper uiHelper)
        {
            this.uiHelper = uiHelper;

            board = new Board(boardSizeX, boardSizeY, diff);

            guesses = new List<string>();
            boardWriter = new CLIBoardWriter(board, guesses);
            hasHitMine = false;
        }

        public void Play()
        {
            string guess;
            uiHelper.Clear();
            uiHelper.WriteLine("");
            uiHelper.WriteLine("New Game Started!\n");
            boardWriter.WriteBoard();
            uiHelper.WriteLine("");

            while (!isFinished)
            {
                guess = uiHelper.GetString("Enter space to guess:").ToUpper();
                uiHelper.Clear();
                uiHelper.WriteLine("");
                Square? sq = Guess(guess);
                if (sq is null && !guesses.Contains(guess))
                {
                    uiHelper.WriteLine("That wasn't a square!\n");
                }
                else if (sq is null && guesses.Contains(guess))
                {
                    uiHelper.WriteLine("That has already been guessed!\n");
                }
                else if (sq.Equals(Square.Empty))
                {
                    uiHelper.WriteLine("That was Empty!\n");
                }
                else
                {
                    uiHelper.WriteLine($"That was a {sq}!\n");
                }
                boardWriter.WriteBoard();
                uiHelper.WriteLine("");
            }

            if (hasHitMine)
            {
                uiHelper.WriteLine("Better luck next time!");
            }
            else
            {
                uiHelper.WriteLine("Congratulations!");
            }
        }

        private Square? Guess(string square)
        {
            Square? sq = null;

            if (board.Contains(square) && !guesses.Contains(square))
            {
                guesses.Add(square);
                sq = board.GetValue(square);
                switch (sq)
                {
                    case Square.Mine:
                        hasHitMine = true;
                        break;
                    case Square.Empty:
                        GuessSurroundingSquares(square);
                        break;
                    default:
                        break;
                }
            }
            return sq;
        }

        private void GuessSurroundingSquares(string square)
        {
            int yValue = -1, xValue = -1;
            string xValueString = String.Empty;
            for (int i = square.Length - 1; int.TryParse(square.Substring(i), out int y); i--)
            {
                xValueString = square.Substring(0, i);
                yValue = int.Parse(square.Substring(i)) - 1;
            }

            string alphabet = @"ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            xValue = alphabet.IndexOf(xValueString[0]) + 26 * (xValueString.Length - 1);

            for (int i = xValue - 1; i <= xValue + 1; i++)
            {
                for (int j = yValue - 1; j <= yValue + 1; j++)
                {
                    if (i < 0 || i >= board.xSize || j < 0 || j >= board.ySize || (i == xValue && j == yValue))
                        continue;
                    string sq = $"{Board.GetLetterOfInt(i)}{j + 1}";
                    if (!guesses.Contains(sq))
                        Guess(sq);
                }
            }
        }
    }
}
