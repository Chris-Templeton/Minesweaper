using System;

namespace Minesweaper.Helpers
{
    public class CLIHelper : IUIHelper
    {
        public void Write(object message)
        {
            Console.Write(message.ToString());
        }

        public void WriteLine(object message)
        {
            Console.WriteLine(message.ToString());
        }

        public int GetInteger(string message)
        {
            string userInput = string.Empty;
            int intValue = 0;
            int numberOfAttempts = 0;

            do
            {
                if (numberOfAttempts > 0)
                {
                    Console.WriteLine("Invalid input format. Please try again");
                }

                Console.Write(message + " ");
                userInput = Console.ReadLine();
                numberOfAttempts++;
            }
            while (!int.TryParse(userInput, out intValue));

            return intValue;
        }

        public string GetString(string message)
        {
            string userInput = string.Empty;
            int numberOfAttempts = 0;

            do
            {
                if (numberOfAttempts > 0)
                {
                    Console.WriteLine("Invalid input format. Please try again");
                }

                Console.Write(message + " ");
                userInput = Console.ReadLine();
                numberOfAttempts++;
            }
            while (string.IsNullOrEmpty(userInput));

            return userInput;
        }

        public void Clear()
        {
            Console.Clear();
        }
    }
}
