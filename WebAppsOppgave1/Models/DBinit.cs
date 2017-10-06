﻿using System;
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
                
            };

            var Departure = new DateTime(2017, 10, 10, 8, 30, 00);
            flight1.DepartureDate = Departure.ToString("yyyy-MM-dd");
            flight1.DepartureTime = Departure.ToString("HH:mm");


            //var flight2 = new Flight
            //{
            //    FromAirport = airport1,
            //    ToAirport = airport2,
            //    //Departure = new DateTime(2017, 10, 11, 8, 30, 00)
            //};

            //var flight3 = new Flight
            //{
            //    FromAirport = airport1,
            //    ToAirport = airport2,
            //    //Departure = new DateTime(2017, 10, 11, 16, 00, 00)
            //};

            //var flight4 = new Flight
            //{
            //    FromAirport = airport1,
            //    ToAirport = airport2,
            //    //Departure = new DateTime(2017, 10, 11, 22, 30, 00)
            //};

            //var flight5 = new Flight
            //{
            //    FromAirport = airport1,
            //    ToAirport = airport2,
            //    //Departure = new DateTime(2017, 10, 12, 16, 00, 00)
            //};

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
            //db.Flight.Add(flight2);
            //db.Flight.Add(flight3);
            //db.Flight.Add(flight4);
            //db.Flight.Add(flight5);
            //db.Booking.Add(booking1);
            base.Seed(db);
        }
    }
}