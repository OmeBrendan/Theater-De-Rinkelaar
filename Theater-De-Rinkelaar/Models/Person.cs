using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Theater_De_Rinkelaar.Models
{
    public class Person
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Adress { get; set; }
        public string Description { get; set; }
    }
}
