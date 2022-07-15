using Homework.Domain.DomainModels;
using Homework.Repository.Interface;
using Homework.Services.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Homework.Services.Implementation
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            this._orderRepository = orderRepository;
        }
        public List<Order> getAllOrders()
        {
            return this._orderRepository.GetAllOrders();
        }

        public Order getOrderDetails(BaseEntity model)
        {
            return this._orderRepository.GetOrderDetails(model);
        }
    }
}
