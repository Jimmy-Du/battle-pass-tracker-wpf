using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using System.Net.Http;
using battle_pass_tracker_wpf.Models;
using System.Text;
using System.Linq;

namespace battle_pass_tracker_wpf.Utilities
{
    static class BattlePassTrackerApi
    {
        private static readonly string BaseURL = "https://battle-pass-tracker-api.herokuapp.com";
        public static HttpClient ApiClient { get; set; }



        public static void InitializeClient()
        {
            ApiClient = new HttpClient();
            ApiClient.BaseAddress = new Uri(BaseURL);
            ApiClient.DefaultRequestHeaders.Accept.Clear();
            ApiClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }



        // Function:    GetUserGames()
        // Description: retrieves a list of the users game preference and creates a json object
        //              to specify which games to retrieve from the api
        // Parameters:  N/A
        // Return:      a string containing json to specify which games to retrieve from the api
        public static string GetUserGames()
        {
            List<string> userSelectedGames = new List<string>();
            List<int> userSelectedGamesIntList = new List<int>();

            // if user game preferences exist, their preferences will be loaded
            if (JsonSerializer.Deserialize<List<string>>(Properties.Settings.Default.SelectedGames) != null)
            {
                userSelectedGames = JsonSerializer.Deserialize<List<string>>(Properties.Settings.Default.SelectedGames);
                userSelectedGamesIntList = userSelectedGames.Select(x => Int32.Parse(x)).ToList();
            }

            return $"{{ \"games\" : {JsonSerializer.Serialize(userSelectedGamesIntList)} }}";
        }



        // Function:    GetGames()
        // Description: sends a POST request to the api with a list of the desired games to be retrieved,
        //              and return the result of the api request
        // Parameters:  N/A
        // Return:      A list of the retrieved games from the api, or null if an error is encountered.
        public static async Task<List<GameModel>?> GetGames()
        {
            try
            {
                StringContent selectedGamesContent = new StringContent(GetUserGames(), Encoding.UTF8, "application/json");

                var response = await ApiClient.PostAsync("/games", selectedGamesContent);
                var responseContent = response.Content.ReadAsStringAsync().Result;

                List<GameModel>? retrievedGames = JsonSerializer.Deserialize<List<GameModel>>(responseContent);

                return retrievedGames.OrderBy(game => game.days_left).ToList(); ;
            }
            catch (Exception ex)
            {
                return null;
            }
        }



        // Function:    GetEvents()
        // Description: sends a POST request to the api with a list of the desired events to be retrieved,
        //              and return the result of the api request
        // Parameters:  N/A
        // Return:      A list of the retrieved events from the api, or null if an error is encountered.
        public static async Task<List<EventModel>?> GetEvents()
        {
            try
            {
                StringContent selectedGamesContent = new StringContent(GetUserGames(), Encoding.UTF8, "application/json"); ;

                var response = await ApiClient.PostAsync("/events", selectedGamesContent);
                var responseContent = response.Content.ReadAsStringAsync().Result;

                List<EventModel>? retrievedEvents = JsonSerializer.Deserialize<List<EventModel>>(responseContent);

                return retrievedEvents.OrderBy(gameEvent => gameEvent.days_left).ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }



        // Function:    GetGameSelection()
        // Description: sends a GET request to the api to retrieve all ids and titles of all games 
        //              to allow the user to select which games the application will display to them
        // Parameters:  N/A
        // Return:      A list of the titles and ids of all games from the api, or null if an error is encountered.
        public static async Task<List<SelectGameModel>?> GetGameSelection()
        {
            try
            {
                var response = await ApiClient.GetAsync("/games");
                var responseContent = response.Content.ReadAsStringAsync().Result;

                List<SelectGameModel>? selectedGames = JsonSerializer.Deserialize<List<SelectGameModel>>(responseContent);

                return selectedGames.OrderBy(game => game.title).ToList(); ;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
