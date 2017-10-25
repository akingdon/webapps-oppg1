using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAppsOppgave1.Model;

namespace WebAppsOppgave1.DAL
{
    public class AdminDAL : IAdminDAL
    {
        private readonly string LogPath = AppDomain.CurrentDomain.BaseDirectory + "\\logs\\";
        private const string LOG_EVENTS = "log_events.txt";
        private const string LOG_ERRORS = "log_errors.txt";

        public AdminUser AdminInDb(string UserName, byte[] HashedPassword)
        {
            DB Db = new DB();
            AdminUser user = Db.Admins.FirstOrDefault(a => a.PassordHash == HashedPassword && a.Epost == UserName);
            return user;
        }
        public List<Airport> getAllAirports()
        {
            DB Db = new DB();
            return Db.Airport.ToList();
        }
        public Airport getAirport(int id)
        {
            DB Db = new DB();
            return Db.Airport.Find(id);
        } 
        public string registerAirport(string name)
        {
            DB Db = new DB();
            try
            {
                var airport = new Airport {Name = name};

                Db.Airport.Add(airport);
                Db.SaveChanges();
                int id = airport.Id;

                WriteLogEvent("Added new airport: " + name + " with id: " + id);

                return "ok";
            }
            catch (Exception e)
            {
                WriteLogError("Adding new airport failed: " + e.Message);
                return "Adding to DB failed";
            }
        }
        public string editAirport(int id, string name)
        {
            DB Db = new DB();
            try
            {
                var airportToEdit = Db.Airport.Find(id);
                string OldName = airportToEdit.Name;

                airportToEdit.Name = name;
                Db.SaveChanges();

                WriteLogEvent("Edited airport id: " + airportToEdit.Id + ". Name changed from " + OldName + " to " + name);

                return "ok";
            }
            catch (Exception e)
            {
                WriteLogError("Could not edit airport: " + e.Message);
                return "Editing in DB failed";
            }
        }
        public string deleteAirport(int id)
        {
            DB Db = new DB();
            try
            {
                var airportToDelete = Db.Airport.Find(id);
                Db.Airport.Remove(airportToDelete);
                Db.SaveChanges();

                WriteLogEvent("Deleted airport id: " + airportToDelete.Id + " name: " + airportToDelete.Name);

                return "ok";
            }
            catch (Exception e)
            {
                WriteLogError("Could not delete airport: " + e.Message);
                return "Deleting from DB failed";
            }
        }

        public List<Flight> getAllFlights()
        {
            DB Db = new DB();
            return Db.Flight.ToList();
        }
        public Flight getFlight(int id)
        {
            DB Db = new DB();
            return Db.Flight.Find(id);
        }
        public string RegisterFlight(int fromAirportId, int toAirportId, DateTime departure, DateTime arrival, int price)
        {
            DB Db = new DB();
            try
            {
                var fromAirport = Db.Airport.Find(fromAirportId);
                var toAirport = Db.Airport.Find(toAirportId);
                
                var flight = new Flight()
                {
                    ToAirport = toAirport,
                    FromAirport = fromAirport,
                    Departure = departure,
                    Arrival = arrival,
                    Price = price
                };
                Db.Flight.Add(flight);
                Db.SaveChanges();

                WriteLogEvent("Added new flight: From " + fromAirport.Name + " " + departure + ", to " + toAirport.Name + " " + arrival + ", price: " + price + " (id: " + flight.Id + ").");

                return "ok";
            }
            catch (Exception e)
            {
                WriteLogError("Could not register new flight. Error: " + e.Message);
                return "Adding to DB failed";
            }
        }
        public string editFlight(int id, int fromAirportId, int toAirportId, DateTime departure, DateTime arrival, int price)
        {
            DB Db = new DB();
            try
            {
                var fromAirport = Db.Airport.Find(fromAirportId);
                var toAirport = Db.Airport.Find(toAirportId);
                var flightToEdit = Db.Flight.Find(id);

                var From = flightToEdit.FromAirport.Name;
                var To = flightToEdit.ToAirport.Name;
                var Departure = flightToEdit.Departure;
                var Arrival = flightToEdit.Arrival;
                var Price = flightToEdit.Price;

                flightToEdit.FromAirport = fromAirport;
                flightToEdit.ToAirport = toAirport;
                flightToEdit.Departure = departure;
                flightToEdit.Arrival = arrival;
                flightToEdit.Price = price;
                Db.SaveChanges();

                WriteLogEvent("Edited flight id " + id + ": From airport was " + From + " " + Departure + 
                    ", now " + flightToEdit.FromAirport.Name + " " + flightToEdit.Departure + ". Destination was " + To + 
                    " " + Arrival + ", now " + flightToEdit.ToAirport.Name + " " + flightToEdit.Arrival + 
                    ". Price was " + Price + ", now " + flightToEdit.Price);

                return "ok";
            }
            catch (Exception e)
            {
                WriteLogError("Could not edit flight id " + id + ". Error: " + e.Message);
                return "Editing in DB failed";
            }
        }
        public string deleteFlight(int id)
        {
            DB Db = new DB();
            try
            {
                var flightToDelete = Db.Flight.Find(id);

                var From = flightToDelete.FromAirport.Name;
                var Departure = flightToDelete.Departure;
                var To = flightToDelete.ToAirport.Name;
                var Arrival = flightToDelete.Arrival;

                Db.Flight.Remove(flightToDelete);
                Db.SaveChanges();

                WriteLogEvent("Deleted flight id " + id + ": From " + From + " " + Departure + ", to " + To + " " + Arrival);

                return "ok";
            }
            catch (Exception e)
            {
                WriteLogError("Could not delete flight Id " + id + ". Error: " + e.Message);
                return "Deleting from DB failed";
            }
        }

