using System.Collections.Generic;
using System.Text.Json;
using battle_pass_tracker_wpf.Models;
using battle_pass_tracker_wpf.Utilities;
using battle_pass_tracker_wpf.Commands;

namespace battle_pass_tracker_wpf.ViewModels
{
    public class GameSelectionViewModel : ViewModelBase
    {
        public RelayCommand SelectCommand { get; set; }

        private List<SelectGameModel>? gameSelectionList;
        public List<SelectGameModel>? GameSelectionList
        {
            get { return gameSelectionList; }
            set
            {
                gameSelectionList = value;
                OnPropertyChanged(nameof(GameSelectionList));
            }
        }

        public List<string>? SelectedGames { get; set; } = new List<string>();



        public GameSelectionViewModel()
        {
            // if user game preferences exist, their preferences will be loaded
            if (JsonSerializer.Deserialize<List<string>>(Properties.Settings.Default.SelectedGames) != null)
            {
                SelectedGames = JsonSerializer.Deserialize<List<string>>(Properties.Settings.Default.SelectedGames);
            }

            SetupGameSelection();
            SelectCommand = new RelayCommand(SelectGame, SelectCanUse);
        }



        // Function:    SetupGameSelection()
        // Description: makes an api request to retrieve the list of games to select from
        // Parameters:  N/A
        // Return:      N/A
        public async void SetupGameSelection()
        {
            GameSelectionList = await BattlePassTrackerApi.GetGameSelection();

            // for each loop to load the user's previously selected games
            foreach (SelectGameModel game in GameSelectionList)
            {
                if (SelectedGames.Contains(game.id))
                {
                    game.isSelected = true;
                }
            }
        }



        // Function:    SelectGame()
        // Description: adds or removes a game from the user's game selection to determine
        //              which games will be displayed on the battle passes and events screens
        // Parameters:  object gameId: an object containing the game id 
        //              of the game that was selected/deselected
        // Return:      N/A
        public void SelectGame(object gameId)
        {
            // if the game selected is already in the selected games list,
            // it will be removed from the list
            if (SelectedGames.Contains((string) gameId))
            {
                SelectedGames.Remove((string) gameId);
            }
            // else, the selected game is added to the list
            else
            {
                SelectedGames.Add((string) gameId);
            }

            Properties.Settings.Default.SelectedGames = JsonSerializer.Serialize(SelectedGames);
            Properties.Settings.Default.Save();
        }



        // Function:    SelectCanUse()
        // Description: determines if the Select Game Command can be executed 
        // Parameters:  object gameId: an object containing the game id 
        //              of the game that was selected/deselected
        // Return:      a boolean indiciting if the Select Game Command can be executed
        public static bool SelectCanUse(object gameId)
        {
            return true;
        }
    }
}
