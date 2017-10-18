using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebAppsOppgave1.Model;

namespace WebAppsOppgave1.DAL
{
    public class DB : DbContext
    {
        public DB()
            : base("name=Booking")
        {
            Database.CreateIfNotExists();
        }

        public DbSet<Booking> Booking { get; set; }
        public DbSet<Flight> Flight { get; set; }
        public DbSet<Airport> Airport { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<PostSted> Poststed { get; set; }
    }
}