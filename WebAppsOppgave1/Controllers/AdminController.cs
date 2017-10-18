using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using WebAppsOppgave1.Model;
using WebAppsOppgave1.BLL;
using System.Web.Script.Serialization;

namespace WebAppsOppgave1.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            if (Session["Admin"] == null)
            {
                Session["Admin"] = false;
                ViewBag.Admin = false;
            }
            else
            {
                ViewBag.Admin = (bool)Session["Admin"];
            }

            return View();
        }

        [HttpPost]
        public ActionResult Index(FormCollection form)
        {
            if (AdminInDb(form))
            {
                Session["Admin"] = true;
                ViewBag.Admin = true;
                return View();
            }
            else
            {
                Session["Admin"] = false;
                ViewBag.Admin = false;
                return View();
            }
        }

        public ActionResult LogIn()
        {
            return View();
        }

        private static bool AdminInDb(FormCollection form)
        {
            var AdminBLL = new AdminBLL();
            
            var UserName = form["username"];
            byte[] HashedPassword = HashPassword(form["password"]);
            var AdminUser = AdminBLL.AdminInDb(UserName, HashedPassword);

            if (AdminUser == null)
            {
                return false;
            }
            else
            {
                return true;
            }
            
        }
        public static byte[] HashPassword(string p)
        {
            byte[] input, output;
            var hashing = System.Security.Cryptography.SHA256.Create();
            input = System.Text.Encoding.ASCII.GetBytes(p);
            output = hashing.ComputeHash(input);

            return output;
        }

        public string getAllAirports()
        {
            var AdminBLL = new AdminBLL();
            List<jsAirport> airports = AdminBLL.getAllAirports();
            var jsonSerializer = new JavaScriptSerializer();
            return jsonSerializer.Serialize(airports);
        }

    }
}