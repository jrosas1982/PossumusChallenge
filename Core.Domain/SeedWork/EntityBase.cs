using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Core.Domain.SeedWork
{
    //No usado en esta implementación
    public class EntityBase
    {
        public EntityBase()
        {
            CreationDate = DateTime.Now;
        }
        public DateTime CreationDate { get; set; }
        public DateTime? ModificationDate { get; set; }
        [Column(TypeName = "varchar(50)")]
        public string CreatedBy { get; set; }
        [Column(TypeName = "varchar(50)")]
        public string ModifiedBy { get; set; }
        public bool Deleted { get; set; }
    }
}
