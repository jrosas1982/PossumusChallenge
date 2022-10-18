using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Application.Dto
{
    public class CandidatoActualizacionDto : CandidatoBaseDto
    {
        public int CandidatoId { get; set; }
        public ICollection<EmpleoActualizacionDto> Empleos { get; set; }
    }
}
