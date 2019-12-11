using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text.Json.Serialization;

namespace PokemonExplorer
{
    public class PokemonList
    {
        [JsonPropertyName("results")]
        public ObservableCollection<Pokemon> Results { get; set; }
    }

    public class PokemonData
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("weight")]
        public int Weight { get; set; }

        [JsonPropertyName("abilities")]
        public List<Ability> Abilities { get; set; }

        [JsonPropertyName("sprites")]
        public ImageURLs ImageURLs { get; set; }
    }

    public class Ability
    {
        [JsonPropertyName("ability")]
        public AbilityName AbilityName { get; set; }
    }

    public class AbilityName
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
    }

    public class ImageURLs
    {
        [JsonPropertyName("front_default")]
        public string FrontDefault { get; set; }

        [JsonPropertyName("back_default")]
        public string BackDefault { get; set; }
    }
}
