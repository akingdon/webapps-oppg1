using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAppsOppgave1.Model;

namespace WebAppsOppgave1.DAL
{
    public class AdminDAL
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
                return "ok";
            }
            catch (Exception e)
            {
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
                flightToEdit.FromAirport = fromAirport;
                flightToEdit.ToAirport = toAirport;
                flightToEdit.Departure = departure;
                flightToEdit.Arrival = arrival;
                flightToEdit.Price = price;
                Db.SaveChanges();
                return "ok";
            }
            catch
            {
                return "Editing in DB failed";
            }
        }
        public string deleteFlight(int id)
        {
            DB Db = new DB();
            try
            {
                var flightToDelete = Db.Flight.Find(id);
                Db.Flight.Remove(flightToDelete);
                Db.SaveChanges();
                return "ok";
            }
            catch
            {
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
                return "ok";
            }
            catch (Exception e)
            {
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
                bookingToEdit.User = user;
                bookingToEdit.Flight = flight;
                bookingToEdit.Amount = amount;
                Db.SaveChanges();
                return "ok";
            }
            catch
            {
                return "Editing in DB failed";
            }
        }
        public string deleteBooking(int id)
        {
            DB Db = new DB();
            try
            {
                var bookingToDelete = Db.Booking.Find(id);
                Db.Booking.Remove(bookingToDelete);
                Db.SaveChanges();
                return "ok";
            }
            catch
            {
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

                return "ok";
            }
            catch (Exception e)
            {
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
                userToEdit.Fornavn = fornavn;
                userToEdit.Etternavn = etternavn;
                userToEdit.Adresse = adresse;
                userToEdit.Poststed = poststedToInsert;
                userToEdit.Epost = epost;
                userToEdit.PassordHash = passord;
                Db.SaveChanges();

                return "ok";
            }
            catch (Exception e)
            {
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
                return "ok";
            }
            catch
            {
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
