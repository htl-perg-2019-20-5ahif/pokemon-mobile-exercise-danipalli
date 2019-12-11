using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace PokemonExplorer
{
    public class Pokemon
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        public int Weight { get; set; }

        public List<String> Abilities { get; set; }

        public string FrontDefaultImage { get; set; }

        public string BackDefaultImage { get; set; }

        public bool DetailsLoaded { get; set; }

    }
}
