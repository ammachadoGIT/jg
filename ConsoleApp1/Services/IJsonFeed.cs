using System.Collections.Generic;
using System.Threading.Tasks;
using JokeGenerator.Models;

namespace JokeGenerator.Services
{
    public interface IJsonFeed
    {
        Task<IEnumerable<Joke>> GetRandomJokes(Name name, string category, int jokesCount);

        Task<Name> GetRandomNameAsync();

        Task<string[]> GetCategoriesAsync();
    }
}
