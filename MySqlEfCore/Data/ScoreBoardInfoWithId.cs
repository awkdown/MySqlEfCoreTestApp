using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MySqlEfCore.Data
{
    public class ScoreBoardInfoWithId
    {
        public string AppId { get; set; };
        public string Name { get; set; }
        public int Score { get; set; }
    }
}
