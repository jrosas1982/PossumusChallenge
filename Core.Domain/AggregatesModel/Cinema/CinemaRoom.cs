using Core.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Core.Domain.AggregatesModel.Cinema
{
    public class CinemaRoom : EntityBase
    {
        public CinemaRoom()
        {
            Active = true;
        }
        [Key]
        public int Id { get; set; }
        [Column(TypeName = "varchar(50)")]
        public string Name { get; set; }
        [Column(TypeName = "varchar(50)")]
        public string Description { get; set; }
        public bool Active { get; set; }
        public int CinemaId { get; set; }
        public Cinema Cinema { get; set; }

    }
}
