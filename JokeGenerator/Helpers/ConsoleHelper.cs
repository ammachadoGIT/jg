using System;
using System.Collections.Generic;
using System.Linq;
using JokeGenerator.Extensions;

namespace JokeGenerator.Helpers
{
    public static class ConsoleHelper
    {
        public static void DisplayInstructions()
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

        public static int ReadUntilInt(string question, Func<int, bool> successCondition)
        {
            int value;

            while (true)
            {
                Console.WriteLine(question);
                if (int.TryParse(Console.ReadKey().ToChar().ToString(), out value) && successCondition(value))
                {
                    break;
                }
            }

            return value;
        }

        public static char? ReadUntilKey(string question, Func<char?, bool> successCondition)
        {
            char? key;

            while (true)
            {
                Console.WriteLine(question);
                key = Console.ReadKey().ToChar();
                if (successCondition(key))
                {
                    break;
                }
            }

            return key;
        }

        public static string ReadUntilListContains(string question, IEnumerable<string> list)
        {
            string value;
            while (true)
            {
                Console.WriteLine(question);
                value = Console.ReadLine();
                if (list.Contains(value))
                {
                    break;
                }
            }

            return value;
        }
    }
}
