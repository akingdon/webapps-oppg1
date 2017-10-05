using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAppsOppgave1.Models
{
    public class Flight
    {
        public int Id { get; set; }
        public virtual Airport FromAirport { get; set; }
        public virtual Airport ToAirport { get; set; }
        public DateTime Departure { get; set; }
    }
}