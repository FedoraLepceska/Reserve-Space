using Homework.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Homework.Domain.DomainModels
{
    public class ShoppingCart : BaseEntity
    {
        public string OwnerId { get; set; }
        public CinemaApplicationUser Owner { get; set; }
        public virtual ICollection<TicketInShoppingCart> TicketInShoppingCarts { get; set; }
    }
}
