using Core.Domain.AggregatesModel.Cinema;
using Core.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Interfaces
{
    public interface ICinemaService
    {
        Task<Response<bool>> CreateCinema(Cinema cinema);
        Task<Response<ICollection<Cinema>>> GetCinemas();
        Task<Response<ICollection<Cinema>>> GetCinemasRoomsByCinema(int cinemaId);
        Task<Response<bool>> UpdateCinema(Cinema cinemaId);
        Task<Response<bool>> DeleteCinema(int cinemaId);
    }
}
