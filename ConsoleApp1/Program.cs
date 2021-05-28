using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using JokeGenerator.Extensions;
using JokeGenerator.Helpers;
using JokeGenerator.Models;
using JokeGenerator.Services;

namespace JokeGenerator
{
    public static class Program
    {
        private static IJsonFeed chuckNorrisJsonFeed;
        private static IJsonFeed namesJsonFeed;

        public static async Task Main()
        {
            InitializeServices();

            while (true)
            {
                ConsoleHelper.DisplayInstructions();

                var key = Console.ReadKey().ToChar();
                switch (key)
                {
                    case 'c':
                        await ListCategoriesAsync(true);
                        break;
                    case 'r':
                        {
                            var randomName = await GetRandomNameAsync();
                            var category = await GetCategoryAsync();
                            await ListRandomJokesAsync(randomName, category);
                            break;
                        }
                    case 'q':
                        return;
                    default:
                        continue;
                }

                ConsoleHelper.WaitForAnyKey();
            }
        }

        private static void InitializeServices()
        {
            var chuckNorrisHttpClient = new HttpClient { BaseAddress = new Uri("https://api.chucknorris.io") };
            var namesHttpClient = new HttpClient { BaseAddress = new Uri("https://www.names.privserv.com/api") };

            // TODO: ideally, these services should be initialized via Dependency Injection
            chuckNorrisJsonFeed = new JsonFeed(chuckNorrisHttpClient);
            namesJsonFeed = new JsonFeed(namesHttpClient);
        }

        private static async Task<string> GetCategoryAsync()
        {
            Console.WriteLine("\nWant to specify a category? y/N");

            var key = Console.ReadKey().ToChar();
            if (key != 'y')
            {
                return null;
            }

            Console.WriteLine();
            Console.WriteLine();
            var categories = await ListCategoriesAsync(false);

            Console.WriteLine("Enter a category: ");
            var category = Console.ReadLine();

            if (!categories.Contains(category))
            {
                Console.WriteLine("\nInvalid value. No category will be selected.");
                category = null;
            }

            return category;
        }

        private static async Task<IEnumerable<string>> ListCategoriesAsync(bool shouldClearConsole)
        {
            var categories = await chuckNorrisJsonFeed.GetCategoriesAsync();
            ConsoleHelper.PrintResults(shouldClearConsole, "Categories", categories);

            return categories;
        }

        private static async Task<Name> GetRandomNameAsync()
        {
            Name name = null;

            Console.WriteLine("\nWant to use a random name? y/N");

            var key = Console.ReadKey().ToChar();
            if (key == 'y')
            {
                name = await namesJsonFeed.GetRandomNameAsync();
            }

            return name;
        }

        private static async Task ListRandomJokesAsync(Name name, string category)
        {
            Console.WriteLine("\nHow many jokes do you want? (1-9)");

            if (!int.TryParse(Console.ReadLine(), out var jokesCount) || jokesCount < 1 || jokesCount > 9)
            {
                Console.WriteLine("\nInvalid value. You will get 1 joke.");
                ConsoleHelper.WaitForAnyKey();

                jokesCount = 1;
            }

            var jokes = await chuckNorrisJsonFeed.GetRandomJokes(name, category, jokesCount);
            ConsoleHelper.PrintResults(true, "Joke(s)", jokes.Select(joke => joke.Value));
        }
    }
}
