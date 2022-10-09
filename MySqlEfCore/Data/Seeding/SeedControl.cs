using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySqlEfCore.Models;
using System.IO;

namespace MySqlEfCore.Data.Seeding
{
    public class SeedControl
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new MyDbContext(serviceProvider.GetRequiredService<DbContextOptions<MyDbContext>>()))
            {
                // Look for any entries in the Leaderboard
                if (context.Controls.Any())
                {
                    return;   // DB has been seeded, so do nothing
                }

                Control controlVariables = new Control
                {
                    GPSRadius = 100, // default 100m
                    THOver = false
                };

                context.Controls.Add(controlVariables);
                context.SaveChanges();
            }
        }

    }
}
