﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebAppsOppgave1.Model
{
    public class Flight
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int FlightId { get; set; }
        public virtual Airport FromAirport { get; set; }
        public virtual Airport ToAirport { get; set; }
        public DateTime Departure { get; set; }
        public DateTime Arrival { get; set; }
        public virtual Booking Bookings { get; set; }
        public int Price { get; set; }
    }
}