using Minesweaper.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Minesweaper.Menus
{
    public abstract class Menu
    {
        protected abstract Dictionary<string, Action> options { get; }
        protected IUIHelper uiHelper;
        protected bool isRunning;
        protected virtual string name { get; }

        public Menu(IUIHelper uiHelper)
        {
            this.uiHelper = uiHelper;
            isRunning = true;
        }

        public void Open()
        {
            List<string> prompt = Enumerable.ToList<string>(options.Keys);
            prompt.Add("Exit");
            int selection;

            do
            {
                if (!(name is null))
                {
                    uiHelper.WriteLine("");
                    uiHelper.WriteLine(name);
                }

                uiHelper.WriteLine("");
                for (int i = 0; i < prompt.Count; i++)
                {
                    uiHelper.WriteLine($"{i + 1}. {prompt[i]}");
                }

                selection = uiHelper.GetInteger($"Select option (1-{prompt.Count}):") - 1;
                if (selection < 0 || selection >= prompt.Count)
                {
                    uiHelper.WriteLine("Not a valid option.");
                }
                else if (selection < prompt.Count - 1)
                {
                    options[prompt[selection]]();
                }
            } while (isRunning && selection != prompt.Count - 1);
        }
    }
}
