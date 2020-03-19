using SalesOrganizer.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesOrganizer.Repositories.Interfaces
{
    public interface IOrderRepository
    {
        Task AddOrder(ViewModels.Order order);
        Task<ViewModels.Order> GetOrder(int id);
        Task<IEnumerable<ViewModels.Order>> GetAllOrders();
        void DeleteOrder(int id);
        IEnumerable<ViewModels.Order> GetOrdersByCustomer(int id);
        IEnumerable<ViewModels.Order> GetOrdersByProduct(int id);
    }
}
