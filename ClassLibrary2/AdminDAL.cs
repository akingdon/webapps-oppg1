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
                Airport airportToEdit = Db.Airport.Find(id);
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
                Airport airportToDelete = Db.Airport.Find(id);
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
    }
}
