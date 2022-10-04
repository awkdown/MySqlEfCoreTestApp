using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MySqlEfCore.Models
{
    public class QuizCategoryLength
    {
        [Key]
        public Guid QuizCategoryLengthId { get; set; }
        public int CategoryId { get; set; }
        public int NumberOfQuestions { get; set; }
    }
}
