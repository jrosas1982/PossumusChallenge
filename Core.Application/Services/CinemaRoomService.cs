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
    public class CinemaRoomService : ICinemaRoomService
    {
        private readonly ApplicationDBContext _db;
        public CinemaRoomService(ApplicationDBContext db)
        {
            _db = db;
        }
        public async Task<Response<bool>> CreateCinemaRoom(CinemaRoom room)
        {
            var response = new Response<bool>();
            try
            {
                _db.CinemaRooms.Add(room);
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
        public async Task<Response<bool>> DeleteCinemaRoom(int Id)
        {
            var response = new Response<bool>();
            try
            {
                var cinemaToDelete = _db.CinemaRooms.Where(x => x.Id == Id).FirstOrDefaultAsync().Result;
                if (cinemaToDelete != null)
                {
                    _db.Remove(cinemaToDelete);
                    await _db.SaveChangesAsync();
                    response.Data = true;
                    response.IsSuccess = true;
                }
                else
                {
                    response.Message = $"No existe la sala del Cine especificado por parametro";
                }
                return response;
            }
            catch (Exception ex)
            {
                response.Data = false;
                response.Message = $"Error DeleteCinemaRoom :  {ex.Message}";
                return response;
            }
        }
        public async Task<Response<bool>> UpdateCinemaRoom(CinemaRoom room)
        {
            var response = new Response<bool>();
            try
            {
                _db.Update(room);
                await _db.SaveChangesAsync();
                response.Data = true;
                response.IsSuccess = true;
                return response;
            }
            catch (Exception ex)
            {
                response.Data = false;
                response.Message = $"Error UpdateCinemaRoom :  {ex.Message}";
                return response;
            }
        }
        public async Task<Response<ICollection<CinemaRoom>>> GetCinemasRooms()
        {
            var response = new Response<ICollection<CinemaRoom>>();
            try
            {
                var rooms = _db.CinemaRooms.Where(x => !x.Deleted).OrderByDescending(x => x.Id);
                response.Data = await rooms.ToListAsync();
                response.IsSuccess = true;
                return response;
            }
            catch (Exception ex)
            {
                response.Data = null;
                response.Message = $"Error GetCinemasRooms :  {ex.Message}";
                return response;
            }
        }
    }
}
