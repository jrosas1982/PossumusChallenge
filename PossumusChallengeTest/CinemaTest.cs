using API.PossumusChallenge.Controllers.v1;
using Core.Application.Interfaces;
using Core.Application.Services;
using Core.Domain.AggregatesModel.Cinema;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PossumusChallengeTest
{
    [TestClass]
    public class CinemaTest : BaseUnitTest
    {
        private string dbName;

        [TestInitialize]
        public async Task CreateCinemasInDataBase()
        {

            dbName = Guid.NewGuid().ToString();
            var contex1 = ContextBuild(dbName);

            //Create a cinema List
            var cinemaTest = new List<Cinema>();
            //Create 2 cinemas
            var c1 = new Cinema()
            {
                CinemaId = 1,
                Address = "Caballito 134",
                Name = "Cinemax"
            };
            var c2 = new Cinema()
            {
                CinemaId = 2,
                Address = "Pilar 3665",
                Name = "CineMark"
            };
            // add cinemas created
            cinemaTest.Add(c1);
            cinemaTest.Add(c2);
            //Save it in a memory database
            foreach (var item in cinemaTest)
            {
                contex1.Cinemas.Add(item);
                await contex1.SaveChangesAsync();
            }
        }
        [TestMethod]
        public async Task GetCinemas()
        {
            //Prepare
            //Create a second memmory database with the same GUID
            var contex2 = ContextBuild(dbName);
            //Use cinemaServices looking for the cinemas just created
            var cinemaServices = new CinemaService(contex2);
            //Save cinemaList
            var list = await cinemaServices.GetCinemas();
            //
            var count = list.Data.Count();
            var nameCinema1 = list.Data.Where(x => x.CinemaId == 1).Select(x => x.Name).FirstOrDefault();

            // Test
            Assert.AreEqual(count, 2);
            Assert.AreEqual(nameCinema1, "Cinemax");

        }
        [TestMethod]
        public async Task UptadeCinema()
        {
            var c3 = new Cinema()
            {
                CinemaId = 2,
                Address = "Rivadavia 64",
                Name = "Cinepolis"
            };
            var contex2 = ContextBuild(dbName);

            var cinemaServices = new CinemaService(contex2);

            await cinemaServices.UpdateCinema(c3);
            var list = await cinemaServices.GetCinemas();
            //
            var count = list.Data.Count();
            var nameCinema2 = list.Data.Where(x => x.CinemaId == 2).Select(x => x.Name).FirstOrDefault();

            Assert.AreEqual(count, 2);
            Assert.AreEqual(nameCinema2, "Cinepolis");
        }
        [TestMethod]
        public async Task CreateCinema()
        {
            var c3 = new Cinema()
            {
                Address = "Paseo La Plaza 8764",
                Name = "Hoyts"
            };
            var contex2 = ContextBuild(dbName);
            //Use cinemaServices looking for the cinemas just created
            var cinemaServices = new CinemaService(contex2);
            //Save cinemaList
           var a =  await cinemaServices.CreateCinema(c3);
            //
            var list = await cinemaServices.GetCinemas();

            var count = list.Data.Count();
            var nameCinema1 = list.Data.Where(x => x.CinemaId == 3).Select(x => x.Name).FirstOrDefault();

            // Test
            Assert.AreEqual(count, 3);
            Assert.AreEqual(nameCinema1, "Hoyts");
        }
        [TestMethod]
        public async Task CreateCinemaIdDuplicado()
        {
            var c3 = new Cinema()
            {
                CinemaId = 9,
                Address = "Paseo La Plaza 8764",
                Name = "Hoyts"
            };
            var contex2 = ContextBuild(dbName);
            //Use cinemaServices looking for the cinemas just created
            var cinemaServices = new CinemaService(contex2);
            //Save cinemaList
            var a = await cinemaServices.CreateCinema(c3);
            //
            var list = await cinemaServices.GetCinemas();

            var count = list.Data.Count();
            var nameCinema1 = list.Data.Where(x => x.CinemaId == 3).Select(x => x.Name).FirstOrDefault();

            // Test
            Assert.AreEqual(count, 3);
        }
        [TestMethod]
        public async Task DeleteCinema()
        {
            var contex2 = ContextBuild(dbName);
            //Use cinemaServices looking for the cinemas just created
            var cinemaServices = new CinemaService(contex2);
            //Save cinemaList
            await cinemaServices.DeleteCinema(1);
            //
            var list = await cinemaServices.GetCinemas();
            var count = list.Data.Count();

            // Test
            Assert.AreEqual(count, 1);
        }


        }
}
