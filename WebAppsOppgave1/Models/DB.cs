using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace WebAppsOppgave1.Models
{
    public class DB : DbContext
    {
        public DB()
            : base("name=Booking")
        {
            Database.CreateIfNotExists();
        }

        public DbSet<Booking> Booking { get; set; }
    }
}