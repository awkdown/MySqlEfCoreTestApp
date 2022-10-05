using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MySqlEfCore.Data
{
    //Object returned from GET categories by CategoryController
    public class CategoryInfo
    {
        public string CategoryName { get; set; }
        public List<int> Options { get; set; }
    }
}
