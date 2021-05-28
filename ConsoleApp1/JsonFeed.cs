using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace JokeGenerator
{
    public class JsonFeed
    {
        private readonly HttpClient client;

        public JsonFeed(string endpoint)
        {
            this.client = new HttpClient { BaseAddress = new Uri(endpoint) };
        }

        public async Task<string[]> GetRandomJokes(string firstname, string lastname, string category, int number)
        {
            var url = $"jokes/random/{number}";
            if (category != null)
            {
                if (url.Contains('?'))
                {
                    url += "&";
                }
                else
                {
                    url += "?";
                }

                url += $"category={category}";
            }

            var jokes = await this.client.GetStringAsync(url);

            if (firstname != null && lastname != null)
            {
                var index = jokes.IndexOf("Chuck Norris", StringComparison.Ordinal);
                var firstPart = jokes.Substring(0, index);
                var secondPart = jokes.Substring(0 + index + "Chuck Norris".Length, jokes.Length - (index + "Chuck Norris".Length));
                jokes = firstPart + " " + firstname + " " + lastname + secondPart;
            }

            return new string[] { JsonConvert.DeserializeObject<dynamic>(jokes).value };
        }

        public async Task<dynamic> GetNames()
        {
            var result = await this.client.GetStringAsync(string.Empty);
            return JsonConvert.DeserializeObject<dynamic>(result);
        }

        public async Task<string[]> GetCategoriesAsync()
        {
            var result = await this.client.GetStringAsync("categories");
            return new[] { result };
        }
    }
}
