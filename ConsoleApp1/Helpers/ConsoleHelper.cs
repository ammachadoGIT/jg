using System;
using System.Collections.Generic;

namespace JokeGenerator.Helpers
{
    public static class ConsoleHelper
    {
        public static void DisplayOptions()
        {
            Console.Clear();
            Console.WriteLine("Press c to get categories");
            Console.WriteLine("Press r to get random jokes");
            Console.WriteLine("Press q to quit");
        }

        public static void WaitForAnyKey()
        {
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        public static void PrintResults(bool shouldClearConsole, string header, IEnumerable<string> results)
        {
            if (shouldClearConsole)
            {
                Console.Clear();
            }

            Console.WriteLine(header);
            Console.WriteLine(new string('-', 20));
            Console.WriteLine(string.Join("\n", results));
            Console.WriteLine();
        }
    }
}
