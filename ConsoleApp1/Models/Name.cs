using Newtonsoft.Json;

namespace JokeGenerator.Models
{
    public class Name
    {
        [JsonProperty("name")]
        public string FirstName { get; set; }

        [JsonProperty("surname")]
        public string LastName { get; set; }
    }
}
