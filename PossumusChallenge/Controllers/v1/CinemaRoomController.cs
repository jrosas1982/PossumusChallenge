using API.PossumusChallenge.Filters;
using AutoMapper;
using Core.Application.Dto;
using Core.Application.Interfaces;
using Core.Domain.AggregatesModel.Cinema;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.PossumusChallenge.Controllers.v1
{
    [ApiController]
    [ServiceFilter(typeof(ApiKeyAuth))]
    [Route("v1/[Controller]")]
    public class CinemaRoomController : ControllerBase
    {
        private readonly ICinemaRoomService _cinemaRoomService;
        private readonly IMapper _mapper;

        public CinemaRoomController(ICinemaRoomService cinemaRService, IMapper mapper)
        {
            _cinemaRoomService = cinemaRService;
            _mapper = mapper;
        }

        [HttpGet]
        [SwaggerOperation("Retorna todos los cines")]
        public async Task<IEnumerable<CinemaRoom>> Get()
        {
            var result = await _cinemaRoomService.GetCinemasRooms();
            return result.Data;
        }
        [HttpPost]
        [SwaggerOperation("Crea una sala para el cine especificado según su cinemaId ")]
        public async Task<IActionResult> CreateCinema([FromBody] CinemaRoomDto cinema)
        {
            var cine = _mapper.Map<CinemaRoom>(cinema);
            var result = await _cinemaRoomService.CreateCinemaRoom(cine);
            if (result.Data)
                return Ok();
            return BadRequest($"Error al crear el cine : {result.Message} ");
        }
        [HttpPut("{roomId:int}")]
        [SwaggerOperation("Actualiza la sala del cine especificado por el parametro roomId")]
        public async Task<IActionResult> UpdateCinema([FromBody] CinemaRoomDto cinema, int roomId)
        {
            var cine = _mapper.Map<CinemaRoom>(cinema);
            cine.Id = roomId;
            var result = await _cinemaRoomService.UpdateCinemaRoom(cine);
            if (result.Data)
                return Ok();
            return BadRequest($"Error al actualizar la sale del cine : {result.Message} ");
        }
        [HttpDelete("{roomId:int}")]
        [SwaggerOperation("Elimina la sala del cine especificado por el parametro roomId")]
        public async Task<IActionResult> DeleteCinema(int roomId)
        {
            var result = await _cinemaRoomService.DeleteCinemaRoom(roomId);
            if (result.Data)
                return Ok();
            return BadRequest($"Error al eliminar la sala del cine : {result.Message} ");
        }
    }
}
