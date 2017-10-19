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
        public jsAirport getAirport(int id)
        {
            var AdminDAL = new AdminDAL();
            return AdminDAL.getAirport(id);
        }
        public string registerAirport(string name)
        {
            var AdminDAL = new AdminDAL();
            return AdminDAL.registerAirport(name);
        }
        public string editAirport(int id, string name)
        {
            var AdminDAL = new AdminDAL();
            return AdminDAL.editAirport(id, name);
        }
        public string deleteAirport(int id)
        {
            var AdminDAL = new AdminDAL();
            return AdminDAL.deleteAirport(id);
        }

        public List<Flight> getAllFlights()
        {
            var AdminDAL = new AdminDAL();
            return AdminDAL.getAllFlights();
        }
    }
}
