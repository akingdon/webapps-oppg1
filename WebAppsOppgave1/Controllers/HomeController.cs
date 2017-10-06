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

        public string getFlights(int from, int to, DateTime date)
        {
            var Db = new BookingLogic();
            List<Flight> matchingFlights = Db.getMatchingflights(from, to, date);
            var jsMatchingFlights = new List<jsFlight>();
            foreach (Flight f in matchingFlights)
            {
                var aFlight = new jsFlight()
                {
                    id = f.Id,
                    fromAirport = f.FromAirport.Name,
                    toAirport = f.ToAirport.Name,
                    departure = f.Departure.ToString()
                };
                jsMatchingFlights.Add(aFlight);
            }
            var jsonSerializer = new JavaScriptSerializer();
            return jsonSerializer.Serialize(jsMatchingFlights);
        }

        [HttpPost]
        public ActionResult Register(Models.Booking booking)
        {

            var bookingLogic = new Models.BookingLogic();
            bool hasValidatedCorrectly = false;

         /*   if (bookingLogic.DestinationsAreSet(booking) && 
                bookingLogic.DestinationsAreDifferent(booking) && 
                bookingLogic.DepartureDateIsBeforeReturnDate(booking) && 
                !bookingLogic.DepartureDateIsBeforeToday(booking))
            {*/
                hasValidatedCorrectly = true;
            //}

            if (hasValidatedCorrectly)
            {
                using (var Db = new Models.DB())
                {
                    try
                    {
                        Db.Booking.Add(booking);
                        Db.SaveChanges();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Feil under skriving til DB. Lær deg hvordan du håndterer exceptions"+e.Message);
                    }
                }
                return RedirectToAction("Orders");
            }
            else
            {
                return View("ValidationFailed");
            }
            
        }

        public ActionResult Orders()
        {
            using (var Db = new Models.DB())
            {
                List<Models.Flight> Flights = Db.Flight.Include(c => c.FromAirport).Include(c => c.ToAirport).ToList();
                return View(Flights);
            }
        }

        public ActionResult Delete(int id)
        {
            using (var Db = new Models.DB())
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