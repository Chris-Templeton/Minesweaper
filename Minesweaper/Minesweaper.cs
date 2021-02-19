using System;
using System.Collections.Generic;
using Minesweaper.BoardInfo;
using Minesweaper.Helpers;
using Minesweaper.Menus;

namespace Minesweaper
{
    public class Minesweaper
    {
        private IUIHelper uiHelper;
        private Board board;
        private List<string> guesses;
        private CLIBoardWriter boardWriter;
        private bool isFinished, hasHitMine;

        public Minesweaper(Difficulty diff, int boardSizeX, int boardSizeY, IUIHelper uiHelper)
        {
            this.uiHelper = uiHelper;

            board = new Board(boardSizeX, boardSizeY);
            board.Populate(diff);

            guesses = new List<string>();
            boardWriter = new CLIBoardWriter(board, guesses);
            isFinished = false; hasHitMine = false;
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
                guess = uiHelper.GetString("Enter space to guess:");
                uiHelper.Clear();
                uiHelper.WriteLine("");
                Square? sq = Guess(guess);
                if (sq is null)
                {
                    uiHelper.WriteLine("That wasn't a square!\n");
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

            if (board.Contains(square))
            {
                guesses.Add(square);
                sq = board.GetValue(square);
                switch (sq)
                {
                    case Square.Mine:
                        isFinished = true;
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

        }
    }
}
