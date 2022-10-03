using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MySqlEfCore.Models
{
    public class LeaderboardEntry
    {
        [Key]
        public int LeaderboardId { get; set; }

        //this is the letter/number code A2 etc.
        public string TeamCode { get; set; }
        public string TeamName { get; set; }
        public int Score { get; set; }

        // Numbered 1 - 4
        public int League { get; set; }
    }
}
