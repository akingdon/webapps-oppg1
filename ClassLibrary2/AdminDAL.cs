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
        public List<jsAirport> getAllAirports()
        {
            DB Db = new DB();
            List<jsAirport> allAirports = Db.Airport.Select(a => new jsAirport()
            {
                id = a.Id,
                name = a.Name
            }).ToList();
            return allAirports;
        }
        public jsAirport getAirport(int id)
        {
            DB Db = new DB();
            var anAirport = Db.Airport.Find(id);
            if (anAirport != null)
            {
                var jsAirport = new jsAirport()
                {
                    id = anAirport.Id,
                    name = anAirport.Name
                };
                return jsAirport;
            }
            else
            {
                return null;
            }
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
                Airport airportToEdit = Db.Airport.Find(id);

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
                Airport airportToDelete = Db.Airport.Find(id);
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
            List<Flight> allFlights = Db.Flight.Select(f => new Flight()
            {
                Id = f.Id,
                FromAirport = f.FromAirport,
                ToAirport = f.ToAirport,
                Departure = f.Departure,
                Arrival = f.Arrival,
                Price = f.Price
            }).ToList();
            return allFlights;
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
