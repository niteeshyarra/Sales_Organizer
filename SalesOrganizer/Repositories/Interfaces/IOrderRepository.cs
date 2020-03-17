using SalesOrganizer.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesOrganizer.Repositories.Interfaces
{
    public interface IOrderRepository
    {
        void AddOrder(Order order);
        Order GetOrder(int id);
        void DeleteOrder(int id);
        IEnumerable<Order> GetOrdersByCustomer(int id);
        IEnumerable<Order> GetOrdersByProduct(int id);
    }
}
