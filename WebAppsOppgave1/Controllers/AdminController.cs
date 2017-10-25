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
        private IAdminBLL _adminBLL;

        public AdminController()
        {
            _adminBLL = new AdminBLL();
        }

        public AdminController(IAdminBLL stub)
        {
            _adminBLL = stub;
        }

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

        public bool AdminInDb(FormCollection form)
        {
            
            var UserName = form["username"];
            byte[] HashedPassword = HashPassword(form["password"]);
            var AdminUser = _adminBLL.AdminInDb(UserName, HashedPassword);

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
            var airports = _adminBLL.getAllAirports();
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
            var airport = _adminBLL.getAirport(id);
            if (airport != null)
            {
                var jsAirport = new jsAirport()
                {
                    id = airport.Id,
                    name = airport.Name
                };

                var jsonSerializer = new JavaScriptSerializer();
                return jsonSerializer.Serialize(jsAirport);
            }

            return null;
        }

        public string registerAirport(string name)
        {
            var jsonSerializer = new JavaScriptSerializer();
            return jsonSerializer.Serialize(_adminBLL.registerAirport(name));
        }
        public string editAirport(int id, string name)
        {
            var jsonSerializer = new JavaScriptSerializer();
            return jsonSerializer.Serialize(_adminBLL.editAirport(id, name));
        }
        public string deleteAirport(int id)
        {
            var jsonSerializer = new JavaScriptSerializer();
            return jsonSerializer.Serialize(_adminBLL.deleteAirport(id));
        }


        public string getAllFlights()
        {
            var flights = _adminBLL.getAllFlights();
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
            var flight = _adminBLL.getFlight(id);
            if (flight != null)
            {
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

            return null;
        }
        public string registerFlight(int fromAirportId, int toAirportId, DateTime departure, DateTime arrival, int price)
        {
            var jsonSerializer = new JavaScriptSerializer();
            return jsonSerializer.Serialize(_adminBLL.registerFlight(fromAirportId, toAirportId, departure, arrival, price));
        }
        public string editFlight(int id, int fromAirportId, int toAirportId, DateTime departure, DateTime arrival, int price)
        {
            var jsonSerializer = new JavaScriptSerializer();
            return jsonSerializer.Serialize(_adminBLL.editFlight(id, fromAirportId, toAirportId, departure, arrival, price));
        }
        public string deleteFlight(int id)
        {
            var jsonSerializer = new JavaScriptSerializer();
            return jsonSerializer.Serialize(_adminBLL.deleteFlight(id));
        }


        public string getAllBookings()
        {
            var bookings = _adminBLL.getAllBookings();
            var jsBookings = new List<JsBooking>();
            foreach (Booking b in bookings)
            {
                var aBooking = new JsBooking()
                {
                    Id = b.Id,
                    UserId = b.User.Id,
                    UserFirstname = b.User.Fornavn,
                    UserLastname = b.User.Etternavn,
                    FlightId = b.Flight.Id,
                    FlightFrom = b.Flight.FromAirport.Name,
                    FlightTo = b.Flight.ToAirport.Name,
                    FlightDeparture = b.Flight.Departure.ToString("dd.MM.yyyy HH:mm"),
                    Amount = b.Amount
                };
                jsBookings.Add(aBooking);
            }
            var jsonSerializer = new JavaScriptSerializer();
            return jsonSerializer.Serialize(jsBookings);
        }
        public string getBooking(int id)
        {
            var booking = _adminBLL.getBooking(id);
            if (booking != null)
            {
                var jsBooking = new JsBooking()
                {
                    Id = booking.Id,
                    UserId = booking.User.Id,
                    UserFirstname = booking.User.Fornavn,
                    UserLastname = booking.User.Etternavn,
                    FlightId = booking.Flight.Id,
                    FlightFrom = booking.Flight.FromAirport.Name,
                    FlightTo = booking.Flight.ToAirport.Name,
                    FlightDeparture = booking.Flight.Departure.ToString("dd.MM.yyyy HH:mm"),
                    Amount = booking.Amount
                };
                var jsonSerializer = new JavaScriptSerializer();
                return jsonSerializer.Serialize(jsBooking);
            }

            return null;
        }
        public string registerBooking(int userId, int flightId, int amount)
        {
            var jsonSerializer = new JavaScriptSerializer();
            return jsonSerializer.Serialize(_adminBLL.registerBooking(userId, flightId, amount));
        }
        public string editBooking(int id, int userId, int flightId, int amount)
        {
            var jsonSerializer = new JavaScriptSerializer();
            return jsonSerializer.Serialize(_adminBLL.editBooking(id, userId, flightId, amount));
        }
        public string deleteBooking(int id)
        {
            var jsonSerializer = new JavaScriptSerializer();
            return jsonSerializer.Serialize(_adminBLL.deleteBooking(id));
        }

        public string getAllUsers()
        {
            var users = _adminBLL.getAllUsers();
            var jsUsers = new List<JsUser>();
            foreach (User u in users)
            {
                var aUser = new JsUser()
                {
                    Id = u.Id,
                    Fornavn = u.Fornavn,
                    Etternavn = u.Etternavn,
                    Adresse = u.Adresse,
                    Postnummer = u.Poststed.Postnr,
                    Poststed = u.Poststed.Poststed,
                    Epost = u.Epost
                };
                jsUsers.Add(aUser);
            }
            var jsonSerializer = new JavaScriptSerializer();
            return jsonSerializer.Serialize(jsUsers);
        }
        public string getUser(int id)
        {
            var user = _adminBLL.getUser(id);
            if (user != null)
            {
                var jsUser = new JsUser()
                {
                    Id = user.Id,
                    Fornavn = user.Fornavn,
                    Etternavn = user.Etternavn,
                    Adresse = user.Adresse,
                    Postnummer = user.Poststed.Postnr,
                    Poststed = user.Poststed.Poststed,
                    Epost = user.Epost
                };
                var jsonSerializer = new JavaScriptSerializer();
                return jsonSerializer.Serialize(jsUser);
            }

            return null;
        }
        public string registerUser(string fornavn, string etternavn, string adresse, string postnummer, string poststed, string epost, string passord)
        {
            var hashedPassword = HashPassword(passord);
            var jsonSerializer = new JavaScriptSerializer();
            return jsonSerializer.Serialize(_adminBLL.registerUser(fornavn, etternavn, adresse, postnummer, poststed, epost, hashedPassword));
        }
        public string editUser(int id, string fornavn, string etternavn, string adresse, string postnummer, string poststed, string epost, string passord)
        {
            var hashedPassword = HashPassword(passord);
            var jsonSerializer = new JavaScriptSerializer();
            return jsonSerializer.Serialize(_adminBLL.editUser(id, fornavn, etternavn, adresse, postnummer, poststed, epost, hashedPassword));
        }
        public string deleteUser(int id)
        {
            var jsonSerializer = new JavaScriptSerializer();
            return jsonSerializer.Serialize(_adminBLL.deleteUser(id));
        }
    }
}