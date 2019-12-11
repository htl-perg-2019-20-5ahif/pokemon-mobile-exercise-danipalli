using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PokemonExplorer
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        private static HttpClient _httpClient;

        private ObservableCollection<Pokemon> PokemonsValue;
        public ObservableCollection<Pokemon> Pokemons
        {
            get => PokemonsValue;
            set
            {
                PokemonsValue = value;
                OnPropertyChanged(nameof(Pokemons));
            }
        }


        public MainPage()
        {
            InitializeComponent();
            BindingContext = this;

            _httpClient = new HttpClient();
            Pokemons = new ObservableCollection<Pokemon>();

            LoadPokemonsAsync();
        }

        private async Task LoadPokemonDetailsAsync(string name)
        {
            var pokemonApiResponse = await _httpClient.GetAsync($"https://pokeapi.co/api/v2/pokemon/{name}");
            pokemonApiResponse.EnsureSuccessStatusCode();
            var responseBody = await pokemonApiResponse.Content.ReadAsStringAsync();
            var pokemonData = JsonSerializer.Deserialize<PokemonData>(responseBody);

            Pokemon pokemon = new Pokemon
            {
                Name = pokemonData.Name,
                Weight = pokemonData.Weight,
                Abilities = new List<string>(),
                FrontDefaultImage = pokemonData.ImageURLs.FrontDefault,
                BackDefaultImage = pokemonData.ImageURLs.BackDefault,
                DetailsLoaded = true
            };

            var index = Pokemons.IndexOf(Pokemons.First(p => p.Name == name));
            Pokemons[index] = pokemon;
        }


        private async Task LoadPokemonsAsync()
        {
            var pokemonApiResponse = await _httpClient.GetAsync($"https://pokeapi.co/api/v2/pokemon/?offset=0&limit=1000");
            pokemonApiResponse.EnsureSuccessStatusCode();
            var responseBody = await pokemonApiResponse.Content.ReadAsStringAsync();
            var pokemonList = JsonSerializer.Deserialize<PokemonList>(responseBody);
            Pokemons = pokemonList.Results;
        }

        private async void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            await Navigation.PushModalAsync(new PokemonDetails(e.Item as Pokemon));
        }

        private void ListView_ItemAppearing(object sender, ItemVisibilityEventArgs e)
        {
            var pokemon = e.Item as Pokemon;
            if (!pokemon.DetailsLoaded)
            {
                LoadPokemonDetailsAsync(pokemon.Name);
            }
        }
    }
}
