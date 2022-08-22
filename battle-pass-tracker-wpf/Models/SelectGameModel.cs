using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace battle_pass_tracker_wpf.Models
{
    public class SelectGameModel
    {
        public string? id { get; set; }
        public string? title { get; set; }
        public bool isSelected { get; set; } = false;
    }
}
