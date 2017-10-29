using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace WebAppsOppgave1.Models
{
    public class DBInit : DropCreateDatabaseAlways<DB>
    {
        protected override void Seed(DB db)
        {
            var airport1 = new Airport
            {
                Name = "Oslo"
            };

            var airport2 = new Airport
            {
                Name = "Trondheim"
            };

            var airport3 = new Airport
            {
                Name = "London"
            };

            var airport4 = new Airport
            {
                Name = "Stockholm"
            };


            var flight1 = new Flight
            {
                FromAirport = airport1,
                ToAirport = airport2,
                Departure = new DateTime(2017, 10, 10, 8, 30, 00),
                Price = 600
            };
            flight1.Arrival = flight1.Departure.AddHours(1);

            var flight2 = new Flight
            {
                FromAirport = airport1,
                ToAirport = airport2,
                Departure = new DateTime(2017, 10, 10, 16, 00, 00),
                Price = 800
            };
            flight2.Arrival = flight2.Departure.AddHours(1);

            var flight3 = new Flight
            {
                FromAirport = airport1,
                ToAirport = airport2,
                Departure = new DateTime(2017, 10, 10, 22, 30, 00),
                Price = 800
            };
            flight3.Arrival = flight3.Departure.AddHours(1);

            var flight4 = new Flight
            {
                FromAirport = airport1,
                ToAirport = airport2,
                Departure = new DateTime(2017, 10, 11, 8, 30, 00),
                Price = 600
            };
            flight4.Arrival = flight4.Departure.AddHours(1);

            var flight5 = new Flight
            {
                FromAirport = airport1,
                ToAirport = airport2,
                Departure = new DateTime(2017, 10, 11, 16, 00, 00),
                Price = 800
            };
            flight5.Arrival = flight5.Departure.AddHours(1);

            var flight6 = new Flight
            {
                FromAirport = airport1,
                ToAirport = airport2,
                Departure = new DateTime(2017, 10, 11, 22, 30, 00),
                Price = 500
            };
            flight6.Arrival = flight6.Departure.AddHours(1);

            var flight7 = new Flight
            {
                FromAirport = airport1,
                ToAirport = airport3,
                Departure = new DateTime(2017, 10, 10, 09, 30, 00),
                Price = 800
            };
            flight7.Arrival = flight7.Departure.AddHours(2);

            var flight8 = new Flight
            {
                FromAirport = airport1,
                ToAirport = airport3,
                Departure = new DateTime(2017, 10, 10, 17, 00, 00),
                Price = 800
            };
            flight8.Arrival = flight8.Departure.AddHours(2);

            var flight9 = new Flight
            {
                FromAirport = airport1,
                ToAirport = airport3,
                Departure = new DateTime(2017, 10, 10, 23, 30, 00),
                Price = 800
            };
            flight9.Arrival = flight9.Departure.AddHours(2);

            var flight10 = new Flight
            {
                FromAirport = airport2,
                ToAirport = airport1,
                Departure = new DateTime(2017, 10, 10, 10, 00, 00),
                Price = 600
            };
            flight10.Arrival = flight10.Departure.AddHours(1);

            var flight11 = new Flight
            {
                FromAirport = airport2,
                ToAirport = airport1,
                Departure = new DateTime(2017, 10, 10, 14, 30, 00),
                Price = 800
            };
            flight11.Arrival = flight11.Departure.AddHours(1);

            var flight12 = new Flight
            {
                FromAirport = airport2,
                ToAirport = airport1,
                Departure = new DateTime(2017, 10, 10, 19, 30, 00),
                Price = 800
            };
            flight12.Arrival = flight12.Departure.AddHours(1);

            var flight13 = new Flight
            {
                FromAirport = airport2,
                ToAirport = airport1,
                Departure = new DateTime(2017, 10, 11, 10, 00, 00),
                Price = 600
            };
            flight13.Arrival = flight13.Departure.AddHours(1);

            var flight14 = new Flight
            {
                FromAirport = airport2,
                ToAirport = airport1,
                Departure = new DateTime(2017, 10, 11, 14, 30, 00),
                Price = 800
            };
            flight14.Arrival = flight14.Departure.AddHours(1);

            var flight15 = new Flight
            {
                FromAirport = airport2,
                ToAirport = airport1,
                Departure = new DateTime(2017, 10, 11, 19, 30, 00),
                Price = 800
            };
            flight15.Arrival = flight15.Departure.AddHours(1);

            var flight16 = new Flight
            {
                FromAirport = airport1,
                ToAirport = airport3,
                Departure = new DateTime(2017, 10, 11, 09, 30, 00),
                Price = 800
            };
            flight16.Arrival = flight16.Departure.AddHours(2);

            var flight17 = new Flight
            {
                FromAirport = airport1,
                ToAirport = airport3,
                Departure = new DateTime(2017, 10, 11, 17, 00, 00),
                Price = 800
            };
            flight17.Arrival = flight17.Departure.AddHours(2);

            var flight18 = new Flight
            {
                FromAirport = airport1,
                ToAirport = airport3,
                Departure = new DateTime(2017, 10, 11, 23, 30, 00),
                Price = 800
            };
            flight18.Arrival = flight18.Departure.AddHours(2);

            var flight19 = new Flight
            {
                FromAirport = airport3,
                ToAirport = airport1,
                Departure = new DateTime(2017, 10, 10, 06, 00, 00),
                Price = 600
            };
            flight19.Arrival = flight19.Departure.AddHours(2);

            var flight20 = new Flight
            {
                FromAirport = airport3,
                ToAirport = airport1,
                Departure = new DateTime(2017, 10, 10, 11, 00, 00),
                Price = 800
            };
            flight20.Arrival = flight20.Departure.AddHours(2);

            var flight21 = new Flight
            {
                FromAirport = airport3,
                ToAirport = airport1,
                Departure = new DateTime(2017, 10, 10, 18, 30, 00),
                Price = 800
            };
            flight21.Arrival = flight21.Departure.AddHours(2);

            var flight22 = new Flight
            {
                FromAirport = airport3,
                ToAirport = airport1,
                Departure = new DateTime(2017, 10, 11, 6, 30, 00),
                Price = 600
            };
            flight22.Arrival = flight22.Departure.AddHours(2);

            var flight23 = new Flight
            {
                FromAirport = airport3,
                ToAirport = airport1,
                Departure = new DateTime(2017, 10, 11, 11, 00, 00),
                Price = 800
            };
            flight23.Arrival = flight23.Departure.AddHours(2);

            var flight24 = new Flight
            {
                FromAirport = airport3,
                ToAirport = airport1,
                Departure = new DateTime(2017, 10, 11, 18, 30, 00),
                Price = 500
            };
            flight24.Arrival = flight24.Departure.AddHours(2);

            var flight25 = new Flight
            {
                FromAirport = airport2,
                ToAirport = airport3,
                Departure = new DateTime(2017, 10, 10, 07, 00, 00),
                Price = 650
            };
            flight25.Arrival = flight25.Departure.AddHours(3);

            var flight26 = new Flight
            {
                FromAirport = airport2,
                ToAirport = airport3,
                Departure = new DateTime(2017, 10, 10, 11, 00, 00),
                Price = 650
            };
            flight26.Arrival = flight26.Departure.AddHours(3);

            var flight27 = new Flight
            {
                FromAirport = airport2,
                ToAirport = airport3,
                Departure = new DateTime(2017, 10, 10, 19, 30, 00),
                Price = 700
            };
            flight27.Arrival = flight27.Departure.AddHours(3);

            var flight28 = new Flight
            {
                FromAirport = airport2,
                ToAirport = airport3,
                Departure = new DateTime(2017, 10, 11, 07, 00, 00),
                Price = 650
            };
            flight28.Arrival = flight28.Departure.AddHours(3);

            var flight29 = new Flight
            {
                FromAirport = airport2,
                ToAirport = airport3,
                Departure = new DateTime(2017, 10, 11, 11, 00, 00),
                Price = 650
            };
            flight29.Arrival = flight29.Departure.AddHours(3);

            var flight30 = new Flight
            {
                FromAirport = airport2,
                ToAirport = airport3,
                Departure = new DateTime(2017, 10, 11, 19, 30, 00),
                Price = 700
            };
            flight30.Arrival = flight30.Departure.AddHours(3);

            var flight31 = new Flight
            {
                FromAirport = airport3,
                ToAirport = airport2,
                Departure = new DateTime(2017, 10, 10, 07, 00, 00),
                Price = 650
            };
            flight31.Arrival = flight31.Departure.AddHours(3);

            var flight32 = new Flight
            {
                FromAirport = airport3,
                ToAirport = airport2,
                Departure = new DateTime(2017, 10, 10, 11, 00, 00),
                Price = 650
            };
            flight32.Arrival = flight32.Departure.AddHours(3);

            var flight33 = new Flight
            {
                FromAirport = airport3,
                ToAirport = airport2,
                Departure = new DateTime(2017, 10, 10, 19, 30, 00),
                Price = 700
            };
            flight33.Arrival = flight33.Departure.AddHours(3);

            var flight34 = new Flight
            {
                FromAirport = airport3,
                ToAirport = airport2,
                Departure = new DateTime(2017, 10, 11, 07, 00, 00),
                Price = 650
            };
            flight34.Arrival = flight34.Departure.AddHours(3);

            var flight35 = new Flight
            {
                FromAirport = airport3,
                ToAirport = airport2,
                Departure = new DateTime(2017, 10, 11, 11, 00, 00),
                Price = 650
            };
            flight35.Arrival = flight35.Departure.AddHours(3);

            var flight36 = new Flight
            {
                FromAirport = airport3,
                ToAirport = airport2,
                Departure = new DateTime(2017, 10, 11, 19, 30, 00),
                Price = 700
            };
            flight36.Arrival = flight36.Departure.AddHours(3);

            var postSted1 = new PostSted
            {
                Postnr = "0171",
                Poststed = "Oslo"
            };

            var postSted2 = new PostSted
            {
                Postnr = "2345",
                Poststed = "Snertingdal"
            };

            var postSted3 = new PostSted
            {
                Postnr = "6962",
                Poststed = "Hammerfest"
            };


            var user1 = new User
            {
                Fornavn = "Kurt",
                Etternavn = "Johansen",
                Adresse = "Brandts gate 2b",
                Poststed = postSted1,
                Epost = "test@epost.no",
                PassordHash = Controllers.HomeController.HashPassword("testbruker")
            };

            var user2 = new User
            {
                Fornavn = "Arnulf",
                Etternavn = "Paulsen",
                Adresse = "Høgglinna 9",
                Poststed = postSted2,
                Epost = "nr9@talas.no",
                PassordHash = Controllers.HomeController.HashPassword("knusebil")
            };

            var user3 = new User
            {
                Fornavn = "Tore",
                Etternavn = "Brattebakk",
                Adresse = "Josefines gate 64 F",
                Poststed = postSted1,
                Epost = "tore@epost.no",
                PassordHash = Controllers.HomeController.HashPassword("passord12")
            };

            var user4 = new User
            {
                Fornavn = "Arnulf",
                Etternavn = "Paulsen",
                Adresse = "Doppelganger Allé 2",
                Poststed = postSted3,
                Epost = "imposter@email.com",
                PassordHash = Controllers.HomeController.HashPassword("passord23")
            };


            var booking1 = new Booking
            {
                User = user1,
                Flight = flight2,
                Amount = 2
            };

            var booking2 = new Booking
            {
                User = user1,
                Flight = flight5,
                Amount = 1
            };

            var admin = new Model.AdminUser
            {
                Epost = "admin@epost.no",
                PassordHash = Controllers.AdminController.HashPassword("adminpassord")
            };


            db.Airport.Add(airport1);
            db.Airport.Add(airport2);
            db.Airport.Add(airport3);
            db.Airport.Add(airport4);

            db.Flight.Add(flight1);
            db.Flight.Add(flight2);
            db.Flight.Add(flight3);
            db.Flight.Add(flight4);
            db.Flight.Add(flight5);
            db.Flight.Add(flight6);
            db.Flight.Add(flight7);
            db.Flight.Add(flight8);
            db.Flight.Add(flight9);
            db.Flight.Add(flight10);
            db.Flight.Add(flight11);
            db.Flight.Add(flight12);
            db.Flight.Add(flight13);
            db.Flight.Add(flight14);
            db.Flight.Add(flight15);
            db.Flight.Add(flight16);
            db.Flight.Add(flight17);
            db.Flight.Add(flight18);
            db.Flight.Add(flight19);
            db.Flight.Add(flight20);
            db.Flight.Add(flight21);
            db.Flight.Add(flight22);
            db.Flight.Add(flight23);
            db.Flight.Add(flight24);
            db.Flight.Add(flight25);
            db.Flight.Add(flight26);
            db.Flight.Add(flight27);
            db.Flight.Add(flight28);
            db.Flight.Add(flight29);
            db.Flight.Add(flight30);
            db.Flight.Add(flight31);
            db.Flight.Add(flight32);
            db.Flight.Add(flight33);
            db.Flight.Add(flight34);
            db.Flight.Add(flight35);
            db.Flight.Add(flight36);

            db.Poststed.Add(postSted1);
            db.Poststed.Add(postSted2);
            db.Poststed.Add(postSted3);
            db.Users.Add(user1);
            db.Users.Add(user2);
            db.Users.Add(user3);
            db.Users.Add(user4);
            db.Admins.Add(admin);

            db.Booking.Add(booking1);
            db.Booking.Add(booking2);

            base.Seed(db);
        }
    }
}