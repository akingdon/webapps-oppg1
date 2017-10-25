using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Mvc;
using System.Linq;
using WebAppsOppgave1.Controllers;
using WebAppsOppgave1.BLL;
using WebAppsOppgave1.DAL;
using System.Collections.Generic;
using WebAppsOppgave1.Model;
using MvcContrib.TestHelper;

namespace UnitTest
{
    [TestClass]
    public class AdminControllerTest
    {
        [TestMethod]
        public void TestShowIndexViewLoggedIn()
        {
            var SessionMock = new TestControllerBuilder();
            var Controller = new AdminController();
            SessionMock.InitializeController(Controller);
            Controller.Session["Admin"] = true;
            var Result = (ViewResult)Controller.Index();
            Assert.AreEqual(Result.ViewName, "");
        }

        [TestMethod]
        public void TestRedirectFromIndexToLogInWhenSessionValueIsFalse()
        {
            var SessionMock = new TestControllerBuilder();
            var Controller = new AdminController();
            SessionMock.InitializeController(Controller);
            Controller.Session["Admin"] = false;
            var Result = (RedirectToRouteResult)Controller.Index();

            Assert.AreEqual("", Result.RouteName);
            Assert.AreEqual("LogIn", Result.RouteValues.Values.First());
        }

        [TestMethod]
        public void TestRedirectFromIndexToLogInWhenSesssionValueIsNull()
        {
            var SessionMock = new TestControllerBuilder();
            var Controller = new AdminController();
            SessionMock.InitializeController(Controller);
            Controller.Session["Admin"] = null;
            var Result = (RedirectToRouteResult)Controller.Index();

            Assert.AreEqual("", Result.RouteName);
            Assert.AreEqual("LogIn", Result.RouteValues.Values.First());
        }

        [TestMethod]
        public void TestShowLogInView()
        {
            var Controller = new AdminController();
            var Result = (ViewResult)Controller.LogIn();
            Assert.AreEqual(Result.ViewName, "");
        }

        [TestMethod]
        public void TestShowLogOutView()
        {
            var SessionMock = new TestControllerBuilder();
            var Controller = new AdminController();
            SessionMock.InitializeController(Controller);
            Controller.Session["Admin"] = true;
            var Result = (ViewResult)Controller.LogOut();
            Assert.AreEqual(Result.ViewName, "");
            Assert.IsFalse((bool)Controller.Session["Admin"]);
            Assert.IsFalse((bool)Controller.ViewData["Admin"]);
        }

        [TestMethod]
        public void TestAdminLogin()
        {
            var SessionMock = new TestControllerBuilder();
            var Controller = new AdminController(new AdminBLL(new AdminDALStub()));
            SessionMock.InitializeController(Controller);
            var form = new FormCollection();
            form.Add("username", "admin");
            form.Add("password", "admin");

            var Result = (ViewResult)Controller.Index(form);
            Assert.AreEqual("", Result.ViewName);
            Assert.IsTrue((bool) Controller.Session["Admin"]);
            Assert.IsTrue((bool) Controller.ViewData["Admin"]);            
        }

        [TestMethod]
        public void TestAdminLoginWithIncorrectCredentials()
        {
            var SessionMock = new TestControllerBuilder();
            var Controller = new AdminController(new AdminBLL(new AdminDALStub()));
            SessionMock.InitializeController(Controller);
            var form = new FormCollection();
            form.Add("username", "not_admin");
            form.Add("password", "admin");

            var Result = (RedirectToRouteResult)Controller.Index(form);
            Assert.AreEqual("", Result.RouteName);
            Assert.AreEqual("LogIn", Result.RouteValues.Values.First());
            Assert.IsFalse((bool)Controller.Session["Admin"]);
            Assert.IsFalse((bool)Controller.ViewData["Admin"]);
        }

        [TestMethod]
        public void TestMethod1()
        {
            var Controller = new AdminController(new AdminBLL(new AdminDALStub()));
            var Expected = new List<User>();
            var User = new User()
            {

            };
            Expected.Add(User);
            Expected.Add(User);
            Expected.Add(User);

            //var Result = (ViewResult)Controller.;
            //var ResultList = (List<User>)Result.Model;

            //Assert.AreEqual(Result.ViewName, "");

            //for (var i = 0; i < ResultList.Count; i++)
            //{
                //Assert.AreEqual(Expected[i]., ResultList[i].);
            //}
        }

