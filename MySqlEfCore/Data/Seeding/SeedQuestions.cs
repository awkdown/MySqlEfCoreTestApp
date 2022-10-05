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
    public class SeedQuestions
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new MyDbContext(serviceProvider.GetRequiredService<DbContextOptions<MyDbContext>>()))
            {
                // Look for any entries in the Leaderboard
                if (context.Questions.Any())
                {
                    return;   // DB has been seeded, so do nothing
                }

                List<Question> questions = new List<Question>();
                using (var reader = new StreamReader(@".\Data\Seeding\Questions.csv"))
                {
                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();
                        var values = line.Split(',');

                        Question entry = new Question();
                        entry.QuestionType = values[0];
                        entry.CategoryId = int.Parse(values[1]);
                        entry.QuestionText = values[2];
                        entry.AnswerA = values[3];
                        entry.AnswerB = values[4];
                        entry.AnswerC = values[5];
                        entry.AnswerD = values[6];
                        entry.CorrectAnswer = values[7];

                        questions.Add(entry);
                    }
                }

                context.Questions.AddRange(questions);
                context.SaveChanges();
            }
        }
    }
}
