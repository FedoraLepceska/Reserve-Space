using Homework.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Homework.Domain.DomainModels
{
    public class Order : BaseEntity
    {
        public string UserId { get; set; }
        public CinemaApplicationUser User { get; set; }
        public IEnumerable<TicketInOrder> TicketInOrders { get; set; }
    }
}
