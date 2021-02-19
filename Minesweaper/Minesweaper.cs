using System;
using Minesweaper.BoardInfo;
using Minesweaper.Helpers;
using Minesweaper.Menus;

namespace Minesweaper
{
    public class Minesweaper
    {
        private Board board;
        private IUIHelper UIHelper;

        public Minesweaper(Difficulty diff, int boardSizeX, int boardSizeY, IUIHelper uiHelper)
        {
            UIHelper = uiHelper;

            board = new Board(boardSizeX, boardSizeY);
            board.Populate(diff);
        }

        public void Play()
        {
            PrintBoard("Make a guess to start playing!");

            while (!board.IsFinished)
            {
                string guess = UIHelper.GetString("Select square to guess (ie. A1):");
                string result = board.Guess(guess);

                PrintBoard(result);
            }

            string input = UIHelper.GetString("Play again (y/n)?");
            switch(input)
            {
                case "y":
                    Menu diffMenu = new DifficultyMenu(UIHelper);
                    diffMenu.Open();
                    break;
                case "n":
                    UIHelper.WriteLine("That's unfortunate.");
                    break;
                default:
                    UIHelper.WriteLine("That wasn't y or n! Exiting game.");
                    break;
            }
        }

        private void PrintBoard(string header)
        {
            UIHelper.Clear();
            UIHelper.WriteLine(header);
            UIHelper.WriteLine("");
            UIHelper.WriteLine(board);
        }
    }
}
