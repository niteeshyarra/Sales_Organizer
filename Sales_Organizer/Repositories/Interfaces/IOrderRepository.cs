using Sales_Organizer.Data_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sales_Organizer.Repositories.Interfaces
{
    public interface IOrderRepository
    {
        void AddOrder(Order order);
        void DeleteOrder(Order order);
        IEnumerable<Order> GetOrdersByCustomer(int Id);
        IEnumerable<Order> GetOrdersByProduct(int Id);
    }
}
