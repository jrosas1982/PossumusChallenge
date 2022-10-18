using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Application.Dto
{
    public class CandidatoBaseDto
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public DateTime FecNacimiento { get; set; }
        public string Email { get; set; }
        public string Teléfono { get; set; }
    }
}
