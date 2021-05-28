namespace JokeGenerator.Models
{
    public class Joke
    {
        public string Value { get; set; }

        public void ReplaceNameTo(Name name)
        {
            if (string.IsNullOrEmpty(name?.FirstName) || string.IsNullOrEmpty(name.LastName))
            {
                return;
            }

            this.Value = this.Value.Replace("Chuck Norris", $"{name.FirstName} {name.LastName}");
        }
    }
}
