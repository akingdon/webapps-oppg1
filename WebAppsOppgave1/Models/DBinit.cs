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

            var returnFlight1 = new Flight
            {
                FromAirport = airport2,
                ToAirport = airport1,
                Departure = new DateTime(2017, 10, 20, 12, 00, 00),
                Price = 600
            };
            returnFlight1.Arrival = returnFlight1.Departure.AddHours(1);

            var returnFlight2 = new Flight
            {
                FromAirport = airport2,
                ToAirport = airport1,
                Departure = new DateTime(2017, 10, 20, 14, 30, 00),
                Price = 800
            };
            returnFlight2.Arrival = returnFlight2.Departure.AddHours(1);

            var postSted = new PostSted
            {
                Postnr = "0171",
                Poststed = "Oslo"
            };

            var testUser = new User
            {
                Fornavn = "Kurt",
                Etternavn = "Johnny",
                Adresse = "Brandts gate 2b",
                Poststed = postSted,
                Epost = "test",
                PassordHash = Controllers.HomeController.HashPassword("test")
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

            db.Flight.Add(returnFlight1);
            db.Flight.Add(returnFlight2);

            db.Poststed.Add(postSted);
            db.Users.Add(testUser);
            db.Admins.Add(admin);
            

            base.Seed(db);
        }
    }
}