using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Core.Domain.AggregatesModel.RRHH
{
    public class Empleo
    {
        [Key]
        public int Id { get; set; }
        public string NombreEmpresa { get; set; }
        public string Periodo { get; set; }
        public int CandidatoId { get; set; }
        public Candidato Candidato { get; set; }
    }
}
