using Core.Domain.AggregatesModel.RRHH;
using Core.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Interfaces
{
    public interface IEmpleoService
    {
        Task<Response<bool>> CrearEmpleosAlCandidato(IEnumerable<Empleo> empleos , int empleadoId);
        Task<Response<bool>> ActualizarEmpleosDelCandidato(IEnumerable<Empleo> empleos);
    }
}
