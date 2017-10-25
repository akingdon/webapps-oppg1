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
            var AllAirports = new List<Airport>();

            var Airport1 = new Airport()
            {
                Id = 1,
                Name = "Torp"
            };
            var Airport2 = new Airport
            {
                Id = 2,
                Name = "Rygge"
            };
            var Airport3 = new Airport
            {
                Id = 3,
                Name = "Torp"
            };

            if (String.IsNullOrEmpty(name))
            {
                AllAirports.Add(Airport1);
                AllAirports.Add(Airport2);
                AllAirports.Add(Airport3);

            }
            else
            {
                AllAirports.Add(Airport1);
                AllAirports.Add(Airport3);
            }

            return AllAirports;
        }

        public List<Booking> getAllBookings(string user, string flight)
        {
            var AllBookings = new List<Booking>();

            var User1 = new User
            {
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
            var User2 = new User
            {
                Fornavn = "AnnetFornavn",
                Etternavn = "AnnetEtternavn",
                Adresse = "Postnummerveien 9",
                Poststed = new PostSted
                {
                    Postnr = "9876",
                    Poststed = "Huttiheita"
                },
                Epost = "b@post.no"
            };

            var Flight1 = new Flight
            {
                Id = 8,
                FromAirport = new Airport
                {
                    Name = "Torp"
                },
                Departure = DateTime.Parse("01.11.2017 12:00"),
                ToAirport = new Airport
                {
                    Name = "Stockholm"
                },
                Arrival = DateTime.Parse("01.11.2017 12:50"),
                Price = 900
            };

            var Flight2 = new Flight
            {
                Id = 9,
                FromAirport = new Airport
                {
                    Name = "London"
                },
                Departure = DateTime.Parse("01.11.2017 12:00"),
                ToAirport = new Airport
                {
                    Name = "Oslo"
                },
                Arrival = DateTime.Parse("01.11.2017 14:00"),
                Price = 1200
            };

            var Booking1 = new Booking
            {
                Id = 1,
                Amount = 4,
                User = User1,
                Flight = Flight1,
            };
            var Booking2 = new Booking
            {
                Id = 2,
                Amount = 7,
                User = User1,
                Flight = Flight2,
            };
            var Booking3 = new Booking
            {
                Id = 3,
                Amount = 6,
                User = User2,
                Flight = Flight1,
            };

            var Booking4 = new Booking
            {
                Id = 4,
                Amount = 2,
                User = User2,
                Flight = Flight2,
            };

            if (String.IsNullOrEmpty(user) && String.IsNullOrEmpty(flight))
            {
                AllBookings.Add(Booking1);
                AllBookings.Add(Booking2);
                AllBookings.Add(Booking3);
                AllBookings.Add(Booking4);
            }
            else if (!String.IsNullOrEmpty(user) && String.IsNullOrEmpty(flight))
            {
                AllBookings.Add(Booking3);
                AllBookings.Add(Booking4);
            }
            else if (String.IsNullOrEmpty(user) && !String.IsNullOrEmpty(flight))
            {
                AllBookings.Add(Booking2);
            }
            else
            {
                AllBookings.Add(Booking1);
            }

            return AllBookings;
        }

        public List<Flight> getAllFlights(string from, string to, string departure)
        {
            var AllFlights = new List<Flight>();

            var Torp = new Airport
            {
                Id = 1,
                Name = "Torp"
            };
            var Trondheim = new Airport
            {
                Id = 2,
                Name = "Trondheim"
            };
            var Rygge = new Airport
            {
                Id = 3,
                Name = "Rygge"
            };

            var RyggeTrondheim = new Flight
            {
                Id = 1,
                FromAirport = Rygge,
                ToAirport = Trondheim,
                Departure = DateTime.Parse("01.11.2017 13:00"),
                Arrival = DateTime.Parse("01.11.2017 13:50"),
                Price = 799
            };
            var RyggeTorp = new Flight
            {
                Id = 2,
                FromAirport = Rygge,
                ToAirport = Torp,
                Departure = DateTime.Parse("02.11.2017 11:25"),
                Arrival = DateTime.Parse("02.11.2017 12:10"),
                Price = 299
            };
            var TorpTrondheim = new Flight
            {
                Id = 3,
                FromAirport = Torp,
                ToAirport = Trondheim,
                Departure = DateTime.Parse("02.11.2017 11:55"),
                Arrival = DateTime.Parse("02.11.2017 12:45"),
                Price = 499
            };
            var TorpRygge = new Flight
            {
                Id = 4,
                FromAirport = Torp,
                ToAirport = Rygge,
                Departure = DateTime.Parse("03.11.2017 19:10"),
                Arrival = DateTime.Parse("03.11.2017 19:45"),
                Price = 299
            };
            var TrondheimRygge = new Flight
            {
                Id = 5,
                FromAirport = Trondheim,
                ToAirport = Rygge,
                Departure = DateTime.Parse("03.11.2017 17:30"),
                Arrival = DateTime.Parse("03.11.2017 17:20"),
                Price = 399
            };
            var TrondheimTorp = new Flight
            {
                Id = 6,
                FromAirport = Trondheim,
                ToAirport = Torp,
                Departure = DateTime.Parse("04.11.2017 18:50"),
                Arrival = DateTime.Parse("04.11.2017 19:40"),
                Price = 299
            };

            if (!String.IsNullOrEmpty(from) && String.IsNullOrEmpty(to) && String.IsNullOrEmpty(departure)) 
            {
                AllFlights.Add(TrondheimRygge);
                AllFlights.Add(TrondheimTorp);
            }
            else if (String.IsNullOrEmpty(from) && !String.IsNullOrEmpty(to) && String.IsNullOrEmpty(departure))
            {
                AllFlights.Add(RyggeTrondheim);
                AllFlights.Add(TorpTrondheim);
            }
            else if (String.IsNullOrEmpty(from) && String.IsNullOrEmpty(to) && !String.IsNullOrEmpty(departure))
            {
                AllFlights.Add(RyggeTrondheim);
            }
            else if (!String.IsNullOrEmpty(from) && !String.IsNullOrEmpty(to) && String.IsNullOrEmpty(departure))
            {
                AllFlights.Add(TrondheimRygge);
            }
            else if (!String.IsNullOrEmpty(from) && String.IsNullOrEmpty(to) && !String.IsNullOrEmpty(departure))
            {
                AllFlights.Add(TrondheimTorp);
            }
            else if (String.IsNullOrEmpty(from) && !String.IsNullOrEmpty(to) && !String.IsNullOrEmpty(departure))
            {
                AllFlights.Add(TrondheimRygge);
            }
            else if (!String.IsNullOrEmpty(from) && !String.IsNullOrEmpty(to) && !String.IsNullOrEmpty(departure))
            {
                AllFlights.Add(RyggeTorp);
            }
            else
            {
                AllFlights.Add(RyggeTrondheim);
                AllFlights.Add(RyggeTorp);
                AllFlights.Add(TorpTrondheim);
                AllFlights.Add(TorpRygge);
                AllFlights.Add(TrondheimRygge);
                AllFlights.Add(TrondheimTorp);
            }

            return AllFlights;
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
