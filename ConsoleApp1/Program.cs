using System;
using System.Threading.Tasks;

namespace JokeGenerator
{
    public static class Program
    {
        private static readonly JsonFeed ChuckNorrisJsonFeed = new JsonFeed("https://api.chucknorris.io");

        public static async Task Main()
        {
            ConsolePrinter.PrintValue("Press ? to get instructions.");
            if (Console.ReadLine() == "?")
            {
                while (true)
                {
                    Tuple<string, string> names = null;

                    ConsolePrinter.PrintValue("Press c to get categories");
                    ConsolePrinter.PrintValue("Press r to get random jokes");
                    var key = GetEnteredKey(Console.ReadKey());
                    if (key == 'c')
                    {
                        var categories = await GetCategories();
                        PrintResults(categories);
                    }

                    if (key == 'r')
                    {
                        ConsolePrinter.PrintValue("Want to use a random name? y/n");
                        key = GetEnteredKey(Console.ReadKey());
                        if (key == 'y')
                        {
                            names = await GetRandomName();
                        }

                        ConsolePrinter.PrintValue("Want to specify a category? y/n");
                        if (key == 'y')
                        {
                            ConsolePrinter.PrintValue("How many jokes do you want? (1-9)");
                            var n = int.Parse(Console.ReadLine());
                            ConsolePrinter.PrintValue("Enter a category;");
                            var jokes = await GetRandomJokes(names, Console.ReadLine(), n);
                            PrintResults(jokes);
                        }
                        else
                        {
                            ConsolePrinter.PrintValue("How many jokes do you want? (1-9)");
                            var n = int.Parse(Console.ReadLine());
                            var jokes = await GetRandomJokes(names, null, n);
                            PrintResults(jokes);
                        }
                    }
                }
            }
        }

        private static void PrintResults(string[] results)
        {
            ConsolePrinter.PrintValue("[" + string.Join(",", results) + "]");
        }

        private static char? GetEnteredKey(ConsoleKeyInfo consoleKeyInfo)
        {
            char? key = null;

            switch (consoleKeyInfo.Key)
            {
                case ConsoleKey.C:
                    key = 'c';
                    break;
                case ConsoleKey.D0:
                    key = '0';
                    break;
                case ConsoleKey.D1:
                    key = '1';
                    break;
                case ConsoleKey.D3:
                    key = '3';
                    break;
                case ConsoleKey.D4:
                    key = '4';
                    break;
                case ConsoleKey.D5:
                    key = '5';
                    break;
                case ConsoleKey.D6:
                    key = '6';
                    break;
                case ConsoleKey.D7:
                    key = '7';
                    break;
                case ConsoleKey.D8:
                    key = '8';
                    break;
                case ConsoleKey.D9:
                    key = '9';
                    break;
                case ConsoleKey.R:
                    key = 'r';
                    break;
                case ConsoleKey.Y:
                    key = 'y';
                    break;
            }

            return key;
        }

        private static Task<string[]> GetRandomJokes(Tuple<string, string> names, string category, int number)
        {
            return ChuckNorrisJsonFeed.GetRandomJokes(names?.Item1, names?.Item2, category, number);
        }

        private static Task<string[]> GetCategories()
        {
            return ChuckNorrisJsonFeed.GetCategoriesAsync();
        }

        private static async Task<dynamic> GetRandomName()
        {
            var jsonFeed = new JsonFeed("https://www.names.privserv.com/api/");
            var result = await jsonFeed.GetNames();
            return Tuple.Create(result.name.ToString(), result.surname.ToString());
        }
    }
}
