using Minesweaper.BoardInfo;
using Minesweaper.Helpers;
using System;
using System.Collections.Generic;

namespace Minesweaper.Menus
{
    public class DifficultyMenu : Menu
    {
        protected override Dictionary<string, Action> options
        {
            get
            {
                return new Dictionary<string, Action>()
                {
                    { "Easy", PlayEasyGame },
                    { "Medium", PlayMediumGame },
                    { "Hard", PlayHardGame }
                };
            }
        }

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
            Minesweaper ms = new Minesweaper(diff, 10, 10, UIHelper);
            ms.Play();
            isRunning = false;
        }
    }
}
