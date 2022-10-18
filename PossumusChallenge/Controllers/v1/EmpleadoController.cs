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
using Serilog;
using System;
using System.Linq;

namespace API.PossumusChallenge.Controllers.v1
{
    [ApiController]
    // [ServiceFilter(typeof(ApiKeyAuth))] // No usado para esta implementación
    [Route("v1/[Controller]")]
    public class EmpleadoController : ControllerBase
    {
        private readonly ICandidatoService _candidatoService;
        private readonly IEmpleoService _empleoService;
        private readonly IMapper _mapper;

        public EmpleadoController(ICandidatoService candidatoService, IMapper mapper , IEmpleoService empleoService)
        {
            _mapper = mapper;
            _candidatoService = candidatoService;
            _empleoService = empleoService;
        }
        [HttpGet("GetEmpleados")]
        [SwaggerOperation("Retorna todos los candidatos")]
        public async Task<IActionResult> Get()
        {
            Log.Information("Obteniendo datos de GetCandidatos");
            try
            {
                var result = await _candidatoService.GetCandidatos();
                var empleados = _mapper.Map<IEnumerable<CandidatoActualizacionDto>>(result.Data);
                return Ok(empleados);
            }
            catch (Exception ex)
            {
                Log.Warning($"Error obteniendo datos de GetCandidatos msj: {ex.Message}");
                return NotFound();
            }
        
        }
        [HttpGet("GetEmpleadoById/{Id:int}")]
        [SwaggerOperation("Retorna un candidato según el Id especificado")]
        public async Task<IActionResult> GetEmpleadoById(int Id)
        {
            Log.Information("Obteniendo datos de GetCandidatos");
            try
            {
                var result = await _candidatoService.GetCandidatoById(Id);
                if (result.Data == null)
                {
                    Log.Warning($"No hay candidato con Id: {Id}");
                    return NotFound($"No hay candidato con Id: {Id}");
                }
                var empleados = _mapper.Map<CandidatoActualizacionDto>(result.Data);
                return Ok(empleados);
            }
            catch (Exception ex)
            {
                Log.Warning($"Error obteniendo datos de GetCandidatoById msj: {ex.Message}");
                return BadRequest();
            }
    
        }
        [HttpPost("AddEmpleado")]
        [SwaggerOperation("Crea un Empleado")]
        public async Task<IActionResult> AddEmpleado([FromBody] CandidatoCreacionDto candidato)
        {
            Log.Information("Intentando CrearEmpleado");
            try
            {
                var empleado = _mapper.Map<Candidato>(candidato);
                var result = await _candidatoService.CrearCandidato(empleado);
                if (result.Data)
                    return Ok();
                return BadRequest($"Error al crear el empleado : {result.Message} ");

            }
            catch (Exception ex)
            {
                Log.Warning($"Error obteniendo datos de GetCandidatoById msj: {ex.Message}");
                return BadRequest($"Error al crear el empleado : {ex.Message} ");
            }
     
        }
        [HttpPut("UpdateEmpleado/{Id:int}")]
        [SwaggerOperation("Actualiza el empleado especificado por el parametro candidatoId")]
        public async Task<IActionResult> UpdateEmpleado([FromBody] CandidatoBaseDto candidato, int Id)
        {
            Log.Information("Intentando UpdateCandidato");
            try
            {
                var empleado = await _candidatoService.GetCandidatoById(Id);
                var empleadoMapp = _mapper.Map(candidato, empleado.Data);
                var result = await _candidatoService.UpdateCandidato(empleadoMapp);
                if (result.Data)
                    return Ok();
                return BadRequest($"Error al UpdateCandidato : {result.Message} ");
            }
            catch (Exception ex)
            {
                Log.Warning($"Error UpdateCandidato con msj: {ex.Message}");
                return BadRequest($"Error al UpdateCandidato : {ex.Message} ");
            }

        }
        [HttpPatch("UpdateParcialEmpleado/{Id:int}")]
        [SwaggerOperation("Actualiza algunos datos del empleado especificado por el parametro candidatoId")]
        public async Task<IActionResult> Patch([FromBody] JsonPatchDocument<CandidatoActualizacionDto> candidatoPatch, int Id )
        {
            Log.Information(messageTemplate: "Intentando UpdateParcialEmpleado");
            try
            {
                if (candidatoPatch == null)
                {
                    Log.Warning($"Error UpdateParcialEmpleado BadRequest");
                    return BadRequest();
                }
                Log.Debug("Intentando buscar el candidato");
                var empleadoDB = await _candidatoService.GetCandidatoById(Id);
                if (empleadoDB == null)
                {
                    Log.Warning($"no existe el candidato");
                    return NotFound();
                }
                Log.Debug("Intentando Mappeo del candidato");
                var empleadoMappDTO = _mapper.Map<CandidatoActualizacionDto>(empleadoDB.Data);
                candidatoPatch.ApplyTo(empleadoMappDTO, ModelState);
                if (!ModelState.IsValid)
                {
                    Log.Warning($"ModelState invalido");
                    return BadRequest(ModelState);
                }
                Log.Debug("Intentando segundo Mappeo del candidato");
                var candidatoMapp = _mapper.Map(empleadoMappDTO, empleadoDB.Data);
                var result = await _candidatoService.UpdateCandidato(candidatoMapp);
                if (result.Data)
                    return Ok();
                return BadRequest($"Error al actualizar el candidato : {result.Message} ");
            }
            catch (Exception ex)
            {
                Log.Warning($"Error UpdateParcialEmpleado con msj: {ex.Message} ");
                return BadRequest($"Error al actualizar el candidato : {ex.Message} ");
            }

        }
        [HttpDelete("DeleteEmpleado/{Id:int}")]
        [SwaggerOperation("Elimina el empleado especificado por el parametro Id")]
        public async Task<IActionResult> DeleteEmpleado(int Id)
        {
            Log.Information(messageTemplate: "Intentando DeleteCandidato");
            try
            {
                var result = await _candidatoService.DeleteCandidato(Id);
                if (result.Data)
                    return Ok();
                return BadRequest($"Error al eliminar el empleado : {result.Message} ");
            }
            catch (Exception ex)
            {
                Log.Warning($"Error DeleteCandidato con msj: {ex.Message} ");
                return BadRequest($"Error al eliminar el empleado : {ex.Message} ");
            }
          }
        //------------------------------------------------------------------------------------------
        //Se agrega enpoint en el mismo controlador de Empleado solo por practicidad    
        [HttpPost("AddEmpleoAlEmpleado/{empleadoId:int}")]
        [SwaggerOperation("Agrega empleo/s al empleado especificado por parametro empleadoId")]
        public async Task<IActionResult> AddEmpleoAlEmpleado([FromBody] IEnumerable<EmpleoDto> empleoDto , int empleadoId)
        {
            Log.Information(messageTemplate: "Intentando AgregarEmpleoAlEmpleado");
            try
            {
                var empleado = _mapper.Map<IEnumerable<Empleo>>(empleoDto);
                var result = await _empleoService.CrearEmpleosAlCandidato(empleado, empleadoId);
                if (result.Data)
                    return Ok();
                return BadRequest($"Error al Agregar el empleo al empleado con msj : {result.Message} ");
            }
            catch (Exception ex)
            {
                Log.Warning($"Error AgregarEmpleoAlEmpleado con msj: {ex.Message} ");
                return BadRequest($"Error al AgregarEmpleoAlEmpleado el empleado : {ex.Message} ");
            }
        }
       
        [HttpDelete("DeleteEmpleoAlEmpleado/{empleoId:int}/{empleadoId:int}")]
        [SwaggerOperation("Agrega empleo/s al empleado especificado por parametro empleadoId")]
        public async Task<IActionResult> DeleteEmpleoAlEmpleado(int empleoId , int empleadoId)
        {
            Log.Information(messageTemplate: "Intentando DeleteEmpleoAlEmpleado");
            try
            {
                var empleado = await _candidatoService.GetCandidatoById(empleadoId);
                if (empleado.Data.Empleos.Where(x => x.Id == empleoId).FirstOrDefault() == null) {
                    Log.Warning(messageTemplate: "No coinciden el empleo con el Empleado");
                    return NotFound($"No coinciden el empleo con el Empleado");
                }
                var result = await _empleoService.EliminarEmpleosDelCandidato(empleoId);
                if (result.Data)
                    return Ok();
                return BadRequest($"Error al eliminar el empleo al empleado con msj : {result.Message} ");
            }
            catch (Exception ex)
            {
                Log.Warning($"Error DeleteEmpleoAlEmpleado con msj: {ex.Message} ");
                return BadRequest($"Error al eliminar el empleo : {ex.Message} ");
            }
        }

    }
}
