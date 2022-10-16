using Core.Application.Interfaces;
using Core.Domain.AggregatesModel.Cinema;
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
    public class CinemaService : ICinemaService
    {
        private readonly ApplicationDBContext _db;
        public CinemaService(ApplicationDBContext db)
        {
            _db = db;
        }
        public async Task<Response<bool>> CreateCinema(Cinema cinema)
        {
            var response = new Response<bool>();
            try
            {
                _db.Cinemas.Add(cinema);
                await _db.SaveChangesAsync();
                response.Data = true;
                response.IsSuccess = true;
                return response;
            }
            catch (Exception ex)
            {
                response.Data = false;
                response.Message = $"Error CreateCinema :  {ex.Message}";
                return response;
            }
        }
        public async Task<Response<ICollection<Cinema>>> GetCinemas()
        {
            var response = new Response<ICollection<Cinema>>();
            try
            {
                var cinemas = _db.Cinemas.Where(x => !x.Deleted).OrderByDescending(x => x.CinemaId);
                response.Data = await cinemas.ToListAsync();
                response.IsSuccess = true;
                return response;
            }
            catch (Exception ex)
            {
              response.Data = null;
              response.Message = $"Error GetCinemas :  {ex.Message}";
              return response;
            }
        }
        public async Task<Response<ICollection<Cinema>>> GetCinemasRoomsByCinema(int cinemaId)
        {
            var response = new Response<ICollection<Cinema>>();
            try
            {
                // TODO: filtrar por activos
                var cinemas = _db.Cinemas.Where(x => !x.Deleted && x.CinemaId == cinemaId).Include(x => x.Rooms)
                    .OrderByDescending(x => x.CinemaId);
                response.Data = await cinemas.ToListAsync();
                response.IsSuccess = true;
                return response;
            }
            catch (Exception ex)
            {
                response.Data = null;
                response.Message = $"Error GetCinemas :  {ex.Message}";
                return response;
            }
        }
        public async Task<Response<bool>> UpdateCinema(Cinema cinema)
        {
            var response = new Response<bool>();
            try
            {
                _db.Update(cinema);
                await _db.SaveChangesAsync();
                response.Data = true;
                response.IsSuccess = true;
                return response;
            }
            catch (Exception ex)
            {
                response.Data = false;
                response.Message = $"Error UpdateCinema :  {ex.Message}";
                return response;
            }
        }
        public async Task<Response<bool>> DeleteCinema(int cinemaId)
        {
            var response = new Response<bool>();
            try
            {
                var cinemaToDelete = _db.Cinemas.Where(x => x.CinemaId == cinemaId).FirstOrDefaultAsync().Result;
                if (cinemaToDelete != null)
                {
                    _db.Remove(cinemaToDelete);
                    await _db.SaveChangesAsync();
                    response.Data = true;
                    response.IsSuccess = true;
                }
                else {
                    response.Message = $"No existe el Cine especificado por parametro";
                }
                return response;
            }
            catch (Exception ex)
            {
                response.Data = false;
                response.Message = $"Error DeleteCinema :  {ex.Message}";
                return response;
            }
        }
    }
}
