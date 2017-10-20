using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using WebAppsOppgave1.Model;
using WebAppsOppgave1.BLL;
using System.Web.Script.Serialization;

namespace WebAppsOppgave1.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            if ((bool)Session["Admin"] == false)
            {
                return RedirectToAction("LogIn");
            }

            if (Session["Admin"] == null)
            {
                Session["Admin"] = false;
                ViewBag.Admin = false;

                return RedirectToAction("LogIn");
            }
            else
            {
                ViewBag.Admin = (bool)Session["Admin"];
                return View();
            }            
        }

        [HttpPost]
        public ActionResult Index(FormCollection form)
        {
            if (AdminInDb(form))
            {
                Session["Admin"] = true;
                ViewBag.Admin = true;
                return View();
            }
            else
            {
                Session["Admin"] = false;
                ViewBag.Admin = false;
                return RedirectToAction("LogIn");
            }
        }

        public ActionResult LogIn()
        {
            return View();
        }

        public ActionResult LogOut()
        {
            ViewBag.Admin = false;
            Session["Admin"] = false;

            return View();
        }

        private static bool AdminInDb(FormCollection form)
        {
            var AdminBLL = new AdminBLL();
            
            var UserName = form["username"];
            byte[] HashedPassword = HashPassword(form["password"]);
            var AdminUser = AdminBLL.AdminInDb(UserName, HashedPassword);

            if (AdminUser == null)
            {
                return false;
            }
            else
            {
                return true;
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

        public string getAllAirports()
        {
            var AdminBLL = new AdminBLL();
            var airports = AdminBLL.getAllAirports();
            var jsAirports = new List<jsAirport>();
            foreach (Airport a in airports)
            {
                var anAirport = new jsAirport()
                {
                    id = a.Id,
                    name = a.Name
                };
                jsAirports.Add(anAirport);
            }
            var jsonSerializer = new JavaScriptSerializer();
            return jsonSerializer.Serialize(jsAirports);
        }

        public string getAirport(int id)
        {
            var AdminBLL = new AdminBLL();
            var airport = AdminBLL.getAirport(id);
            var jsAirport = new jsAirport()
            {
                id = airport.Id,
                name = airport.Name
            };
            var jsonSerializer = new JavaScriptSerializer();
            return jsonSerializer.Serialize(jsAirport);
        }

        public string registerAirport(string name)
        {
            var AdminBLL = new AdminBLL();
            var jsonSerializer = new JavaScriptSerializer();
            return jsonSerializer.Serialize(AdminBLL.registerAirport(name));
        }
        public string editAirport(int id, string name)
        {
            var AdminBLL = new AdminBLL();
            var jsonSerializer = new JavaScriptSerializer();
            return jsonSerializer.Serialize(AdminBLL.editAirport(id, name));
        }
        public string deleteAirport(int id)
        {
            var AdminBLL = new AdminBLL();
            var jsonSerializer = new JavaScriptSerializer();
            return jsonSerializer.Serialize(AdminBLL.deleteAirport(id));
        }

        public string getAllFlights()
        {
            var AdminBLL = new AdminBLL();
            var flights = AdminBLL.getAllFlights();
            var jsFlights = new List<jsFlight>();
            foreach (Flight f in flights)
            {
                var aFlight = new jsFlight()
                {
                    id = f.Id,
                    fromAirportName = f.FromAirport.Name,
                    toAirportName = f.ToAirport.Name,
                    departure = f.Departure.ToString("dd.MM.yyyy HH:mm"),
                    arrival = f.Arrival.ToString("dd.MM.yyy HH:mm"),
                    price = f.Price
                };
                jsFlights.Add(aFlight);
            }
            var jsonSerializer = new JavaScriptSerializer();
            return jsonSerializer.Serialize(jsFlights);
        }
        public string getFlight(int id)
        {
            var AdminBLL = new AdminBLL();
            var flight = AdminBLL.getFlight(id);
                var jsFlight = new jsFlight()
                {
                    id = flight.Id,
                    fromAirportId = flight.FromAirport.Id,
                    fromAirportName = flight.FromAirport.Name,
                    toAirportId = flight.ToAirport.Id,
                    toAirportName = flight.ToAirport.Name,
                    departure = flight.Departure.ToString("dd.MM.yyyy HH:mm"),
                    arrival = flight.Arrival.ToString("dd.MM.yyy HH:mm"),
                    price = flight.Price
                };
                var jsonSerializer = new JavaScriptSerializer();
                return jsonSerializer.Serialize(jsFlight);
        }
        public string registerFlight(int fromAirportId, int toAirportId, DateTime departure, DateTime arrival, int price)
        {
            var AdminBLL = new AdminBLL();
            var jsonSerializer = new JavaScriptSerializer();
            return jsonSerializer.Serialize(AdminBLL.registerFlight(fromAirportId, toAirportId, departure, arrival, price));
        }
        public string editFlight(int id, int fromAirportId, int toAirportId, DateTime departure, DateTime arrival, int price)
        {
            var AdminBLL = new AdminBLL();
            var jsonSerializer = new JavaScriptSerializer();
            return jsonSerializer.Serialize(AdminBLL.editFlight(id, fromAirportId, toAirportId, departure, arrival, price));
        }
        public string deleteFlight(int id)
        {
            var AdminBLL = new AdminBLL();
            var jsonSerializer = new JavaScriptSerializer();
            return jsonSerializer.Serialize(AdminBLL.deleteFlight(id));
        }
    }
}