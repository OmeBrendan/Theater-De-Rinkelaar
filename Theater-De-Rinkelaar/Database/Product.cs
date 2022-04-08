using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Theater_De_Rinkelaar.Database;

namespace Theater_De_Rinkelaar.Databases
{
    public class Product
    {
        public int Id { get; set; }

        public string? Naam { get; set; }

        public string? Prijs { get; set; }

        public int Beschikbaarheid { get; set; }
    }

    
}

