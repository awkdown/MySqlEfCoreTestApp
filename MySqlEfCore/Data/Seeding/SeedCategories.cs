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
    public class SeedCategories
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new MyDbContext(serviceProvider.GetRequiredService<DbContextOptions<MyDbContext>>()))
            {
                // Look for any entries in the Leaderboard
                if (context.Categories.Any())
                {
                    return;   // DB has been seeded, so do nothing
                }

                List<Category> categories = new List<Category>();
                using (var reader = new StreamReader(Path.Combine(".", "Data", "Seeding", "Categories.csv")))
                {
                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();
                        var values = line.Split(',');

                        Category entry = new Category();
                        // entry.Id = int.Parse(values[0]);
                        entry.CategoryId = int.Parse(values[0]);
                        entry.CategoryName = values[1];

                        categories.Add(entry);
                    }
                }

                context.Categories.AddRange(categories);
                context.SaveChanges();
            }
        }


    }
}
