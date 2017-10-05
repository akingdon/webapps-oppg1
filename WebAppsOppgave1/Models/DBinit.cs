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
                Departure = new DateTime(2017, 10, 9, 8, 30, 00)
            };

            var booking1 = new Booking
            {
                CustomerId = 1,
                Flight = flight1,
                Amount = 2
            };


            db.Airport.Add(airport1);
            db.Airport.Add(airport2);
            db.Airport.Add(airport3);
            db.Airport.Add(airport4);
            db.Flight.Add(flight1);
            db.Booking.Add(booking1);
            base.Seed(db);
        }
    }
}