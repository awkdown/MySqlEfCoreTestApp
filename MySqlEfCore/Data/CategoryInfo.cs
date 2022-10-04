using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MySqlEfCore.Data
{
    public class CategoryInfo
    {
        public string CategoryName { get; set; }
        public List<int> Options { get; set; }
    }
}
