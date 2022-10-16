using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Application.Dto
{
    public class CinemaRoomDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Active { get; set; }
        public int CinemaId { get; set; }

    }
}
