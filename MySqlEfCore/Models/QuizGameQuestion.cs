using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MySqlEfCore.Models
{
    public class QuizGameQuestion
    {
        public Guid QuizGameQuestionId { get; set; }
        public Guid QuizGameId { get; set; }
        public Guid QuestionId { get; set; }

        // imposes an ordering on the set of questions for this game
        // ranges between 1 and QuizGame's length
        public int QuestionPosition { get; set; }


        public bool LocationHintUsed { get; set; }
        public bool QuestionHintUsed { get; set; }

    }
}
