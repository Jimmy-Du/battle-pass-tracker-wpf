using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace battle_pass_tracker_wpf.Models
{
    public class GameModel
    {
        public string id { get; set; }
        public string title { get; set; }
        public string season_title { get; set; }
        public DateTime season_start_date { get; set; }
        public DateTime season_end_date { get; set; }
        public int days_left 
        {
            get => (season_end_date.Date - DateTime.Today.Date).Days;
        }
    }
}
