using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAppsOppgave1.Model;

namespace WebAppsOppgave1.DAL
{
    public class AdminDAL
    {
        public AdminUser AdminInDb(string UserName, byte[] HashedPassword)
        {
            DB Db = new DB();
            AdminUser user = Db.Admins.FirstOrDefault(a => a.PassordHash == HashedPassword && a.Epost == UserName);
            return user;
        }
        public List<jsAirport> getAllAirports()
        {
            DB Db = new DB();
            List<jsAirport> allAirports = Db.Airport.Select(a => new jsAirport()
            {
                id = a.Id,
                name = a.Name
            }).ToList();
            return allAirports;
        }
    }
}