        public List<Booking> getAllBookings()
        {
            DB Db = new DB();
            return Db.Booking.ToList();
        }
        public Booking getBooking(int id)
        {
            DB Db = new DB();
            return Db.Booking.Find(id);
        }
        public string registerBooking(int userId, int flightId, int amount)
        {
            DB Db = new DB();
            try
            {
                var user = Db.Users.Find(userId);
                var flight = Db.Flight.Find(flightId);

                var booking = new Booking
                {
                    User = user,
                    Flight = flight,
                    Amount = amount
                };
                Db.Booking.Add(booking);
                Db.SaveChanges();

                WriteLogEvent("Registered new booking: User id " + user.Id + " with flight id " + flight.Id + ", " + amount + " passengers");

                return "ok";
            }
            catch (Exception e)
            {
                WriteLogError("Could not register new booking: " + e.Message);
                return "Adding to DB failed";
            }
        }
        public string editBooking(int id, int userId, int flightId, int amount)
        {
            DB Db = new DB();
            try
            {
                var user = Db.Users.Find(userId);
                var flight = Db.Flight.Find(flightId);
                var bookingToEdit = Db.Booking.Find(id);

                var OldUser = bookingToEdit.User.Id;
                var OldFlight = bookingToEdit.Flight.Id;
                var OldAmountOfPassengers = bookingToEdit.Amount;

                bookingToEdit.User = user;
                bookingToEdit.Flight = flight;
                bookingToEdit.Amount = amount;
                Db.SaveChanges();

                WriteLogEvent("Edited booking with id " + bookingToEdit.Id + ": User id was " + OldUser + ", now " + 
                    bookingToEdit.User.Id + ", flight id was " + OldFlight + ", now " + bookingToEdit.Flight.Id + 
                    ", passengers was " + OldAmountOfPassengers + ", now " + bookingToEdit.Amount);

                return "ok";
            }
            catch (Exception e)
            {
                WriteLogError("Could not edit booking with id " + id + ". Error: " + e.Message);
                return "Editing in DB failed";
            }
        }
        public string deleteBooking(int id)
        {
            DB Db = new DB();
            try
            {
                var bookingToDelete = Db.Booking.Find(id);
                var User = bookingToDelete.User.Id;
                var Flight = bookingToDelete.Flight.Id;
                var Amount = bookingToDelete.Amount;
                Db.Booking.Remove(bookingToDelete);
                Db.SaveChanges();

                WriteLogEvent("Deleted booking id " + id + ": User " + User + ", flight " + Flight + ", passengers " + Amount);

                return "ok";
            }
            catch (Exception e)
            {
                WriteLogError("Could not delete booking with id " + id + ". Error: " + e.Message);
                return "Deleting from DB failed";
            }
        }

