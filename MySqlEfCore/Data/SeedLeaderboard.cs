using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySqlEfCore.Models;
using System.IO;

namespace MySqlEfCore.Data
{
    public class SeedLeaderboard
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new MyDbContext(serviceProvider.GetRequiredService<DbContextOptions<MyDbContext>>()))
            {
                // Look for any entries in the Leaderboard
                if (context.LeaderboardEntries.Any())
                {
                    return;   // DB has been seeded, so do nothing
                }

                List<LeaderboardEntry> leaderboardEntries = new List<LeaderboardEntry>();
                using (var reader = new StreamReader(@".\Data\LeaderboardEntries.csv"))
                {
                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();
                        var values = line.Split(',');

                        LeaderboardEntry entry = new LeaderboardEntry();

                        entry.LeaderboardId = int.Parse(values[0]);
                        entry.TeamCode = values[1];
                        entry.TeamName = values[2];
                        entry.Score = int.Parse(values[3]);
                        entry.League = int.Parse(values[4]);

                        leaderboardEntries.Add(entry);
                    }
                }

                context.LeaderboardEntries.AddRange(
                    leaderboardEntries
                );
                context.SaveChanges();
            }
        }
    }
}
