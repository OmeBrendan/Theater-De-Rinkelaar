using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Theater_De_Rinkelaar.Database;

namespace Theater_De_Rinkelaar.Databases
{
    public class Voorstelling
    {
        public int Id { get; set; }

        public string? Naam { get; set; }

        public string? Beschrijvingkort { get; set; }

        public string? Beschrijvinglang { get; set; }

        public string Datum { get; set; }

        public int Beschikbaarheid { get; set; }

        public string? Begintijd { get; set; }
        public string? Eindtijd { get; set; }
        public string? Duur { get; set; }
        public string? Plaatje { get; set; }
    }

    
}

