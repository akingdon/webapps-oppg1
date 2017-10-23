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
                Departure = new DateTime(2017, 10, 11, 8, 30, 00),
                Price = 600
            };
            flight2.Arrival = flight2.Departure.AddHours(1);

            var flight3 = new Flight
            {
                FromAirport = airport1,
                ToAirport = airport2,
                Departure = new DateTime(2017, 10, 11, 16, 00, 00),
                Price = 800
            };
            flight3.Arrival = flight3.Departure.AddHours(1);

            var flight4 = new Flight
            {
                FromAirport = airport1,
                ToAirport = airport2,
                Departure = new DateTime(2017, 10, 11, 22, 30, 00),
                Price = 500
            };
            flight4.Arrival = flight4.Departure.AddHours(1);

            var flight5 = new Flight
            {
                FromAirport = airport1,
                ToAirport = airport2,
                Departure = new DateTime(2017, 10, 12, 16, 00, 00),
                Price = 800
            };
            flight5.Arrival = flight5.Departure.AddHours(1);

            var flight6 = new Flight
            {
                FromAirport = airport2,
                ToAirport = airport1,
                Departure = new DateTime(2017, 10, 20, 12, 00, 00),
                Price = 600
            };
            flight6.Arrival = flight6.Departure.AddHours(1);

            var flight7 = new Flight
            {
                FromAirport = airport2,
                ToAirport = airport1,
                Departure = new DateTime(2017, 10, 20, 14, 30, 00),
                Price = 800
            };
            flight7.Arrival = flight7.Departure.AddHours(1);

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

            var user1 = new User
            {
                Fornavn = "Kurt",
                Etternavn = "Johansen",
                Adresse = "Brandts gate 2b",
                Poststed = postSted1,
                Epost = "test",
                PassordHash = Controllers.HomeController.HashPassword("test")
            };

            var user2 = new User
            {
                Fornavn = "Rolf",
                Etternavn = "Paulsen",
                Adresse = "Høgglinna 9",
                Poststed = postSted2,
                Epost = "nr9@talas.no",
                PassordHash = Controllers.HomeController.HashPassword("knus")
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
                Epost = "admin",
                PassordHash = Controllers.AdminController.HashPassword("admin")
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

            db.Poststed.Add(postSted1);
            db.Poststed.Add(postSted2);
            db.Users.Add(user1);
            db.Users.Add(user2);
            db.Admins.Add(admin);

            db.Booking.Add(booking1);
            db.Booking.Add(booking2);

            base.Seed(db);
        }
    }
}