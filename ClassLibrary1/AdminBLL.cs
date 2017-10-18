using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAppsOppgave1.DAL;
using WebAppsOppgave1.Model;

namespace WebAppsOppgave1.BLL
{
    public class AdminBLL
    {
        public AdminUser AdminInDb(string UserName, byte[] HashedPassword)
        {
            var AdminDAL = new AdminDAL();
            return AdminDAL.AdminInDb(UserName, HashedPassword);
        }
        public List<jsAirport> getAllAirports()
        {
            var AdminDAL = new AdminDAL();
            return AdminDAL.getAllAirports();
        }
    }
}
