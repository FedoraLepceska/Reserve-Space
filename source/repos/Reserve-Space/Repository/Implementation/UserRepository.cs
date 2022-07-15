using Domain.Identity;
using Microsoft.EntityFrameworkCore;
using Repository.Interface;
using Reserve_Space.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repository.Implementation
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext context;
        private DbSet<ApplicationUser> entities;
        string errorMessage = string.Empty;

        public UserRepository(ApplicationDbContext context)
        {
            this.context = context;
            entities = context.Set<ApplicationUser>();
        }

        public void Delete(ApplicationUser entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Remove(entity);
            context.SaveChanges();
        }

        public ApplicationUser Get(string id)
        {
            return entities
                .Include(z => z.Reservation)
                .Include("Reservation.ReservedSpaces")
                .Include("Reservation.ReservedSpaces.Space")
                .SingleOrDefault(s => s.Id.Equals(id));
        }

        public IEnumerable<ApplicationUser> GetAll()
        {
            return entities.AsEnumerable();
        }

        public void Insert(ApplicationUser entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Add(entity);
            context.SaveChanges();
        }

        public void Update(ApplicationUser entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Update(entity);
            context.SaveChanges();
        }
    }
}
