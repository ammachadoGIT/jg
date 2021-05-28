using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using JokeGenerator.Models;
using Newtonsoft.Json;

namespace JokeGenerator.Services
{
    public class JsonFeed : IJsonFeed
    {
        private readonly HttpClient client;

        public JsonFeed(HttpClient client)
        {
            this.client = client;
        }

        public async Task<IEnumerable<Joke>> GetRandomJokes(Name name, string category, int jokesCount)
        {
            var jokes = new List<Joke>();

            var url = "/jokes/random";
            if (category != null)
            {
                url += url.Contains('?') ? "&" : "?";
                url += $"category={category}";
            }

            var jokeTasks = new List<Task<string>>();
            for (var i = 0; i < jokesCount; i++)
            {
                jokeTasks.Add(this.client.GetStringAsync(url));
            }

            await Task.WhenAll(jokeTasks);
            foreach (var jokeTask in jokeTasks)
            {
                var joke = JsonConvert.DeserializeObject<Joke>(await jokeTask);
                joke.ReplaceNameTo(name);
                jokes.Add(joke);
            }

            return jokes;
        }

        public async Task<Name> GetRandomNameAsync()
        {
            var response = await this.client.GetStringAsync(string.Empty);
            var result = JsonConvert.DeserializeObject<Name>(response);
            return new Name { FirstName = result.FirstName, LastName = result.LastName };
        }

        public async Task<string[]> GetCategoriesAsync()
        {
            var response = await this.client.GetStringAsync("jokes/categories");
            return JsonConvert.DeserializeObject<string[]>(response);
        }
    }
}
