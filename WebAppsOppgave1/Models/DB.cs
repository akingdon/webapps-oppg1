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
            Database.SetInitializer(new DBInit());
        }

        public DbSet<Booking> Booking { get; set; }
        public DbSet<Flight> Flight { get; set; }
        public DbSet<Airport> Airport { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<PostSted> Poststed { get; set; }
    }
}