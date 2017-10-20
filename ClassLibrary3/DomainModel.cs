using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAppsOppgave1.Model
{
    public class jsAirport
    {
        public int id { get; set; }
        public string name { get; set; }
    }

    public class jsFlight
    {
        public int id { get; set; }
        public int fromAirportId { get; set; }
        public string fromAirportName { get; set; }
        public int toAirportId { get; set; }
        public string toAirportName { get; set; }
        public string departure { get; set; }
        public string arrival { get; set; }
        public int price { get; set; }
    }

    public class JsUser
    {
        public int Id { get; set; }
        public string Fornavn { get; set; }
        public string Etternavn { get; set; }
        public string Adresse { get; set; }
        public string Postnummer { get; set; }
        public string Poststed { get; set; }
        public string Epost { get; set; }
    }

    public class JsBooking
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string UserFirstname { get; set; }
        public string UserLastname { get; set; }
        public int FlightId { get; set; }
        public string FlightFrom { get; set; }
        public string FlightTo { get; set; }
        public string FlightDeparture { get; set; }
        public int Amount { get; set; }
    }
}