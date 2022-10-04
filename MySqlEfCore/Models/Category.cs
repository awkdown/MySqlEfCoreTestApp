﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MySqlEfCore.Models
{
    public class Category
    {
        
        public Guid Id { get; set; }
                
        public int CategoryId { get; set; }

        public string CategoryName { get; set; }
    }
}
