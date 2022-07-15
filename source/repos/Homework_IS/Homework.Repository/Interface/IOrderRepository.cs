using Homework.Domain.DomainModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Homework.Repository.Interface
{
    public interface IOrderRepository
    {
        List<Order> GetAllOrders();
        Order GetOrderDetails(BaseEntity model);
    }
}
