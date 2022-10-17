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
    public class CinemaController : ControllerBase
    {
        private readonly ICinemaService _cinemaService;
        private readonly IMapper _mapper;

        public CinemaController(ICinemaService cinemaService , IMapper mapper)
        {
            _cinemaService = cinemaService;
            _mapper = mapper;
        }

        [HttpGet("GetCinemas")]
        [SwaggerOperation("Retorna todos los cines")]
        public async Task<IEnumerable<CinemasDto>> Get()
        {
            var result = await _cinemaService.GetCinemas();
            var cines = _mapper.Map<IEnumerable<CinemasDto>>(result.Data);
            return (IEnumerable<CinemasDto>)cines;
        } 
        [HttpGet("{cinemaId:int}")]
        [SwaggerOperation("Retorna todos las salas del cine especificado por parametro cinemaId")]
        public async Task<IEnumerable<Cinema>> GetCinemasRoomsByCinema(int cinemaId)
        {
            var result = await _cinemaService.GetCinemasRoomsByCinema(cinemaId);
            return result.Data;
        }
        [HttpPost("CreateCinema")]
        [SwaggerOperation("Crea un cine")]
        public async Task<IActionResult> CreateCinema([FromBody] CinemaDto cinema) {
            var cine = _mapper.Map<Cinema>(cinema);
            var result = await _cinemaService.CreateCinema(cine);
            if (result.Data)
                return Ok();
            return BadRequest($"Error al crear el cine : {result.Message} ");
        }
        [HttpPut("{cinemaId:int}")]
        [SwaggerOperation("Actualiza el cine especificado por el parametro cinemaId")]
        public async Task<IActionResult> UpdateCinema([FromBody] CinemaDto cinema, int cinemaId)
        {
            var cine = _mapper.Map<Cinema>(cinema);
            cine.CinemaId = cinemaId;
            var result = await _cinemaService.UpdateCinema(cine);
            if (result.Data)
                return Ok();
            return BadRequest($"Error al actualizar el cine : {result.Message} ");
        }
        [HttpDelete("{cinemaId:int}")]
        [SwaggerOperation("Elimina el cine especificado por el parametro cinemaId")]
        public async Task<IActionResult> DeleteCinema(int cinemaId)
        {
            var result = await _cinemaService.DeleteCinema(cinemaId);
            if (result.Data)
                return Ok();
            return BadRequest($"Error al eliminar el cine : {result.Message} " );
        }
    }
}
