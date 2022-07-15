using System;
using System.Collections.Generic;
using System.Text;
using Homework.Domain.DomainModels;
using Microsoft.AspNetCore.Identity;

namespace Homework.Domain.Identity
{
    public class CinemaApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }

        public string ConfirmPassword { get; set; }
        public string Role { get; set; }
        public string Address { get; set; }
        public virtual ShoppingCart UserCart { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
