using Core.Domain.AggregatesModel.Cinema;
using Core.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Interfaces
{
    public interface ICinemaRoomService
    {
        Task<Response<bool>> CreateCinemaRoom(CinemaRoom cinema);
        Task<Response<ICollection<CinemaRoom>>> GetCinemasRooms();
        Task<Response<bool>> UpdateCinemaRoom(CinemaRoom cinemaId);
        Task<Response<bool>> DeleteCinemaRoom(int cinemaId);
    }
}
