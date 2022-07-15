using Domain.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Password { get; set; }

        public string ConfirmPassword { get; set; }

        public virtual Reservation Reservation { get; set; }

        public virtual ICollection<Order> Orders { get; set; }

        public Role Role { get; set; }

        public static implicit operator ApplicationUser(string v)
        {
            throw new NotImplementedException();
        }
    }
}
