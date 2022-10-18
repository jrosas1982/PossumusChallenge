using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Application.Dto
{
    public class CandidatoCreacionDto : CandidatoBaseDto
    {
        public ICollection<EmpleoDto> Empleos { get; set; }
    }
}
