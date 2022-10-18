using Core.Application.Interfaces;
using Core.Domain.AggregatesModel.RRHH;
using Core.Domain.SeedWork;
using Core.Infraestructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Services
{
    public class CandidatoService : ICandidatoService
    {
        private readonly ApplicationDBContext _db;
        public CandidatoService(ApplicationDBContext db )
        {
            _db = db;
        }
        public async Task<Response<bool>> CrearCandidato(Candidato candidato)
        {
            var response = new Response<bool>();
            try
            {
                _db.Candidatos.Add(candidato);
                await _db.SaveChangesAsync();
                response.Data = true;
                response.IsSuccess = true;
                return response;
            }
            catch (Exception ex)
            {
                response.Data = false;
                response.Message = $"Error CrearCandidato :  {ex.Message}";
                return response;
            }
        }
        public async Task<Response<bool>> DeleteCandidato(int candidatoId)
        {
            var response = new Response<bool>();
            try
            {
                var empleadoToDelete = _db.Candidatos.Where(x => x.CandidatoId == candidatoId).FirstOrDefaultAsync().Result;
                if (empleadoToDelete != null)
                {
                    _db.Candidatos.Remove(empleadoToDelete);
                    await _db.SaveChangesAsync();
                    response.Data = true;
                    response.IsSuccess = true;
                }
                else
                {
                    response.Message = $"No existe el empleado especificado por parametro";
                }
                return response;
            }
            catch (Exception ex)
            {
                response.Data = false;
                response.Message = $"Error al ejecutar DeleteCandidato con msj :  {ex.Message}";
                return response;
            }
        }     
        public async Task<Response<Candidato>> GetCandidatoById(int candidatoId)
        {
            var response = new Response<Candidato>();
            try
            {
                var empleado = await _db.Candidatos.Where(c => c.CandidatoId == candidatoId).Include(x => x.Empleos).FirstOrDefaultAsync();
              //  var empleado = await _db.Candidatos.Where(c => c.CandidatoId == candidatoId).Include(x => x.Empleos).FirstOrDefaultAsync();
                response.Data = empleado;
                response.IsSuccess = true;
                return response;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = $"Error GetCandidatoById :  {ex.Message}";
                return response;
            }
        }
        public async Task<Response<ICollection<Candidato>>> GetCandidatos()
        {
            var response = new Response<ICollection<Candidato>>();
            try
            {
                var empleados = _db.Candidatos.Include(x=> x.Empleos).OrderByDescending(x => x.CandidatoId);
                response.Data = await empleados.ToListAsync();
                response.IsSuccess = true;
                return response;
            }
            catch (Exception ex)
            {
                response.Data = null;
                response.Message = $"Error al ejecutar GetCandidatos con msj :  {ex.Message}";
                return response;
            }
        }
        public async Task<Response<bool>> UpdateCandidato(Candidato candidato)
        {
            var response = new Response<bool>();
            try
            {
                _db.Candidatos.Update(candidato);
                await _db.SaveChangesAsync();
                response.Data = true;
                response.IsSuccess = true;
                return response;
            }
            catch (Exception ex)
            {
                response.Data = false;
                response.Message = $"Error al ejecutar UpdateCandidato con msj :  {ex.Message}";
                return response;
            }
        }
    }
}
