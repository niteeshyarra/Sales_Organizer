using Microsoft.EntityFrameworkCore;
using SalesOrganizer.DataContexts;
using SalesOrganizer.DataModels;
using SalesOrganizer.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesOrganizer.Repositories
{    
    public class OrderRepository : IOrderRepository
    {
        private readonly CustomerContext _customerContext;
        public OrderRepository(CustomerContext customerContext)
        {
            _customerContext = customerContext;
        }
        public void AddOrder(Order order)
        {
            _customerContext.Orders.Add(order);
            _customerContext.SaveChanges();
        }

        public Order GetOrder(int id)
        {
            return _customerContext.Orders.FirstOrDefault(o => o.OrderId == id);
        }

        public void DeleteOrder(int id)
        {
            var order = GetOrder(id);
            _customerContext.Orders.Remove(order);
            _customerContext.SaveChanges();
        }

        public IEnumerable<Order> GetOrdersByCustomer(int id)
        {
            return _customerContext.Orders.Where(c => c.CustomerId == id);
        }

        public IEnumerable<Order> GetOrdersByProduct(int id)
        {
            return _customerContext.ProductOrders.Where(p => p.ProductId == id)
                                                    .Select(p => p.Order);
        }
    }
}
