using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebAppsOppgave1.Models
{
    public class Booking
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        [Required]
        public Flight Flight { get; set; }
        public byte Amount { get; set; }
    }
}