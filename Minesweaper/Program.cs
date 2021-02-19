using Minesweaper.Helpers;
using Minesweaper.Menus;
using System;

namespace Minesweaper
{
    class Program
    {
        static void Main(string[] args)
        {
            IUIHelper UIHelper = new CLIHelper();
            
            DisplayHeader(UIHelper);
            
            Menu main = new MainMenu(UIHelper);
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
