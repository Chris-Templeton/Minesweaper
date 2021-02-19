using Minesweaper.Helpers;
using System;
using System.Collections.Generic;

namespace Minesweaper.Menus
{
    public class MainMenu : Menu
    {
        protected override Dictionary<string, Action> options
        {
            get
            {
                return new Dictionary<string, Action>()
                {
                    { "Play Game", PlayGame },
                    { "View High Scores", ViewHighScores }
                };
            }
        }

        public MainMenu(IUIHelper uiHelper) : base(uiHelper) { }

        void PlayGame()
        {
            Menu selectDifficulty = new DifficultyMenu(UIHelper);
            selectDifficulty.Open();
        }

        void ViewHighScores()
        {
            UIHelper.WriteLine("View High scores was selected #NotImplimented");
        }
    }
}
