using System;

namespace Minesweaper
{
    class Program
    {
        static void Main(string[] args)
        {
            // menu stuff

            // create objects

            // allow gameplay

            Board b = new Board(10, 10);
            b.Populate(Difficulty.Hard);
            Console.WriteLine(b.ToString());
        }
    }
}