        [TestMethod]
        public void TestGetAirport()
        {
            var Controller = new AdminController(new AdminBLL(new AdminDALStub()));
            var id = 1;
            var Result = Controller.getAirport(id);
            Assert.AreEqual("{\"id\":0,\"name\":\"Torp\"}", Result);
        }

        [TestMethod]
        public void TestGetAirportWithNegativeId()
        {
            var Controller = new AdminController(new AdminBLL(new AdminDALStub()));
            var id = -1;
            var Result = Controller.getAirport(id);
            Assert.IsNull(Result);
        }

        [TestMethod]
        public void TestGetBooking()
        {
            var Controller = new AdminController(new AdminBLL(new AdminDALStub()));
            var id = 1;
            var Result = Controller.getBooking(id);
            Assert.AreEqual("{\"Id\":1,\"UserId\":1,\"UserFirstname\":\"fornavn\",\"UserLastname\":\"etternavn\",\"FlightId\":1,\"FlightFrom\":\"Torp\",\"FlightTo\":\"Rygge\",\"FlightDeparture\":\"01.11.2017 12.00\",\"Amount\":7}", Result);
        }

        [TestMethod]
        public void TestGetBookingWithNegativeId()
        {
            var Controller = new AdminController(new AdminBLL(new AdminDALStub()));
            var id = -1;
            var Result = Controller.getBooking(id);
            Assert.IsNull(Result);
        }

        [TestMethod]
        public void TestGetUser()
        {
            var Controller = new AdminController(new AdminBLL(new AdminDALStub()));
            var id = 1;
            var Result = Controller.getUser(id);
            Assert.AreEqual("{\"Id\":1,\"Fornavn\":\"Fornavn\",\"Etternavn\":\"Etternavn\",\"Adresse\":\"Adresseveien 2\",\"Postnummer\":\"0123\",\"Poststed\":\"Oslo\",\"Epost\":\"e@post.no\"}", Result);
        }

        [TestMethod]
        public void TestGetUserWithNegativeId()
        {
            var Controller = new AdminController(new AdminBLL(new AdminDALStub()));
            var id = -1;
            var Result = Controller.getUser(id);
            Assert.IsNull(Result);
        }

        [TestMethod]
        public void TestGetFlight()
        {
            var Controller = new AdminController(new AdminBLL(new AdminDALStub()));
            var id = 1;
            var Result = Controller.getFlight(id);
            Assert.AreEqual("{\"id\":1,\"fromAirportId\":1,\"fromAirportName\":\"Torp\",\"toAirportId\":2,\"toAirportName\":\"Ikke Torp\",\"departure\":\"01.11.2017 12.00\",\"arrival\":\"01.11.2017 12.50\",\"price\":300}", Result);
        }

        [TestMethod]
        public void TestGetFlightWithNegativeId()
        {
            var Controller = new AdminController(new AdminBLL(new AdminDALStub()));
            var id = -1;
            var Result = Controller.getFlight(id);
            Assert.IsNull(Result);
        }

        [TestMethod]
        public void TestRegisterAirport()
        {
            var Controller = new AdminController(new AdminBLL(new AdminDALStub()));
            var AirportName = "Torp";
            var Result = Controller.registerAirport(AirportName);
            Assert.AreEqual("\"ok\"", Result);
        }

        [TestMethod]
        public void TestRegisterBooking()
        {
            var Controller = new AdminController(new AdminBLL(new AdminDALStub()));
            var userId = 2;
            var flightId = 3;
            var amount = 7;
            var Result = Controller.registerBooking(userId, flightId, amount);
            Assert.AreEqual("\"ok\"", Result);
        }

        [TestMethod]
        public void TestRegisterBookingWithoutPassengerAmount()
        {
            var Controller = new AdminController(new AdminBLL(new AdminDALStub()));
            var userId = 2;
            var flightId = 3;
            var Result = Controller.registerBooking(userId, flightId, 0);
            Assert.AreEqual("\"Adding to DB failed\"", Result);
        }

