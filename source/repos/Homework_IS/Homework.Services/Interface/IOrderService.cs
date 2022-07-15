using Homework.Domain.DomainModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Homework.Services.Interface
{
    public interface IOrderService
    {
        List<Order> getAllOrders();
        Order getOrderDetails(BaseEntity model);
    }
}
