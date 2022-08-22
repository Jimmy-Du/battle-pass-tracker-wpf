using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using battle_pass_tracker_wpf.Models;
using battle_pass_tracker_wpf.Utilities;

namespace battle_pass_tracker_wpf.ViewModels
{
    public class BattlePassViewModel : ViewModelBase
    {
        private List<GameModel>? games;
        public List<GameModel>? Games
        {
            get { return games; }
            set
            {
                games = value;
                OnPropertyChanged(nameof(Games));
            }
        }



        public BattlePassViewModel()
        {
            SetupGames();
        }



        // Function:    SetupGames()
        // Description: makes an api request to retrieve the list of games
        // Parameters:  N/A
        // Return:      N/A
        private async void SetupGames()
        {
            Games = await BattlePassTrackerApi.GetGames();
        }
    }
}
