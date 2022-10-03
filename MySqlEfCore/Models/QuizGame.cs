using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MySqlEfCore.Models
{
    public class QuizGame
    {
        [Key]
        public int QuizGameId { get; set; }
        public int AppId { get; set; }
        public int PlayerId { get; set; }
        public int CategoryId { get; set; }
        public int QuizGameLength { get; set; }
        public int CurrentQuestionPosition { get; set; }
        public int Score { get; set; }

//        public bool AdvancedOption { get; set; }

    }
}
