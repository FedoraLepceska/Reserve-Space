using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Models
{
    public class ReservedSpaces : BaseEntity
    {
        public Guid SpaceId { get; set; }
        public Guid ReservationId { get; set; }
        [ForeignKey("SpaceId")]
        public Space Space { get; set; }
        [ForeignKey("ReservationId")]
        public Reservation Reservation { get; set; }
        public int Quantity { get; set; }
    }
}
