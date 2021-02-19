using Minesweaper.Helpers;
using Minesweaper.Menus;
using System;

namespace Minesweaper
{
    class Program
    {
        static void Main(string[] args)
        {
            IUIHelper uiHelper = new CLIHelper();
            
            DisplayHeader(uiHelper);
            
            Menu main = new MainMenu(uiHelper);
            main.Open();
        }

        static void DisplayHeader(IUIHelper uiHelper)
        {
            uiHelper.WriteLine("*************************");
            uiHelper.WriteLine("***    Minesweaper    ***");
            uiHelper.WriteLine("*************************");
            uiHelper.WriteLine("***  Chris Templeton  ***");
            uiHelper.WriteLine("*************************");
        }
    }
}
