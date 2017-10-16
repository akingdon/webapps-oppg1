using System.ComponentModel.DataAnnotations;

namespace WebAppsOppgave1.Models
{
    public class PostSted
    {
        [Key]
        public string Postnr { get; set; }
        public string Poststed { get; set; }
    }
}