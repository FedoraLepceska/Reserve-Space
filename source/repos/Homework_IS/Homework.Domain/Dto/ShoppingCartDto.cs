using Homework.Domain.DomainModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Homework.Domain.Dto
{
    public class ShoppingCartDto
    {
        public List<TicketInShoppingCart> Tickets { get; set; }
        public double TotalPrice { get; set; }
    }
}
