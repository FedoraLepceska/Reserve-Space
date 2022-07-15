using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Interface
{
    public interface IOrderService
    {
        List<Order> GetAllOrders();
        Order GetOrderDetails(BaseEntity model);
    }
}
