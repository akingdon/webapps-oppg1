using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAppsOppgave1.DAL;
using WebAppsOppgave1.Model;

namespace WebAppsOppgave1.BLL
{
    public class AdminBLL : IAdminBLL
    {
        private IAdminDAL _repository;

        public AdminBLL()
        {
            _repository = new AdminDAL();
        }

        public AdminBLL(IAdminDAL stub)
        {
            _repository = stub;
        }

        public AdminUser AdminInDb(string UserName, byte[] HashedPassword)
        {
            var AdminInDb = _repository.AdminInDb(UserName, HashedPassword);
            return AdminInDb;
        }

        public List<Airport> getAllAirports(string name)
        {
            var AllAirports = _repository.getAllAirports(name);
            return AllAirports;
        }

        public Airport getAirport(int id)
        {
            var Airport = _repository.getAirport(id);
            return Airport;
        }

        public string registerAirport(string name)
        {
            var RegisteredAirport = _repository.registerAirport(name);
            return RegisteredAirport;
        }

        public string editAirport(int id, string name)
        {
            var EditedAirport = _repository.editAirport(id, name);
            return EditedAirport;
        }

        public string deleteAirport(int id)
        {
            var DeletedAirport = _repository.deleteAirport(id);
            return DeletedAirport;
        }

        public List<Flight> getAllFlights(string from, string to, string departure)
        {
            var AllFlights = _repository.getAllFlights(from, to, departure);
            return AllFlights;
        }

        public Flight getFlight(int id)
        {
            var Flight = _repository.getFlight(id);
            return Flight;
        }

        public string registerFlight(int fromAirportId, int toAirportId, DateTime departure, DateTime arrival, int price)
        {
            var RegisteredFlight = _repository.RegisterFlight(fromAirportId, toAirportId, departure, arrival, price);
            return RegisteredFlight;
        }

        public string editFlight(int id, int fromAirportId, int toAirportId, DateTime departure, DateTime arrival, int price)
        {
            var EditedFlight = _repository.editFlight(id, fromAirportId, toAirportId, departure, arrival, price);
            return EditedFlight;
        }

        public string deleteFlight(int id)
        {
            var DeletedFlight = _repository.deleteFlight(id);
            return DeletedFlight;
        }

        public List<Booking> getAllBookings(string user, string flight)
        {
            var AllBookings = _repository.getAllBookings(user, flight);
            return AllBookings;
        }

        public Booking getBooking(int id)
        {
            var Booking = _repository.getBooking(id);
            return Booking;
        }

        public string registerBooking(int userId, int flightId, int amount)
        {
            var RegisteredBooking = _repository.registerBooking(userId, flightId, amount);
            return RegisteredBooking;
        }

        public string editBooking(int id, int userId, int flightId, int amount)
        {
            var EditedBooking = _repository.editBooking(id, userId, flightId, amount);
            return EditedBooking;
        }

        public string deleteBooking(int id)
        {
            var DeletedBooking = _repository.deleteBooking(id);
            return DeletedBooking;
        }

        public List<User> getAllUsers(string etternavn, string postnr)
        {
            var AllUsers = _repository.getAllUsers(etternavn, postnr);
            return AllUsers;
        }

        public User getUser(int id)
        {
            var User = _repository.getUser(id);
            return User;
        }

        public string registerUser(string fornavn, string etternavn, string adresse, string postnummer, string poststed, string epost, byte[] passord)
        {
            var RegisteredUser = _repository.registerUser(fornavn, etternavn, adresse, postnummer, poststed, epost, passord);
            return RegisteredUser;
        }

        public string editUser(int id, string fornavn, string etternavn, string adresse, string postnummer, string poststed, string epost)
        {
            var EditedUser = _repository.editUser(id, fornavn, etternavn, adresse, postnummer, poststed, epost);
            return EditedUser;
        }

        public string deleteUser(int id)
        {
            var DeletedUser = _repository.deleteUser(id);
            return DeletedUser;
        }
    }
}
