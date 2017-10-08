using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using WebAppsOppgave1.Models;

namespace WebAppsOppgave1.Models
{
    public class BookingLogic
    {
        
        DB Db = new DB();

        public List<jsAirport> getAllAirports()
        {
            List<jsAirport> allAirports = Db.Airport.Select(a => new jsAirport()
            {
                id = a.Id,
                name = a.Name
            }).ToList();
            return allAirports;
        }

        public List<Flight> getMatchingflights(int from, int to)
        {
            List<Flight> matchingFlights =
                Db.Flight.Where(a => a.FromAirport.Id == from &&
                    a.ToAirport.Id == to).ToList();
            return matchingFlights;
        }

        public List<Flight> getMatchingflightsOnDate(int from, int to, DateTime date)
        {
            List<Flight> matchingFlights =
                Db.Flight.Where(a => a.FromAirport.Id == from &&
                    a.ToAirport.Id == to && DbFunctions.TruncateTime(a.Departure) == date.Date).ToList();
            return matchingFlights;
        }
    /*    
        public Boolean DestinationsAreSet(int from, int to)
        {
            if (from!=null && )
        }

        public Boolean DestinationsAreDifferent(Booking booking)
        {
            return !booking.FromDestination.Equals(booking.ToDestination);
        }

        public Boolean DepartureDateIsBeforeReturnDate(Booking booking)
        {
            return booking.FromDate < booking.ToDate;
        }

        public Boolean DepartureDateIsBeforeToday(Booking booking)
        {
            return booking.FromDate < DateTime.Today;
        }*/
    }
}