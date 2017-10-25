using System;
using System.Collections.Generic;
using WebAppsOppgave1.Model;

namespace WebAppsOppgave1.BLL
{
    public interface IAdminBLL
    {
        AdminUser AdminInDb(string UserName, byte[] HashedPassword);
        string deleteAirport(int id);
        string deleteBooking(int id);
        string deleteFlight(int id);
        string deleteUser(int id);
        string editAirport(int id, string name);
        string editBooking(int id, int userId, int flightId, int amount);
        string editFlight(int id, int fromAirportId, int toAirportId, DateTime departure, DateTime arrival, int price);
        string editUser(int id, string fornavn, string etternavn, string adresse, string postnummer, string poststed, string epost, byte[] passord);
        Airport getAirport(int id);
        List<Airport> getAllAirports();
        List<Booking> getAllBookings();
        List<Flight> getAllFlights();
        List<User> getAllUsers();
        Booking getBooking(int id);
        Flight getFlight(int id);
        User getUser(int id);
        string registerAirport(string name);
        string registerBooking(int userId, int flightId, int amount);
        string registerFlight(int fromAirportId, int toAirportId, DateTime departure, DateTime arrival, int price);
        string registerUser(string fornavn, string etternavn, string adresse, string postnummer, string poststed, string epost, byte[] passord);
    }
}