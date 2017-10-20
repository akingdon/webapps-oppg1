﻿using System;
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

    public class JsBooking
    {
        public int flight { get; set; }
        public int amount { get; set; }
    }
}