        [TestMethod]
        public void TestRegisterUser()
        {
            var Controller = new AdminController(new AdminBLL(new AdminDALStub()));
            var fornavn = "fornavn";
            var etternavn = "etternavn";
            var adresse = "adresseveien 1";
            var postnummer = "0001";
            var poststed = "poststed";
            var epost = "e@post.no";
            var passord = "passord";
            var Result = Controller.registerUser(fornavn, etternavn, adresse, postnummer, poststed, epost, passord);
            Assert.AreEqual("\"ok\"", Result);
        }

        [TestMethod]
        public void TestRegisterFlight()
        {
            var Controller = new AdminController(new AdminBLL(new AdminDALStub()));
            var fromAirportId = 1;
            var toAirportId = 2;
            var departure = DateTime.Parse("01.11.2017 12:00");
            var arrival = DateTime.Parse("01.11.2017 13:30");
            var price = 800;
            var Result = Controller.registerFlight(fromAirportId, toAirportId, departure, arrival, price);
            Assert.AreEqual("\"ok\"", Result);
        }

        [TestMethod]
        public void TestRegisterFlightWithoutPrice()
        {
            var Controller = new AdminController(new AdminBLL(new AdminDALStub()));
            var fromAirportId = 1;
            var toAirportId = 2;
            var departure = DateTime.Parse("01.11.2017 12:00");
            var arrival = DateTime.Parse("01.11.2017 13:30");
            var price = 0;
            var Result = Controller.registerFlight(fromAirportId, toAirportId, departure, arrival, price);
            Assert.AreEqual("\"Adding to DB failed\"", Result);
        }

        [TestMethod]
        public void TestEditAirport()
        {
            var Controller = new AdminController(new AdminBLL(new AdminDALStub()));
            var id = 1;
            var name = "Rygge";
            var Result = Controller.editAirport(id, name);
            Assert.AreEqual("\"ok\"", Result);
        }

        [TestMethod]
        public void TestEditAirportWithNegativeId()
        {
            var Controller = new AdminController(new AdminBLL(new AdminDALStub()));
            var id = -1;
            var name = "Rygge";
            var Result = Controller.editAirport(id, name);
            Assert.AreEqual("\"Editing in DB failed\"", Result);
        }

        [TestMethod]
        public void TestEditBooking()
        {
            var Controller = new AdminController(new AdminBLL(new AdminDALStub()));
            var id = 1;
            var userId = 2;
            var flightId = 3;
            var amount = 4;
            var Result = Controller.editBooking(id, userId, flightId, amount);
            Assert.AreEqual("\"ok\"", Result);
        }

        [TestMethod]
        public void TestEditBookingWithNegativeId()
        {
            var Controller = new AdminController(new AdminBLL(new AdminDALStub()));
            var id = -1;
            var userId = 2;
            var flightId = 3;
            var amount = 4;
            var Result = Controller.editBooking(id, userId, flightId, amount);
            Assert.AreEqual("\"Editing in DB failed\"", Result);
        }

        [TestMethod]
        public void TestEditUser()
        {
            var Controller = new AdminController(new AdminBLL(new AdminDALStub()));
            var id = 1;
            var fornavn = "Fornavn";
            var etternavn = "Etternavn";
            var adresse = "Adresseveien 2";
            var postnummer = "0123";
            var poststed = "Oslo";
            var epost = "e@post.no";
            var passord = "passord";
            var Result = Controller.editUser(id, fornavn, etternavn, adresse, postnummer, poststed, epost, passord);
            Assert.AreEqual("\"ok\"", Result);
        }

        [TestMethod]
        public void TestEditUserWithNegativeId()
        {
            var Controller = new AdminController(new AdminBLL(new AdminDALStub()));
            var id = -1;
            var fornavn = "Fornavn";
            var etternavn = "Etternavn";
            var adresse = "Adresseveien 2";
            var postnummer = "0123";
            var poststed = "Oslo";
            var epost = "e@post.no";
            var passord = "passord";
            var Result = Controller.editUser(id, fornavn, etternavn, adresse, postnummer, poststed, epost, passord);
            Assert.AreEqual("\"Editing in DB failed\"", Result);
        }

