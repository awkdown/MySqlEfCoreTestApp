using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MySqlEfCore.Data
{
    //Object returned from GET leaderboard by LeaderboardController
    public class LeaderboardRow
    {
        public string TeamCode { get; set; }
        public string TeamName { get; set; }
        public int Score { get; set; }

        // Numbered 1 - 4
        public int League { get; set; }
    }
}
