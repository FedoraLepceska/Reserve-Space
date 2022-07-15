using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Models
{
    public class SpacesInOrder : BaseEntity
    {
        [ForeignKey("SpaceId")]
        public Guid SpaceId { get; set; }
        [ForeignKey("OrderId")]
        public Guid OrderId { get; set; }
        public Order Order { get; set; }
        public Space SelectedSpace { get; set; }
        public int Quantity { get; set; }

        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
    }
}
