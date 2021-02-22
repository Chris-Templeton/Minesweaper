using Minesweaper.BoardInfo;
using Minesweaper.Helpers;
using System;
using System.Collections.Generic;

namespace Minesweaper.Menus
{
    class CustomGameMenu : Menu
    {
        protected override Dictionary<string, Action> options => new Dictionary<string, Action>() 
        {
            { "Change Board's x size", ChangeXSize },
            { "Change Board's y size", ChangeYSize },
            { "Change Difficulty", ChangeDifficulty },
            { "Play with these settings", Play }
        };
        protected override string name => $"Board Size: x = {xSize}, y = {ySize} Difficulty: {diff}";

        private int xSize, ySize;
        private Difficulty diff;

        public CustomGameMenu(IUIHelper uiHelper) : base(uiHelper) 
        {
            xSize = 10; ySize = 10;
            diff = Difficulty.Medium;
        }

        public void ChangeXSize()
        {
            int x = int.MinValue;
            while (x <= 0)
            {
                if (x != int.MinValue) uiHelper.WriteLine("Size needs to be greater than zero.");
                x = uiHelper.GetInteger("Enter desired x size:");
            }
            xSize = x;
        }

        public void ChangeYSize()
        {
            int y = int.MinValue;
            while (y <= 0)
            {
                if (y != int.MinValue) uiHelper.WriteLine("Size needs to be greater than zero.");
                y = uiHelper.GetInteger("Enter desired y size:");
            }
            ySize = y;
        }

        public void ChangeDifficulty()
        {
            Difficulty? diff = null;
            while (diff is null)
            {
                string response = uiHelper.GetString("Enter desired difficulty (Easy/Medium/Hard)");
                switch (response.ToLower())
                {
                    case "easy":
                        diff = Difficulty.Easy;
                        break;
                    case "medium":
                        diff = Difficulty.Medium;
                        break;
                    case "hard":
                        diff = Difficulty.Hard;
                        break;
                    default:
                        uiHelper.WriteLine("That wasn't an option!");
                        break;
                }
            }
            this.diff = (Difficulty)diff;
        }

        public void Play()
        {
            Minesweaper ms = new Minesweaper(xSize, ySize, diff, uiHelper);
            ms.Play();

            uiHelper.WriteLine("");
            string response = uiHelper.GetString("Play again (y/n)?");
            if (response.ToLower() == "n") isRunning = false;
        }
    }
}
