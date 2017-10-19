using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAppsOppgave1.Model
{
    public class Airport
    {
        public int Id { get; set; }
        public String Name { get; set; }
        public virtual Flight Flight {get; set; }
    }
}