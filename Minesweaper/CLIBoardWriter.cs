using Minesweaper.BoardInfo;
using Minesweaper.Helpers;
using System.Collections.Generic;

namespace Minesweaper
{
    public class CLIBoardWriter
    {
        private IUIHelper uiHelper = new CLIHelper();
        private Board board;
        private List<string> guessed;

        public CLIBoardWriter(Board board, List<string> guessed)
        {
            this.board = board;
            this.guessed = guessed;
        }

        public void WriteBoard()
        {
            for (int y = -1; y < board.ySize; y++)
            {
                for (int x = -1; x < board.xSize; x++)
                {
                    string s = GetStringOfBox(x, y);
                    uiHelper.Write(s);
                }
                uiHelper.Write("\n");
            }
        }

        private string GetStringOfBox(int x, int y)
        {
            string output = "";

            if (x < 0)
            {
                output = (y < 0) ? "  " : $"{y + 1} ";
                while (output.Length < board.ySize.ToString().Length + 1)
                {
                    output = " " + output;
                }
            }
            else if (y < 0)
            {
                output = $"| {Board.GetLetterOfInt(x)} ";
            }
            else
            {
                string square = $"{Board.GetLetterOfInt(x)}{y + 1}";
                if (guessed.Contains(square))
                {
                    Square sq = board.GetValue(square);
                    output = $"| {ConvertSquareToString(sq)} ";
                }
                else
                {
                    output = $"| - ";
                }
            }

            return output;
        }

        private string ConvertSquareToString(Square sq)
        {
            switch (sq)
            {
                case Square.Mine:
                    return "*";
                case Square.Empty:
                    return " ";
                default:
                    return $"{(int)sq}";
            }
        }
    }
}
