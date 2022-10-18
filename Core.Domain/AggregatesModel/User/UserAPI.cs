using Core.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Core.Domain.AggregatesModel.User
{
    //No usado en esta implementación
    public class UserAPI : EntityBase
    {
        public UserAPI()
        {
            Active = true;
            ApiKey = Guid.NewGuid().ToString("N");
        }

        [Key]
        public int Id { get; set; }
        [Column(TypeName = "varchar(250)")]
        public string ApiKey { get; set; }
        [Column(TypeName = "varchar(50)")]
        public string Name { get; set; }
        public Boolean Active { get; set; }
    }
}
