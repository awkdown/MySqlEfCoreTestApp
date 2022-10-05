using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MySqlEfCore.Data
{
    // Object received from client when starting a new quiz
    public class NewQuizInfo
    {
        public string PlayerName { get; set; }
        public string AppID { get; set; }
        public string CategoryName { get; set; }
        public int QuizLength { get; set; }

    }
}
