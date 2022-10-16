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
        private readonly ICinemaService _cinemaService;

        [TestMethod]
        public async Task GetCinemas()
        {
            //Prepare
            //Name memmory database
            var dbName = Guid.NewGuid().ToString();
            //create a test context
            var contex1 = ContextBuild(dbName);

            //Create a cinema List
            var cinemaTest = new List<Cinema>();
            //Create 2 cinemas
            var c1 =  new Cinema()
                {
                    CinemaId = 1,
                    Address = "Caballito 134",
                    Name = "Cinemax"
                };
            var c2 =  new Cinema()
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
            //Create a second memmory databes with the same GUID
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
        //          var mapper = ConfigureAutoMapper();

    }
}
