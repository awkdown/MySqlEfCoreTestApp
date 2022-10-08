using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MySqlEfCore.Models
{
    public class Question
    {
        public Guid QuestionId { get; set; }
        public string QuestionType { get; set; }
        public int CategoryId { get; set; }
        public string QuestionText { get; set; }
        public string AnswerA { get; set; }
        public string AnswerB { get; set; }
        public string AnswerC { get; set; }
        public string AnswerD { get; set; }
        public string CorrectAnswer { get; set; }
        public bool UseLatLong  { get; set; }
        public double? QuestionLatitude { get; set; }
        public double? QuestionLongitude { get; set; }
        public string Hint { get; set; }
    }
}
