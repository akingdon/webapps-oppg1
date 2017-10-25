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
using System.Web.Script.Serialization;

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
        public void TestGetAllAirportsWithoutFiltering()
        {
            var Controller = new AdminController(new AdminBLL(new AdminDALStub()));
            var Expected = new List<Airport>();
            var Airport1 = new Airport()
            {
                Id = 1,
                Name = "Torp"
            };
            var Airport2 = new Airport
            {
                Id = 2,
                Name = "Rygge"
            };
            var Airport3 = new Airport
            {
                Id = 3,
                Name = "Torp"
            };
            Expected.Add(Airport1);
            Expected.Add(Airport2);
            Expected.Add(Airport3);

            var ResultJson = Controller.getAllAirports(null);
            var jsonSerializer = new JavaScriptSerializer();
            List<Airport> ResultList = jsonSerializer.Deserialize<List<Airport>>(ResultJson);

            Assert.IsTrue(ResultList.Count == Expected.Count);

            for (var i = 0; i < ResultList.Count; i++)
            {
                Assert.AreEqual(Expected[i].Id, ResultList[i].Id);
                Assert.AreEqual(Expected[i].Name, ResultList[i].Name);
            }
        }

        [TestMethod]
        public void TestGetAllAirportsWithFiltering()
        {
            var Controller = new AdminController(new AdminBLL(new AdminDALStub()));
            var Expected = new List<Airport>();
            var ExpectedName = "Torp";

            var Airport1 = new Airport()
            {
                Id = 1,
                Name = ExpectedName
            };
            var Airport3 = new Airport
            {
                Id = 3,
                Name = ExpectedName
            };
            Expected.Add(Airport1);
            Expected.Add(Airport3);

            var ResultJson = Controller.getAllAirports(ExpectedName);
            var jsonSerializer = new JavaScriptSerializer();
            List<Airport> ResultList = jsonSerializer.Deserialize<List<Airport>>(ResultJson);

            Assert.IsTrue(ResultList.Count == Expected.Count);

            for (var i = 0; i < ResultList.Count; i++)
            {
                Assert.AreEqual(Expected[i].Id, ResultList[i].Id);
                Assert.AreEqual(Expected[i].Name, ResultList[i].Name);
            }
        }

        [TestMethod]
        public void TestGetAllBookingsWithoutFiltering()
        {
            var Controller = new AdminController(new AdminBLL(new AdminDALStub()));
            var Expected = new List<Booking>();

            var User1 = new User
            {
                Fornavn = "Fornavn",
                Etternavn = "Etternavn",
                Adresse = "Adresseveien 2",
                Poststed = new PostSted
                {
                    Postnr = "0123",
                    Poststed = "Oslo"
                },
                Epost = "e@post.no"
            };
            var User2 = new User
            {
                Fornavn = "AnnetFornavn",
                Etternavn = "AnnetEtternavn",
                Adresse = "Postnummerveien 9",
                Poststed = new PostSted
                {
                    Postnr = "9876",
                    Poststed = "Huttiheita"
                },
                Epost = "b@post.no"
            };

            var Flight1 = new Flight
            {
                Id = 8,
                FromAirport = new Airport
                {
                    Name = "Torp"
                },
                Departure = DateTime.Parse("01.11.2017 12:00"),
                ToAirport = new Airport
                {
                    Name = "Stockholm"
                },
                Arrival = DateTime.Parse("01.11.2017 12:50"),
                Price = 900
            };

            var Flight2 = new Flight
            {
                Id = 9,
                FromAirport = new Airport
                {
                    Name = "London"
                },
                Departure = DateTime.Parse("01.11.2017 12:00"),
                ToAirport = new Airport
                {
                    Name = "Oslo"
                },
                Arrival = DateTime.Parse("01.11.2017 14:00"),
                Price = 1200
            };

            var Booking1 = new Booking
            {
                Id = 1,
                Amount = 4,
                User = User1,
                Flight = Flight1,
            };
            var Booking2 = new Booking
            {
                Id = 2,
                Amount = 7,
                User = User1,
                Flight = Flight2,
            };
            var Booking3 = new Booking
            {
                Id = 3,
                Amount = 6,
                User = User2,
                Flight = Flight1,
            };

            var Booking4 = new Booking
            {
                Id = 4,
                Amount = 2,
                User = User2,
                Flight = Flight2,
            };

            Expected.Add(Booking1);
            Expected.Add(Booking2);
            Expected.Add(Booking3);
            Expected.Add(Booking4);

            var ResultJson = Controller.getAllBookings(null, null);
            var jsonSerializer = new JavaScriptSerializer();
            List<JsBooking> ResultList = jsonSerializer.Deserialize<List<JsBooking>>(ResultJson);

            Assert.IsTrue(Expected.Count == ResultList.Count);

            for (var i = 0; i < ResultList.Count; i++)
            {
                Assert.AreEqual(Expected[i].Id, ResultList[i].Id);
                Assert.AreEqual(Expected[i].User.Id, ResultList[i].UserId);
                Assert.AreEqual(Expected[i].User.Fornavn, ResultList[i].UserFirstname);
                Assert.AreEqual(Expected[i].User.Etternavn, ResultList[i].UserLastname);
                Assert.AreEqual(Expected[i].Flight.Id, ResultList[i].FlightId);
                Assert.AreEqual(Expected[i].Flight.FromAirport.Name, ResultList[i].FlightFrom);
                Assert.AreEqual(Expected[i].Flight.ToAirport.Name, ResultList[i].FlightTo);
                Assert.AreEqual(Expected[i].Flight.Departure.ToString("dd.MM.yyyy HH:mm"), ResultList[i].FlightDeparture);
            }
        }

        [TestMethod]
        public void TestGetAllBookingsWithUSerFiltering()
        {
            var Controller = new AdminController(new AdminBLL(new AdminDALStub()));
            var Expected = new List<Booking>();
            var ExpectedUserId = "2";

            var User2 = new User
            {
                Fornavn = "AnnetFornavn",
                Etternavn = "AnnetEtternavn",
                Adresse = "Postnummerveien 9",
                Poststed = new PostSted
                {
                    Postnr = "9876",
                    Poststed = "Huttiheita"
                },
                Epost = "b@post.no"
            };

            var Flight1 = new Flight
            {
                Id = 8,
                FromAirport = new Airport
                {
                    Name = "Torp"
                },
                Departure = DateTime.Parse("01.11.2017 12:00"),
                ToAirport = new Airport
                {
                    Name = "Stockholm"
                },
                Arrival = DateTime.Parse("01.11.2017 12:50"),
                Price = 900
            };

            var Flight2 = new Flight
            {
                Id = 9,
                FromAirport = new Airport
                {
                    Name = "London"
                },
                Departure = DateTime.Parse("01.11.2017 12:00"),
                ToAirport = new Airport
                {
                    Name = "Oslo"
                },
                Arrival = DateTime.Parse("01.11.2017 14:00"),
                Price = 1200
            };

            var Booking3 = new Booking
            {
                Id = 3,
                Amount = 6,
                User = User2,
                Flight = Flight1,
            };

            var Booking4 = new Booking
            {
                Id = 4,
                Amount = 2,
                User = User2,
                Flight = Flight2,
            };

            Expected.Add(Booking3);
            Expected.Add(Booking4);

            
            var ResultJson = Controller.getAllBookings(ExpectedUserId, null);
            var jsonSerializer = new JavaScriptSerializer();
            List<JsBooking> ResultList = jsonSerializer.Deserialize<List<JsBooking>>(ResultJson);

            Assert.IsTrue(Expected.Count == ResultList.Count);

            for (var i = 0; i < ResultList.Count; i++)
            {
                Assert.AreEqual(Expected[i].Id, ResultList[i].Id);
                Assert.AreEqual(Expected[i].User.Id, ResultList[i].UserId);
                Assert.AreEqual(Expected[i].User.Fornavn, ResultList[i].UserFirstname);
                Assert.AreEqual(Expected[i].User.Etternavn, ResultList[i].UserLastname);
                Assert.AreEqual(Expected[i].Flight.Id, ResultList[i].FlightId);
                Assert.AreEqual(Expected[i].Flight.FromAirport.Name, ResultList[i].FlightFrom);
                Assert.AreEqual(Expected[i].Flight.ToAirport.Name, ResultList[i].FlightTo);
                Assert.AreEqual(Expected[i].Flight.Departure.ToString("dd.MM.yyyy HH:mm"), ResultList[i].FlightDeparture);
            }
        }

        [TestMethod]
        public void TestGetAllBookingsWithFlightFiltering()
        {
            var Controller = new AdminController(new AdminBLL(new AdminDALStub()));
            var Expected = new List<Booking>();
            var ExpectedFlightId = "9";

            var User1 = new User
            {
                Fornavn = "Fornavn",
                Etternavn = "Etternavn",
                Adresse = "Adresseveien 2",
                Poststed = new PostSted
                {
                    Postnr = "0123",
                    Poststed = "Oslo"
                },
                Epost = "e@post.no"
            };

            var Flight2 = new Flight
            {
                Id = 9,
                FromAirport = new Airport
                {
                    Name = "London"
                },
                Departure = DateTime.Parse("01.11.2017 12:00"),
                ToAirport = new Airport
                {
                    Name = "Oslo"
                },
                Arrival = DateTime.Parse("01.11.2017 14:00"),
                Price = 1200
            };

            var Booking2 = new Booking
            {
                Id = 2,
                Amount = 7,
                User = User1,
                Flight = Flight2,
            };

            Expected.Add(Booking2);

            var ResultJson = Controller.getAllBookings(null, ExpectedFlightId);
            var jsonSerializer = new JavaScriptSerializer();
            List<JsBooking> ResultList = jsonSerializer.Deserialize<List<JsBooking>>(ResultJson);

            Assert.IsTrue(Expected.Count == ResultList.Count);

            for (var i = 0; i < ResultList.Count; i++)
            {
                Assert.AreEqual(Expected[i].Id, ResultList[i].Id);
                Assert.AreEqual(Expected[i].User.Id, ResultList[i].UserId);
                Assert.AreEqual(Expected[i].User.Fornavn, ResultList[i].UserFirstname);
                Assert.AreEqual(Expected[i].User.Etternavn, ResultList[i].UserLastname);
                Assert.AreEqual(Expected[i].Flight.Id, ResultList[i].FlightId);
                Assert.AreEqual(Expected[i].Flight.FromAirport.Name, ResultList[i].FlightFrom);
                Assert.AreEqual(Expected[i].Flight.ToAirport.Name, ResultList[i].FlightTo);
                Assert.AreEqual(Expected[i].Flight.Departure.ToString("dd.MM.yyyy HH:mm"), ResultList[i].FlightDeparture);
            }
        }

        [TestMethod]
        public void TestGetAllBookingsWithBothFilters()
        {
            var Controller = new AdminController(new AdminBLL(new AdminDALStub()));
            var Expected = new List<Booking>();
            var ExpectedUserId = "1";
            var ExpectedFlightId = "8";

            var User1 = new User
            {
                Fornavn = "Fornavn",
                Etternavn = "Etternavn",
                Adresse = "Adresseveien 2",
                Poststed = new PostSted
                {
                    Postnr = "0123",
                    Poststed = "Oslo"
                },
                Epost = "e@post.no"
            };

            var Flight1 = new Flight
            {
                Id = 8,
                FromAirport = new Airport
                {
                    Name = "Torp"
                },
                Departure = DateTime.Parse("01.11.2017 12:00"),
                ToAirport = new Airport
                {
                    Name = "Stockholm"
                },
                Arrival = DateTime.Parse("01.11.2017 12:50"),
                Price = 900
            };

            var Booking1 = new Booking
            {
                Id = 1,
                Amount = 4,
                User = User1,
                Flight = Flight1,
            };

            Expected.Add(Booking1);

            var ResultJson = Controller.getAllBookings(ExpectedUserId, ExpectedFlightId);
            var jsonSerializer = new JavaScriptSerializer();
            List<JsBooking> ResultList = jsonSerializer.Deserialize<List<JsBooking>>(ResultJson);

            Assert.IsTrue(Expected.Count == ResultList.Count);

            for (var i = 0; i < ResultList.Count; i++)
            {
                Assert.AreEqual(Expected[i].Id, ResultList[i].Id);
                Assert.AreEqual(Expected[i].User.Id, ResultList[i].UserId);
                Assert.AreEqual(Expected[i].User.Fornavn, ResultList[i].UserFirstname);
                Assert.AreEqual(Expected[i].User.Etternavn, ResultList[i].UserLastname);
                Assert.AreEqual(Expected[i].Flight.Id, ResultList[i].FlightId);
                Assert.AreEqual(Expected[i].Flight.FromAirport.Name, ResultList[i].FlightFrom);
                Assert.AreEqual(Expected[i].Flight.ToAirport.Name, ResultList[i].FlightTo);
                Assert.AreEqual(Expected[i].Flight.Departure.ToString("dd.MM.yyyy HH:mm"), ResultList[i].FlightDeparture);
            }
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
