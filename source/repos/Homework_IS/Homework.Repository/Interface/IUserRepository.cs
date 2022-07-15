using Homework.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Homework.Repository.Interface
{
    public interface IUserRepository
    {
        IEnumerable<CinemaApplicationUser> GetAll();
        CinemaApplicationUser Get(string id);
        void Insert(CinemaApplicationUser entity);
        void Update(CinemaApplicationUser entity);
        void Delete(CinemaApplicationUser entity);
    }
}
