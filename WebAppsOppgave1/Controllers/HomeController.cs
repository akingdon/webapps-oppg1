using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace WebAppsOppgave1.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            var db = new Models.DB();
            IEnumerable<Models.Airport> airports = db.Airport;
            return View(airports);
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