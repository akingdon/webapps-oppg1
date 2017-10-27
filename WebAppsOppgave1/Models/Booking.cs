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
        public virtual User User { get; set; }
        public virtual Flight Flight { get; set; }
        public int Amount { get; set; }
    }
}