using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MySqlEfCore.Models
{
    public class QuizGame
    {
        public Guid QuizGameId { get; set; }
        public string AppId { get; set; }
        public Guid PlayerId { get; set; }
        public int CategoryId { get; set; }
        public int QuizGameLength { get; set; }
        public int CurrentQuestionPosition { get; set; }
        public int Score { get; set; }

//        public bool AdvancedOption { get; set; }

    }
}
