using System;
using System.Collections.Generic;
using System.Text;

namespace Homework.Domain.DomainModels
{
    public class Ticket : BaseEntity
    {
        public string Movie { get; set; }
        public DateTime Date { get; set; }
        public int TicketPrice { get; set; }
        public virtual ICollection<TicketInShoppingCart> TicketInShoppingCarts { get; set; }
        public IEnumerable<TicketInOrder> TicketInOrders { get; set; }
    }
}
