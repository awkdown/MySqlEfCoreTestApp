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
    public class SeedQuizCategoryLength
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new MyDbContext(serviceProvider.GetRequiredService<DbContextOptions<MyDbContext>>()))
            {
                // Look for any entries in the Leaderboard
                if (context.QuizCategoryLengths.Any())
                {
                    return;   // DB has been seeded, so do nothing
                }

                List<QuizCategoryLength> lengths = new List<QuizCategoryLength>();
                using (var reader = new StreamReader(@".\Data\QuizCategoryLengths.csv"))
                {
                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();
                        var values = line.Split(',');

                        QuizCategoryLength entry = new QuizCategoryLength();
                        // entry.Id = int.Parse(values[0]);
                        entry.CategoryId = int.Parse(values[0]);
                        entry.NumberOfQuestions = int.Parse(values[1]);

                        lengths.Add(entry);
                    }
                }

                context.QuizCategoryLengths.AddRange(lengths);
                context.SaveChanges();
            }
        }

    }
}
