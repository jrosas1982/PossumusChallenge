using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Core.Domain.AggregatesModel.RRHH
{
    public class Candidato
    {
        [Key]
        public int CandidatoId { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public DateTime FecNacimiento { get; set; }
        public string Email { get; set; }
        public string Teléfono { get; set; }
        public ICollection<Empleo> Empleos { get; set; }

        public object Count()
        {
            throw new NotImplementedException();
        }
    }
}