        [TestMethod]
        public void TestEditFlight()
        {
            var Controller = new AdminController(new AdminBLL(new AdminDALStub()));
            var id = 1;
            var fromAirportId = 2;
            var toAirportId = 3;
            var departure = DateTime.Parse("01.11.2017 12:00");
            var arrival = DateTime.Parse("01.11.2017 12:50");
            var price = 350;
            var Result = Controller.editFlight(id, fromAirportId, toAirportId, departure, arrival, price);
            Assert.AreEqual("\"ok\"", Result);
        }

        [TestMethod]
        public void TestEditFlightWithNegativeId()
        {
            var Controller = new AdminController(new AdminBLL(new AdminDALStub()));
            var id = -1;
            var fromAirportId = 2;
            var toAirportId = 3;
            var departure = DateTime.Parse("01.11.2017 12:00");
            var arrival = DateTime.Parse("01.11.2017 12:50");
            var price = 350;
            var Result = Controller.editFlight(id, fromAirportId, toAirportId, departure, arrival, price);
            Assert.AreEqual("\"Editing in DB failed\"", Result);
        }

        [TestMethod]
        public void TestDeleteAirport()
        {
            var Controller = new AdminController(new AdminBLL(new AdminDALStub()));
            var id = 1;
            var Result = Controller.deleteAirport(id);
            Assert.AreEqual("\"ok\"", Result);
        }

        [TestMethod]
        public void TestDeleteAirportWithNegativeId()
        {
            var Controller = new AdminController(new AdminBLL(new AdminDALStub()));
            var id = -1;
            var Result = Controller.deleteAirport(id);
            Assert.AreEqual("\"Deleting from DB failed\"", Result);
        }

        [TestMethod]
        public void TestDeleteBooking()
        {
            var Controller = new AdminController(new AdminBLL(new AdminDALStub()));
            var id = 1;
            var Result = Controller.deleteBooking(id);
            Assert.AreEqual("\"ok\"", Result);
        }

        [TestMethod]
        public void TestDeleteBookingWithNegativeId()
        {
            var Controller = new AdminController(new AdminBLL(new AdminDALStub()));
            var id = -1;
            var Result = Controller.deleteBooking(id);
            Assert.AreEqual("\"Deleting from DB failed\"", Result);
        }

        [TestMethod]
        public void TestDeleteUser()
        {
            var Controller = new AdminController(new AdminBLL(new AdminDALStub()));
            var id = 1;
            var Result = Controller.deleteUser(id);
            Assert.AreEqual("\"ok\"", Result);
        }

        [TestMethod]
        public void TestDeleteUserWithNegativeId()
        {
            var Controller = new AdminController(new AdminBLL(new AdminDALStub()));
            var id = -1;
            var Result = Controller.deleteUser(id);
            Assert.AreEqual("\"Deleting from DB failed\"", Result);
        }

        [TestMethod]
        public void TestDeleteFlight()
        {
            var Controller = new AdminController(new AdminBLL(new AdminDALStub()));
            var id = 1;
            var Result = Controller.deleteFlight(id);
            Assert.AreEqual("\"ok\"", Result);
        }

        [TestMethod]
        public void TestDeleteFlightWithNegativeId()
        {
            var Controller = new AdminController(new AdminBLL(new AdminDALStub()));
            var id = -1;
            var Result = Controller.deleteFlight(id);
            Assert.AreEqual("\"Deleting from DB failed\"", Result);
        }

        [TestMethod]
        public void TestAdminInDb()
        {
            var Controller = new AdminController(new AdminBLL(new AdminDALStub()));
            var form = new FormCollection();
            form.Add("username", "admin");
            form.Add("password", "admin");
            var Result = Controller.AdminInDb(form);
            Assert.IsTrue(Result);
        }

        [TestMethod]
        public void TestAdminInDbFalse()
        {
            var Controller = new AdminController(new AdminBLL(new AdminDALStub()));
            var form = new FormCollection();
            form.Add("username", "not_admin");
            form.Add("password", "admin");
            var Result = Controller.AdminInDb(form);
            Assert.IsFalse(Result);
        }
    }
}
