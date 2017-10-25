using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAppsOppgave1.Model;

namespace WebAppsOppgave1.DAL
{
    public class AdminDALStub : IAdminDAL
    {
        public AdminUser AdminInDb(string UserName, byte[] HashedPassword)
        {
            if (UserName.Equals("admin"))
            {
                return new AdminUser
                {
                    Id = 1,
                    Epost = "admin",
                    PassordHash = HashedPassword
                };
            }

            return null;
        }

        public string deleteAirport(int id)
        {
            if (id >= 0)
            {
                return "ok";
            }

            return "Deleting from DB failed";
        }

        public string deleteBooking(int id)
        {
            if (id >= 0)
            {
                return "ok";
            }

            return "Deleting from DB failed";
        }

        public string deleteFlight(int id)
        {
            if (id >= 0)
            {
                return "ok";
            }

            return "Deleting from DB failed";
        }

        public string deleteUser(int id)
        {
            if (id >= 0)
            {
                return "ok";
            }

            return "Deleting from DB failed";
        }

        public string editAirport(int id, string name)
        {
            if (id >= 0)
            {
                return "ok";
            }

            return "Editing in DB failed";
        }

        public string editBooking(int id, int userId, int flightId, int amount)
        {
            if (id >= 0)
            {
                return "ok";
            }

            return "Editing in DB failed";
        }

        public string editFlight(int id, int fromAirportId, int toAirportId, DateTime departure, DateTime arrival, int price)
        {
            if (id >= 0)
            {
                return "ok";
            }

            return "Editing in DB failed";
        }

        public string editUser(int id, string fornavn, string etternavn, string adresse, string postnummer, string poststed, string epost, byte[] passord)
        {
            if (id >= 0)
            {
                return "ok";
            }

            return "Editing in DB failed";
        }

        public Airport getAirport(int id)
        {
            if (id >= 0)
            {
                return new Airport
                {
                    Name = "Torp"
                };
            }

            return null;
        }

        public List<Airport> getAllAirports(string name)
        {
            throw new NotImplementedException();
        }

        public List<Booking> getAllBookings(string user, string flight)
        {
            throw new NotImplementedException();
        }

        public List<Flight> getAllFlights(string from, string to, string departure)
        {
            throw new NotImplementedException();
        }

        public List<User> getAllUsers(string etternavn, string postnr)
        {
            throw new NotImplementedException();
        }

        public Booking getBooking(int id)
        {
            if (id >= 0)
            {
                return new Booking
                {
                    Id = 1,
                    Amount = 7,
                    Flight = new Flight
                    {
                        Id = 1,
                        FromAirport = new Airport
                        {
                            Name = "Torp"
                        },
                        ToAirport = new Airport
                        {
                            Name = "Rygge"
                        },
                        Departure = DateTime.Parse("01.11.2017 12:00")
                    },
                    User = new User
                    {
                        Id = 1,
                        Fornavn = "fornavn",
                        Etternavn = "etternavn"
                    }
                };
            }

            return null;
        }

        public Flight getFlight(int id)
        {
            if (id >= 0)
            {
                return new Flight
                {
                    Id = 1,
                    FromAirport = new Airport
                    {
                        Id = 1,
                        Name = "Torp"
                    },
                    ToAirport = new Airport
                    {
                        Id = 2,
                        Name = "Ikke Torp"
                    },
                    Departure = DateTime.Parse("01.11.2017 12:00"),
                    Arrival = DateTime.Parse("01.11.2017 12:50"),
                    Price = 300
                };
            }

            return null;
        }

        public User getUser(int id)
        {
            if (id >= 0)
            {
                return new User
                {
                    Id = 1,
                    Fornavn = "Fornavn",
                    Etternavn = "Etternavn",
                    Adresse = "Adresseveien 2",
                    Poststed = new PostSted
                    {
                        Postnr = "0123",
                        Poststed = "Oslo"
                    },
                    Epost = "e@post.no"
                };
            }

            return null;
        }

        public string registerAirport(string name)
        {
            return "ok";
        }

        public string registerBooking(int userId, int flightId, int amount)
        {
            if (amount > 0)
            {
                return "ok";
            }

            return "Adding to DB failed";
        }

        public string RegisterFlight(int fromAirportId, int toAirportId, DateTime departure, DateTime arrival, int price)
        {
            if (departure == null)
            {
                return "Adding to DB failed";
            }

            if (arrival == null)
            {
                return "Adding to DB failed";
            }

            if (price <= 0)
            {
                return "Adding to DB failed";
            }

            return "ok";
        }

        public string registerUser(string fornavn, string etternavn, string adresse, string postnummer, string poststed, string epost, byte[] passord)
        {
            return "ok";
        }
    }
}
