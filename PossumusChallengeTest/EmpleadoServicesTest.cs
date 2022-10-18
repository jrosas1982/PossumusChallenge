using Core.Application.Services;
using Core.Domain.AggregatesModel.RRHH;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PossumusChallengeTest
{
    [TestClass]
    public class EmpleadoServicesTest : BaseUnitTest
    {
        //Se agregan algunos casos de test (HappyPath) solo a modo de ejemplo de implementación de MSTest,
        //no da cobertura total a los casos de uso del challenge 

        private string dbName;

        [TestInitialize]
        public async Task CrearbaseEmpleadoConEmpleos()
        {

            dbName = Guid.NewGuid().ToString();
            var contex1 = ContextBuild(dbName);

            var CandidatosList = new List<Candidato>();
            var EmpleosList1 = new List<Empleo>();
            var EmpleosList2 = new List<Empleo>();
            var empleo1 = new Empleo() { 
                NombreEmpresa ="Fravega",
                Periodo = "11/2021 - 12/2022",
                CandidatoId = 1
            };
            var empleo2 = new Empleo()
            {
                NombreEmpresa = "Rodo",
                Periodo = "11/2020 - 10/2021",
                CandidatoId = 1
            };
            var empleo3 = new Empleo()
            {
                NombreEmpresa = "Ford",
                Periodo = "11/2021 - 12/2022",
                CandidatoId = 2
            };
            var empleo4 = new Empleo()
            {
                NombreEmpresa = "Fiat",
                Periodo = "11/2020 - 10/2021",
                CandidatoId = 2
            };
            EmpleosList1.Add(empleo1);
            EmpleosList1.Add(empleo2);
            EmpleosList2.Add(empleo3);
            EmpleosList2.Add(empleo4);
            var c1 = new Candidato()
            {
                CandidatoId = 1,
                Nombre = "Francisco",
                Apellido = "Rosas",
                Teléfono = "1146665101",
                FecNacimiento = DateTime.Now,
                Email = "frosas@gmail.com",
                Empleos = EmpleosList1            
            };
            var c2 = new Candidato()
            {
                CandidatoId = 2,
                Nombre = "Vicente",
                Apellido = "Rosas",
                Teléfono = "1146660103",
                FecNacimiento = DateTime.Now,
                Email = "vrosas@gmail.com",
                Empleos = EmpleosList2
            };
            CandidatosList.Add(c1);
            CandidatosList.Add(c2);
            foreach (var item in CandidatosList)
            {
                contex1.Candidatos.Add(item);
                await contex1.SaveChangesAsync();
            }
        }
        [TestMethod]
        public async Task CantidadDeEmpleosParaUnCandidato()
        {
            //Prepare ([TestInitialize])
            var contex2 = ContextBuild(dbName);
            var candidatoService = new CandidatoService(contex2);

            var list = await candidatoService.GetCandidatoById(1);
            var cantidadEmpleos = list.Data.Empleos.Count;
            // Test
            Assert.AreEqual(cantidadEmpleos, 2);
        }
        [TestMethod]
        public async Task CrearCandidatoConEmpleos()
        {
            //Prepare ([TestInitialize]) + ---->
            var contex2 = ContextBuild(dbName);
            var EmpleosList1 = new List<Empleo>();
            var empleo1 = new Empleo()
            {
                NombreEmpresa = "Possumus",
                Periodo = "11/2022 - Actualidad",
                CandidatoId = 3
            };
            EmpleosList1.Add(empleo1);
            var JPR = new Candidato()
            {
                CandidatoId = 3,
                Nombre = "Juan Pablo",
                Apellido = "Rosas",
                Teléfono = "1133883964",
                FecNacimiento = DateTime.Now,
                Email = "jrosas1982@gmail.com",
                Empleos = EmpleosList1
            };
            //Execute
            //------->
            var candidatoService = new CandidatoService(contex2);
            await candidatoService.CrearCandidato(JPR);
            var candidato3 = await candidatoService.GetCandidatoById(3);
            //Test
            //------->
            Assert.AreEqual(candidato3.Data.Empleos.Count, 1);
            Assert.AreEqual(candidato3.Data.Email, "jrosas1982@gmail.com");
        }
        [TestMethod]
        public async Task ActualizaUnSoloCandidato_MantieneLosEmpleosYaCargados()
        {
            //Prepare ([TestInitialize]) + ---->
            var contex2 = ContextBuild(dbName);
            var JPR = new Candidato()
            {
                CandidatoId = 2,
                Nombre = "Juan Pablo Ignacio",
                Apellido = "Rosas",
                Teléfono = "1133883964",
                FecNacimiento = DateTime.Now,
                Email = "jrosas1982@gmail.com"
            };
            //Execute
            //------->
            var candidatoService = new CandidatoService(contex2);
            await candidatoService.UpdateCandidato(JPR);
            var candidato3 = await candidatoService.GetCandidatoById(2);
            //Test
            //------->
            Assert.AreEqual(candidato3.Data.Empleos.Count, 2);
            Assert.AreEqual(candidato3.Data.Email, "jrosas1982@gmail.com");
            Assert.AreEqual(candidato3.Data.Nombre, "Juan Pablo Ignacio");
        }

    }
}
