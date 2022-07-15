using Domain.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class Order : BaseEntity
    {
        public string userId { get; set; }
        public ApplicationUser OrderedBy { get; set; }
        public List<SpacesInOrder> Spaces { get; set; }
    }
}
