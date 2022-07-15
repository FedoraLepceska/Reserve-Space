using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Dto
{
    public class ReserveSpaceDto
    {
        public Space SelectedSpace { get; set; }
        public Guid SpaceId { get; set; }
        public int Quantity { get; set; }

        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
    }
}
