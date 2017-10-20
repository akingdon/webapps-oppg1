using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAppsOppgave1.Model;

namespace WebAppsOppgave1.DAL
{
    public class AdminDAL
    {
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
                return "ok";
            }
            catch (Exception e)
            {
                return "Adding to DB failed";
            }
        }
        public string editAirport(int id, string name)
        {
            DB Db = new DB();
            try
            {
                var airportToEdit = Db.Airport.Find(id);
                airportToEdit.Name = name;
                Db.SaveChanges();
                return "ok";
            }
            catch {
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
                return "ok";
            }
            catch
            {
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
    }
}