        public List<User> getAllUsers()
        {
            DB Db = new DB();
            return Db.Users.ToList();
        }
        public User getUser(int id)
        {
            DB Db = new DB();
            return Db.Users.Find(id);
        }
        public string registerUser(string fornavn, string etternavn, string adresse, string postnummer, string poststed, string epost, byte[] passord)
        {
            try
            {
                var Db = new DB();
                var poststedToInsert = Db.Poststed.Find(postnummer);
                if (poststedToInsert == null)
                {
                    poststedToInsert = new PostSted();
                    poststedToInsert.Postnr = postnummer;
                    poststedToInsert.Poststed = poststed;
                    Db.Poststed.Add(poststedToInsert);
                }

                var user = new User();
                user.Fornavn = fornavn;
                user.Etternavn = etternavn;
                user.Adresse = adresse;
                user.Poststed = poststedToInsert;
                user.Epost = epost;
                user.PassordHash = passord;                

                Db.Users.Add(user);
                Db.SaveChanges();

                WriteLogEvent("Registered new user: " + user.Etternavn + ", " + user.Fornavn + ". " + user.Adresse + " " + user.Poststed.Postnr + " " + 
                    user.Poststed.Poststed + ". " + user.Epost + " (id " + user.Id + ")");

                return "ok";
            }
            catch (Exception e)
            {
                WriteLogError("Could not register new user. Error: " + e.Message);
                return "Adding to DB failed";
            }
        }
        public string editUser(int id, string fornavn, string etternavn, string adresse, string postnummer, string poststed, string epost, byte[] passord)
        {
            try
            {
                var Db = new DB();

                var poststedToInsert = Db.Poststed.Find(postnummer);
                if (poststedToInsert == null)
                {
                    poststedToInsert = new PostSted();
                    poststedToInsert.Postnr = postnummer;
                    poststedToInsert.Poststed = poststed;
                    Db.Poststed.Add(poststedToInsert);
                }

                var userToEdit = Db.Users.Find(id);

                var Fornavn = userToEdit.Fornavn;
                var Etternavn = userToEdit.Etternavn;
                var Adresse = userToEdit.Adresse;
                var Postnr = userToEdit.Poststed.Postnr;
                var Poststed = userToEdit.Poststed.Poststed;
                var Epost = userToEdit.Epost;

                userToEdit.Fornavn = fornavn;
                userToEdit.Etternavn = etternavn;
                userToEdit.Adresse = adresse;
                userToEdit.Poststed = poststedToInsert;
                userToEdit.Epost = epost;
                userToEdit.PassordHash = passord;
                Db.SaveChanges();

                WriteLogEvent("Edited user id " + id + ": Fornavn from " + Fornavn + " to " + userToEdit.Fornavn + 
                    ", etternavn from " + Etternavn + " to " + userToEdit.Etternavn + ", adresse from " + 
                    Adresse + " to " + userToEdit.Adresse + ", postnr/-sted from " + Postnr + 
                    " " + Poststed + " to " + userToEdit.Poststed.Postnr + " " + userToEdit.Poststed.Poststed + 
                    ", epost from " + Epost + " to " + userToEdit.Epost);

                return "ok";
            }
            catch (Exception e)
            {
                WriteLogError("Could not edit user with id " + id + ". Error: " + e.Message);
                return "Editing in DB failed";
            }
        }
        public string deleteUser(int id)
        {
            DB Db = new DB();
            try
            {
                var userToDelete = Db.Users.Find(id);
                Db.Users.Remove(userToDelete);
                Db.SaveChanges();

                WriteLogEvent("Deleted user with Id " + id);

                return "ok";
            }
            catch (Exception e)
            {
                WriteLogError("Could not delete user with id " + id + ". Error: " + e.Message);
                return "Deleting from DB failed";
            }
        }

        private void WriteLogError(string msg)
        {
            if (!Directory.Exists(LogPath))
            {
                try
                {
                    Directory.CreateDirectory(LogPath);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Could not create log folder: " + e.Message);
                }
            }

            using (StreamWriter w = File.AppendText(LogPath + LOG_ERRORS))
            {
                try
                {
                    Logger.Write(msg, w);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Could not write to error log. Exception: " + e.Message);
                }
            }
        }

        private void WriteLogEvent(string msg)
        {
            if (!Directory.Exists(LogPath))
            {
                try
                {
                    Directory.CreateDirectory(LogPath);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Could not create log folder: " + e.Message);
                }
            }

            using (StreamWriter w = File.AppendText(LogPath + LOG_EVENTS))
            {
                try
                {
                    Logger.Write(msg, w);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Could not write to database log. Exception: " + e.Message);
                }
            }
        }
    }
}
