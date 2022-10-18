using Core.Domain.AggregatesModel.RRHH;
using Core.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Interfaces
{
    public interface ICandidatoService
    {
        Task<Response<bool>> CrearCandidato(Candidato candidato);
        Task<Response<ICollection<Candidato>>> GetCandidatos();
        Task<Response<bool>> UpdateCandidato(Candidato candidato);
        Task<Response<bool>> DeleteCandidato(int candidatoId);
        Task<Response<Candidato>> GetCandidatoById(int candidatoId);
    }
}
