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
            if (Session["LoggedIn"] == null)
            {
                Session["LoggedIn"] = false;
                ViewBag.LoggedIn = false;
            }
            else
            {
                ViewBag.LoggedIn = (bool)Session["LoggedIn"];
            }

            return View();
        }

        [HttpPost]
        public ActionResult Index(FormCollection form)
        {
            if (UserInDb(form))
            {
                Session["LoggedIn"] = true;
                ViewBag.LoggedIn = true;
                return View();
            }
            else
            {
                Session["LoggedIn"] = false;
                ViewBag.LoggedIn = false;
                return View();
            }
        }

        private static bool UserInDb(FormCollection form)
        {
            using (var db = new DB())
            {
                var UserName = form["username"];
                byte[] HashedPassword = HashPassword(form["password"]);
                var User = db.Users.FirstOrDefault(u => u.PassordHash == HashedPassword && u.Epost == UserName);

                if (User == null)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        public ActionResult LogIn()
        {
            return View();
        }
        public ActionResult LogOut()
        {
            ViewBag.LoggedIn = false;
            Session["LoggedIn"] = false;

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
                    var InputPostnummer = db.Poststed.Find(form["postnummer"]);
                    if (InputPostnummer == null)
                    {
                        InputPostnummer = new PostSted();
                        InputPostnummer.Postnr = form["postnummer"];
                        InputPostnummer.Poststed = form["poststed"];
                        db.Poststed.Add(InputPostnummer);
                    }

                    var user = new User();
                    user.Fornavn = form["fornavn"];
                    user.Etternavn = form["etternavn"];
                    user.Adresse = form["adresse"];
                    user.Poststed = InputPostnummer;
                    user.Epost = form["epost"];
                    user.PassordHash = HashPassword(form["passord"]);

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