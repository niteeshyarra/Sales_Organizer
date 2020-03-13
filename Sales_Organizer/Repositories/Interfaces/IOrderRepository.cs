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
        Order GetOrder(int id);
        void DeleteOrder(int id);
        IEnumerable<Order> GetOrdersByCustomer(int id);
        IEnumerable<Order> GetOrdersByProduct(int id);
    }
}
