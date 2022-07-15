using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Interface
{
    public interface IOrderRepository
    {
        List<Order> GetAllOrders();
        Order GetOrderDetails(BaseEntity model);
    }
}
