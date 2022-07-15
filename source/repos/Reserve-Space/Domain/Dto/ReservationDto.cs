using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Dto
{
    public class ReservationDto
    {
        public List<ReservedSpaces> ReservedSpaces { get; set; }
        public float TotalPrice { get; set; }
    }
}
