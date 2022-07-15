using Domain.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class Reservation : BaseEntity
    {
        public string ApplicationUserId { get; set; }
        public ApplicationUser User { get; set; }
        public virtual ICollection<ReservedSpaces> ReservedSpaces { get; set; } 
    }
}
