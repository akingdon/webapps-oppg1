using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAppsOppgave1.Model
{
    public class AdminUser
    {
        [Key]
        public int Id { get; set; }
        public string Epost { get; set; }
        public byte[] PassordHash { get; set; }
    }
}
