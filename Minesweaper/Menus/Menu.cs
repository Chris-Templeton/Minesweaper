using Minesweaper.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Minesweaper.Menus
{
    public abstract class Menu
    {
        protected abstract Dictionary<string, Action> options { get; }
        protected IUIHelper UIHelper;
        protected bool isRunning;

        public Menu(IUIHelper uiHelper)
        {
            UIHelper = uiHelper;
            isRunning = true;
        }

        public void Open()
        {
            List<string> prompt = Enumerable.ToList<string>(options.Keys);
            prompt.Add("Exit");
            int selection;

            do
            {
                UIHelper.WriteLine("");
                for (int i = 0; i < prompt.Count; i++)
                {
                    UIHelper.WriteLine($"{i + 1}. {prompt[i]}");
                }

                selection = UIHelper.GetInteger($"Select option (1-{prompt.Count}):") - 1;
                if (selection < 0 || selection >= prompt.Count)
                {
                    UIHelper.WriteLine("Not a valid option.");
                }
                else if (selection < prompt.Count - 1)
                {
                    options[prompt[selection]]();
                }
            } while (isRunning && selection != prompt.Count - 1);
        }
    }
}
