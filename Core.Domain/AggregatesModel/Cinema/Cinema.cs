using Core.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Core.Domain.AggregatesModel.Cinema
{
    public class Cinema : EntityBase
    {
        [Key]
        public int CinemaId { get; set; }
        [Column(TypeName = "varchar(50)")]
        public string Name { get; set; }
        [Column(TypeName = "varchar(50)")]
        public string Address { get; set; }
        public ICollection<CinemaRoom> Rooms { get; set; }
    }
}
