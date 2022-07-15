using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Interface;
using Reserve_Space.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Implementation
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext context;
        private DbSet<Order> entities;
        string errorMessage = string.Empty;

        public OrderRepository(ApplicationDbContext context)
        {
            this.context = context;
            entities = context.Set<Order>();
        }
        public List<Order> GetAllOrders()
        {
            return entities
                .Include(z => z.Spaces)
                .Include(z => z.OrderedBy)
                .Include("Spaces.SelectedSpace")
                .ToListAsync().Result;
        }

        public Order GetOrderDetails(BaseEntity model)
        {
            return entities
               .Include(z => z.Spaces)
               .Include(z => z.OrderedBy)
               .Include("Spaces.SelectedSpace")
               .SingleOrDefaultAsync(z => z.Id.Equals(model.Id)).Result;
        }
    }
}
