using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Theater_De_Rinkelaar.Models;
using MySql.Data.MySqlClient;
using Theater_De_Rinkelaar.Database;
using Theater_De_Rinkelaar.Databases;
using Microsoft.AspNetCore.Http;

namespace Theater_De_Rinkelaar.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            // lijst met producten ophalen
            var products = GetAllVoorstellingen();

            // de lijst met producten in de html stoppen
            return View(products);          
        }



        public List<Voorstelling> GetAllVoorstellingen()
        {
            // alle producten ophalen uit de database
            var rows = DatabaseConnector.GetRows("SELECT agenda.id, beschikbaarheid, naam, datum, beschrijvingkort, beschrijvinglang, begintijd, eindtijd, duur, plaatje, leeftijd, voorstelling_id FROM `agenda` INNER JOIN voorstellingen ON agenda.voorstelling_id = voorstellingen.id");

            // lijst maken om alle producten in te stoppen
            List<Voorstelling> voorstellingen = new List<Voorstelling>();

            foreach (var row in rows)
            {
                // Voor elke rij maken we nu een product
                Voorstelling p = GetVoorstellingFromRow(row);

                // en dat product voegen we toe aan de lijst met producten
                voorstellingen.Add(p);
            }

            return voorstellingen;
        }

        [Route("Agenda")]
        public IActionResult Agenda()
        {
            {
                // lijst met producten ophalen
                var products = GetAllVoorstellingen();

                // de lijst met producten in de html stoppen
                return View(products);
            }
        }

        [Route("InformatieTheater")]
        public IActionResult InformatieTheater()
        {
            return View();
        }

        [Route("OnzeVoorstellingen")]
        public IActionResult OnzeVoorstellingen()
        {
            {
                // lijst met producten ophalen
                var products = GetAllVoorstellingen();

                // de lijst met producten in de html stoppen
                return View(products);
            }
        }

        [Route("Contact")]
        public IActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        [Route("Contact")]
        public IActionResult Contact(Person person)
        {
            if (ModelState.IsValid)
            {
                DatabaseConnector.SavePerson(person);
                return Redirect("/succes");
            }
            return View(person);
            
        }

        [Route("succes")]
        public IActionResult Succes()
        {
            return View();
        }

        [Route("secret")]
        public IActionResult Secret()
        {
            return View();
        }

        [Route("voorstelling/{id}")]
        public IActionResult VoorstellingDetails(int id)
        {
            var voorstelling = GetVoorstelling(id);

            return View(voorstelling);
        }

        [Route("voorstelling/{id}/tickets")]
        public IActionResult VoorstellingTickets(int id)
        {
            var voorstelling = GetVoorstelling(id);

            return View(voorstelling);
        }

        [Route("voorstelling/{id}/tickets/betalen")]
        public IActionResult VoorstellingBetalen(int id)
        {
            var voorstelling = GetVoorstelling(id);

            return View(voorstelling);
        }

        [Route("404")]
        public IActionResult PaginaNietGevonden()
        {
            return View();
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public Voorstelling GetVoorstelling(int id)
        {
            // product ophalen uit de database op basis van het idee
            var rows = DatabaseConnector.GetRows($"select agenda.id, beschikbaarheid, naam, datum, beschrijvingkort, beschrijvinglang, begintijd, eindtijd, duur, plaatje, leeftijd, voorstelling_id from agenda INNER JOIN voorstellingen ON agenda.voorstelling_id = voorstellingen.id where agenda.id = {id}");

            var row = rows[0];

            // lijst maken om alle producten in te stoppen
            Voorstelling v = new Voorstelling();
            v.Naam = row["naam"].ToString();
            v.Beschrijvingkort = row["beschrijvingkort"].ToString();
            v.Beschrijvinglang = row["beschrijvinglang"].ToString();
            v.Datum = row["datum"].ToString();
            v.Beschikbaarheid = Convert.ToInt32(row["beschikbaarheid"]);
            v.Begintijd = row["begintijd"].ToString();
            v.Eindtijd = row["eindtijd"].ToString();
            v.Duur = row["duur"].ToString();
            v.Leeftijd = row["leeftijd"].ToString();
            v.Id = Convert.ToInt32(row["id"]);
            v.Plaatje = row["plaatje"].ToString();

            return v;
        }


        private Voorstelling GetVoorstellingFromRow(Dictionary<string, object> row)
        {
            Voorstelling v = new Voorstelling();
            v.Naam = row["naam"].ToString();
            v.Beschrijvingkort = row["beschrijvingkort"].ToString();
            v.Beschrijvinglang = row["beschrijvinglang"].ToString();
            v.Duur = row["duur"].ToString();
            v.Leeftijd = row["leeftijd"].ToString();
            v.Id = Convert.ToInt32(row["id"]);
            v.Plaatje = row["plaatje"].ToString();

            return v;
        }
        public List<string> GetNames()
        {
            // stel in waar de database gevonden kan worden
            //string connectionString = "Server=172.16.160.21;Port=3306;Database=110664;Uid=110664;Pwd=inf2122sql;";
            string connectionString = "Server=informatica.st-maartenscollege.nl;Port=3306;Database=110698;Uid=110698;Pwd=inf2122sql;";


            // maak een lege lijst waar we de namen in gaan opslaan
            List<string> names = new List<string>();

            // verbinding maken met de database
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                // verbinding openen
                conn.Open();

                // SQL query die we willen uitvoeren
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM `voorstellingen`", conn);

                // resultaat van de query lezen
                using (var reader = cmd.ExecuteReader())
                {
                    // elke keer een regel (of eigenlijk: database rij) lezen
                    while (reader.Read())
                    {
                        // selecteer de kolommen die je wil lezen. In dit geval kiezen we de kolom "naam"
                        string Name = reader["beschrijving_lang"].ToString();

                        // voeg de naam toe aan de lijst met namen
                        names.Add(Name);
                    }
                }
            }

            // return de lijst met namen
            return names;
        }

        

       
    }

}
