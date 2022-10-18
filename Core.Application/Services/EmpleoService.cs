using Core.Application.Interfaces;
using Core.Domain.AggregatesModel.RRHH;
using Core.Domain.SeedWork;
using Core.Infraestructure;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Services
{
    public class EmpleoService : IEmpleoService
    {
        private readonly ApplicationDBContext _db;

        public EmpleoService(ApplicationDBContext db)
        {
            _db = db;
        }

        public async Task<Response<bool>> ActualizarEmpleosDelCandidato(IEnumerable<Empleo> empleos)
        {
            var response = new Response<bool>();
            try
            {
                foreach (var item in empleos)
                {
                    _db.Empleos.Update(item);
                    await _db.SaveChangesAsync();
                }
                response.Data = true;
                response.IsSuccess = true;
                return response;
            }
            catch (Exception ex)
            {
                response.Data = false;
                response.Message = $"Se produjo un error en ActualizarEmpleosDelCandidato con msj :  {ex.Message}";
                return response;
            }
        }

        public async Task<Response<bool>> CrearEmpleosAlCandidato(IEnumerable<Empleo> empleos , int empleadoId)
        {
            var response = new Response<bool>();
            try
            {
                foreach (var item in empleos)
                {
                    item.CandidatoId = empleadoId;
                    _db.Empleos.Add(item);
                    await _db.SaveChangesAsync();
                }
                response.Data = true;
                response.IsSuccess = true;
                return response;
            }
            catch (Exception ex)
            {
                response.Data = false;
                response.Message = $"Se produjo un error en CrearEmpleosAlCandidato con msj :  {ex.Message}";
                return response;
            }
         
        }
    }
}
