using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAppsOppgave1.Models
{
    public class jsAirport
    {
        public int id { get; set; }
        public string name { get; set; }
    }

    public class jsFlight
    {
        public int id { get; set; }
        public string fromAirport { get; set; }
        public string toAirport { get; set; }
        public string departure { get; set; }
        public string arrival { get; set; }
        public int price { get; set; }
    }

    public class JsBooking
    {
        public int flight { get; set; }
        public int amount { get; set; }
    }
}