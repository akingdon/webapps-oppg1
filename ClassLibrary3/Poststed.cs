using System.ComponentModel.DataAnnotations;

namespace WebAppsOppgave1.Model
{
    public class PostSted
    {
        [Key]
        public string Postnr { get; set; }
        public string Poststed { get; set; }
    }
}