using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebAppsOppgave1.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(Models.Booking booking)
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
                    Console.WriteLine("Feil under skriving til DB. Lær deg hvordan du håndterer exceptions");
                }
            }
            return RedirectToAction("Orders");
        }

        public ActionResult Orders()
        {
            using (var Db = new Models.DB())
            {
                List<Models.Booking> Orders = Db.Booking.ToList();
                return View(Orders);
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
                    Console.WriteLine("Feil under sletting av ordre. Lær deg hvordan du håndterer exceptions");
                }
            }
            return RedirectToAction("Orders");
        }
    }
}