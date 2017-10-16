using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using WebAppsOppgave1.Models;
using System.Web.Script.Serialization;

namespace WebAppsOppgave1.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            
            return View();
        }

        public string getAirportNames()
        {
            
            var Db = new BookingLogic();
            List<jsAirport> airports = Db.getAllAirports();
            var jsonSerializer = new JavaScriptSerializer();
            return jsonSerializer.Serialize(airports); 
        }

        public string getFlights(int from, int to)
        {
            var Db = new BookingLogic();
            List<Flight> matchingFlights = Db.getMatchingflights(from, to);
            var jsMatchingFlights = new List<jsFlight>();
            foreach (Flight f in matchingFlights)
            {
                var aFlight = new jsFlight()
                {
                    id = f.Id,
                    fromAirport = f.FromAirport.Name,
                    toAirport = f.ToAirport.Name,
                    departure = f.Departure.ToString("dd.MM.yyyy HH:mm"),
                    arrival = f.Arrival.ToString("dd.MM.yyy HH:mm"),
                    price = f.Price
                };
                jsMatchingFlights.Add(aFlight);
            }
            var jsonSerializer = new JavaScriptSerializer();
            return jsonSerializer.Serialize(jsMatchingFlights);
        }

        public string getFlightsOnDate(int from, int to, DateTime date)
        {
            var Db = new BookingLogic();
            List<Flight> matchingFlights = Db.getMatchingflightsOnDate(from, to, date);
            var jsMatchingFlights = new List<jsFlight>();
            foreach (Flight f in matchingFlights)
            {
                var aFlight = new jsFlight()
                {
                    id = f.Id,
                    fromAirport = f.FromAirport.Name,
                    toAirport = f.ToAirport.Name,
                    departure = f.Departure.ToString("dd.MM.yyyy HH:mm"), 
                    arrival = f.Arrival.ToString("dd.MM.yyy HH:mm")
                };
                jsMatchingFlights.Add(aFlight);
            }
            var jsonSerializer = new JavaScriptSerializer();
            return jsonSerializer.Serialize(jsMatchingFlights);
        }

        public string Register(JsBooking jsBooking)
        {
            var jsonSerializer = new JavaScriptSerializer();

            using (var Db = new DB())
            {
                try
                {
                    var booking = new Booking
                    {
                        Flight = Db.Flight.Single(f => f.Id == jsBooking.flight),
                        Amount = jsBooking.amount
                    };

                    Db.Booking.Add(booking);
                    Db.SaveChanges();
                }
                catch (Exception e)
                {
                    Console.WriteLine("Feil under skriving til DB. Lær deg hvordan du håndterer exceptions"+e.Message);
                    return jsonSerializer.Serialize("failed");
                }
            }
            
            return jsonSerializer.Serialize("ok");            
        }

        public ActionResult RegisterUser()
        {
            return View();
        }

        [HttpPost]
        public ActionResult RegisterUser(FormCollection form)
        {
            try
            {
                using (var db = new DB())
                {
                    var user = new User();
                    user.Fornavn = form["fornavn"];
                    user.Etternavn = form["etternavn"];
                    user.Adresse = form["adresse"];
                    user.Postnummer = form["postnummer"];
                    user.Epost = form["epost"];
                    user.PassordHash = HashPassword(form["passord"]);

                    var InputPostnummer = form["postnummer"];

                    var PoststedFound = db.Poststed.FirstOrDefault(p => p.Postnr == InputPostnummer);

                    if (PoststedFound == null)
                    {
                        var Poststed = new PostSted();
                        Poststed.Postnr = form["postnummer"];
                        Poststed.Poststed = form["poststed"];
                        db.Poststed.Add(Poststed);
                    }

                    db.Users.Add(user);
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("FEIL UNDER REGISTRERING: " + e.Message);
                return View();
            }
        }

        public static byte[] HashPassword(string p)
        {
            byte[] input, output;
            var hashing = System.Security.Cryptography.SHA256.Create();
            input = System.Text.Encoding.ASCII.GetBytes(p);
            output = hashing.ComputeHash(input);

            return output;
        }

        public ActionResult Bookings()
        {
            using (var Db = new DB())
            {
                List<Booking> Bookings = Db.Booking.ToList();

                foreach(Booking b in Bookings)
                {
                    b.Flight = Db.Flight.Include(f => f.FromAirport).Include(f => f.ToAirport).Single(f => f.Id == b.Id);
                    b.TotalPrice = b.Flight.Price * b.Amount;
                }

                return View(Bookings);
            }
        }

        public ActionResult Delete(int id)
        {
            using (var Db = new DB())
            {
                try
                {
                    Models.Booking DeleteBooking = Db.Booking.SingleOrDefault(k => k.Id == id);
                    Db.Booking.Remove(DeleteBooking);
                    Db.SaveChanges();
                }
                catch (Exception e)
                {
                    Console.WriteLine("Feil under sletting av ordre. Lær deg hvordan du håndterer exceptions"+e.Message);
                }
            }
            return RedirectToAction("Orders");
        }
    }
}