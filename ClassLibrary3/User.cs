using System.ComponentModel.DataAnnotations;

namespace WebAppsOppgave1.Model
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string Fornavn { get; set; }
        public string Etternavn { get; set; }
        public string Adresse { get; set; }
        public virtual PostSted Poststed { get; set; }
        public string Epost { get; set; }
        public byte[] PassordHash { get; set; }
    }
}