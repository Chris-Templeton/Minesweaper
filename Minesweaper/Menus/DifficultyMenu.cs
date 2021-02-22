using Minesweaper.BoardInfo;
using Minesweaper.Helpers;
using System;
using System.Collections.Generic;

namespace Minesweaper.Menus
{
    public class DifficultyMenu : Menu
    {
        protected override Dictionary<string, Action> options => new Dictionary<string, Action>()
        {
            { "Easy", PlayEasyGame },
            { "Medium", PlayMediumGame },
            { "Hard", PlayHardGame },
            { "Custom", PlayCustomGame}
        };

        protected override string name => "Select Difficulty";

        public DifficultyMenu(IUIHelper uiHelper) : base(uiHelper) { }

        void PlayEasyGame()
        {
            Play(Difficulty.Easy);
        }

        void PlayMediumGame()
        {
            Play(Difficulty.Medium);
        }

        void PlayHardGame()
        {
            Play(Difficulty.Hard);
        }

        private void Play(Difficulty diff)
        {
            Minesweaper ms = new Minesweaper(10, 10, diff, uiHelper);
            ms.Play();

            uiHelper.WriteLine("");
            string response = uiHelper.GetString("Play again (y/n)?");
            if (response.ToLower() == "n") isRunning = false;
        }

        private void PlayCustomGame()
        {
            Menu custom = new CustomGameMenu(uiHelper);
            custom.Open();
        }
    }
}
