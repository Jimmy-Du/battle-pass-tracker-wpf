using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using battle_pass_tracker_wpf.Models;
using battle_pass_tracker_wpf.Utilities;

namespace battle_pass_tracker_wpf.ViewModels
{
    public class EventViewModel : ViewModelBase
    {
        private List<EventModel>? events;
        public List<EventModel>? Events
        {
            get { return events; }
            set
            {
                events = value;
                OnPropertyChanged(nameof(Events));
            }
        }



        public EventViewModel()
        {
            SetupEvents();
        }



        // Function:    SetupEvents()
        // Description: makes an api request to retrieve the list of events
        // Parameters:  N/A
        // Return:      N/A
        public async void SetupEvents()
        {
            Events = await BattlePassTrackerApi.GetEvents();
        }
    }
}
