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
                    arrival = f.Arrival.ToString("dd.MM.yyy HH:mm")
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

        public ActionResult Orders()
        {
            using (var Db = new DB())
            {
                List<Flight> Flights = Db.Flight.Include(c => c.FromAirport).Include(c => c.ToAirport).ToList();
                return View(Flights);
            }
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