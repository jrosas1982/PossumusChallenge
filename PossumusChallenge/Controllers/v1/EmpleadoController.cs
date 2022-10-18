using AutoMapper;
using Core.Application.Dto;
using Core.Application.Interfaces;
using Core.Domain.AggregatesModel.RRHH;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.PossumusChallenge.Controllers.v1
{
    [ApiController]
    // [ServiceFilter(typeof(ApiKeyAuth))]
    [Route("v1/[Controller]")]
    public class EmpleadoController : ControllerBase
    {
        private readonly ICandidatoService _candidatoService;
        private readonly IEmpleoService _empleoService;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public EmpleadoController(ICandidatoService candidatoService, IMapper mapper , IEmpleoService empleoService)
        {
            _mapper = mapper;
            _candidatoService = candidatoService;
            _empleoService = empleoService;
        }
        [HttpGet("GetCandidatos")]
        [SwaggerOperation("Retorna todos los candidatos")]
        public async Task<IEnumerable<CandidatoDto>> Get()
        {
            var result = await _candidatoService.GetCandidatos();
            var empleados = _mapper.Map<IEnumerable<CandidatoDto>>(result.Data);
            return empleados;
        }
        [HttpGet("GetCandidatoById/{Id:int}")]
        [SwaggerOperation("Retorna un candidato según el Id especificado")]
        public async Task<IActionResult> GetCandidatoById(int Id)
        {
            var result = await _candidatoService.GetCandidatoById(Id);
            if (result.Data == null) {
                return NotFound($"No hay candidato con Id: {Id}");
            }
            var empleados = _mapper.Map<CandidatoDto>(result.Data);
            return Ok(empleados);
        }
        [HttpPost("CrearEmpleado")]
        [SwaggerOperation("Crea un Empleado")]
        public async Task<IActionResult> CrearEmpleado([FromBody] CandidatoCreacionDto candidato)
        {
            var empleado = _mapper.Map<Candidato>(candidato);
            var result = await _candidatoService.CrearCandidato(empleado);
            if (result.Data)
                return Ok();
            return BadRequest($"Error al crear el empleado : {result.Message} ");
        }
        [HttpPut("UpdateCandidato/{Id:int}")]
        [SwaggerOperation("Actualiza el empleado especificado por el parametro candidatoId")]
        public async Task<IActionResult> UpdateCandidato([FromBody] CandidatoBaseDto candidato, int Id)
        {
            var empleado = await _candidatoService.GetCandidatoById(Id);
            var empleadoMapp = _mapper.Map(candidato , empleado.Data);
            var result = await _candidatoService.UpdateCandidato(empleadoMapp);
            if (result.Data)
                return Ok();
            return BadRequest($"Error al actualizar el cine : {result.Message} ");
        }
        [HttpPatch("UpdateParcialEmpleado/{Id:int}")]
        [SwaggerOperation("Actualiza algunos datos del empleado especificado por el parametro candidatoId")]
        public async Task<IActionResult> Patch([FromBody] JsonPatchDocument<CandidatoActualizacionDto> candidatoPatch, int Id )
        {
            if (candidatoPatch == null) {
                return BadRequest();
            }
            var empleadoDB = await _candidatoService.GetCandidatoById(Id);
            if (empleadoDB == null) { 
                return NotFound();
            }

            var empleadoMappDTO = _mapper.Map<CandidatoActualizacionDto>(empleadoDB.Data);
            candidatoPatch.ApplyTo(empleadoMappDTO, ModelState);
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }
            var candidatoMapp = _mapper.Map(empleadoMappDTO, empleadoDB.Data);
            var result = await _candidatoService.UpdateCandidato(candidatoMapp);
            if (result.Data)
                return Ok();
            return BadRequest($"Error al actualizar el cine : {result.Message} ");
        }
        [HttpDelete("DeleteCandidato/{Id:int}")]
        [SwaggerOperation("Elimina el empleado especificado por el parametro Id")]
        public async Task<IActionResult> DeleteCandidato(int Id)
        {
            var result = await _candidatoService.DeleteCandidato(Id);
            if (result.Data)
                return Ok();
            return BadRequest($"Error al eliminar el empleado : {result.Message} ");
        }

        //------------------------------------------------------------------------------------------

        [HttpPost("AgregarEmpleoAlEmpleado/{empleadoId:int}")]
        [SwaggerOperation("Agrega empleo/s al empleado especificado por parametro empleadoId")]
        public async Task<IActionResult> AgregarEmpleoAlEmpleado([FromBody] IEnumerable<EmpleoDto> empleoDto , int empleadoId)
        {
            var empleado = _mapper.Map<IEnumerable<Empleo>>(empleoDto);
            var result = await _empleoService.CrearEmpleosAlCandidato(empleado , empleadoId);
            if (result.Data)
                return Ok();
            return BadRequest($"Error al Agregar el empleo al empleado con msj : {result.Message} ");
        }
    
    }
}
