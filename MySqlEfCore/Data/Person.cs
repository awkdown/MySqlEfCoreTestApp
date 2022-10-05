using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

// FOR TESTING WITH HOME CONTROLLER

namespace MySqlEfCore.Data
{
    public class Person
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(50)]
        public string FirstName { get; set; }

        [MaxLength(50)]
        public string LastName { get; set; }
        public int Age { get; set; }
        //public int ID { get; set; }
        public bool IsPlayer { get; set; }
    }
}
