using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace battle_pass_tracker_wpf.Models
{
    public class EventModel
    {
        public string id { get; set; }
        public string event_title { get; set; }
        public string title { get; set; }
        public DateTime event_start_date { get; set; }
        public DateTime event_end_date { get; set; }
        public int days_left
        {
            get => (event_end_date.Date - DateTime.Today.Date).Days;
        }
    }
}
