using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Theater_De_Rinkelaar.Models
{
    public class Person
    {
        [Required(ErrorMessage = "<--Gelieve uw voornaam in te vullen")]
        [Display(Name = "Voornaam*")]
        public string FirstName { get; set; }
        
        [Required(ErrorMessage = "<--Gelieve uw achternaam in te vullen")]
        [Display(Name = "Achternaam*")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "<--Een Emailadres in vullen is verplicht")]
        [EmailAddress(ErrorMessage = "<--Geen geldig email adres")]
        [Display(Name = "Email*")]
        public string Email { get; set; }

        [Display(Name = "Telefoonnummer")]
        public string Phone { get; set; }

        [Display(Name = "Adres")]
        public string Address { get; set; }

        [Required(ErrorMessage = "<--Je kan geen leeg bericht op sturen")]
        [Display(Name = "Bericht*")]
        public string Description { get; set; }
    }
}
