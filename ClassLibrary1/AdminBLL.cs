using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAppsOppgave1.DAL;
using WebAppsOppgave1.Model;

namespace WebAppsOppgave1.BLL
{
    public class AdminBLL
    {
        public AdminUser AdminInDb(string UserName, byte[] HashedPassword)
        {
            var AdminDAL = new AdminDAL();
            return AdminDAL.AdminInDb(UserName, HashedPassword);
        }

        public List<Airport> getAllAirports()
        {
            var AdminDAL = new AdminDAL();
            return AdminDAL.getAllAirports();
        }
        public Airport getAirport(int id)
        {
            var AdminDAL = new AdminDAL();
            return AdminDAL.getAirport(id);
        }
        public string registerAirport(string name)
        {
            var AdminDAL = new AdminDAL();
            return AdminDAL.registerAirport(name);
        }
        public string editAirport(int id, string name)
        {
            var AdminDAL = new AdminDAL();
            return AdminDAL.editAirport(id, name);
        }
        public string deleteAirport(int id)
        {
            var AdminDAL = new AdminDAL();
            return AdminDAL.deleteAirport(id);
        }

        public List<Flight> getAllFlights()
        {
            var AdminDAL = new AdminDAL();
            return AdminDAL.getAllFlights();
        }
        public Flight getFlight(int id)
        {
            var AdminDAL = new AdminDAL();
            return AdminDAL.getFlight(id);
        }
        public string registerFlight(int fromAirportId, int toAirportId, DateTime departure, DateTime arrival, int price)
        {
            var AdminDAL = new AdminDAL();
            return AdminDAL.RegisterFlight(fromAirportId, toAirportId, departure, arrival, price);
        }
        public string editFlight(int id, int fromAirportId, int toAirportId, DateTime departure, DateTime arrival, int price)
        {
            var AdminDAL = new AdminDAL();
            return AdminDAL.editFlight(id, fromAirportId, toAirportId, departure, arrival, price);
        }
        public string deleteFlight(int id)
        {
            var AdminDAL = new AdminDAL();
            return AdminDAL.deleteFlight(id);
        }

        public List<User> getAllUsers()
        {
            var AdminDAL = new AdminDAL();
            return AdminDAL.getAllUsers();
        }
        public User getUser(int id)
        {
            var AdminDAL = new AdminDAL();
            return AdminDAL.getUser(id);
        }
        public string registerUser(string fornavn, string etternavn, string adresse, string postnummer, string poststed, string epost, byte[] passord)
        {
            var AdminDAL = new AdminDAL();
            return AdminDAL.registerUser(fornavn, etternavn, adresse, postnummer, poststed, epost, passord);
        }
        public string editUser(int id, string fornavn, string etternavn, string adresse, string postnummer, string poststed, string epost, byte[] passord)
        {
            var AdminDAL = new AdminDAL();
            return AdminDAL.editUser(id, fornavn, etternavn, adresse, postnummer, poststed, epost, passord);
        }
        public string deleteUser(int id)
        {
            var AdminDAL = new AdminDAL();
            return AdminDAL.deleteUser(id);
        }
    }
}
