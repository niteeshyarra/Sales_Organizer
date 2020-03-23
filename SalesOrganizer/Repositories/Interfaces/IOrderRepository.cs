using SalesOrganizer.DataModels;
using SalesOrganizer.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesOrganizer.Repositories.Interfaces
{
    public interface IOrderRepository
    {
        Task AddOrder(OrderViewModel order);
        Task<OrderViewModel> GetOrder(int id);
        Task<IEnumerable<OrderViewModel>> GetAllOrders();
        void DeleteOrder(int id);
        IEnumerable<OrderViewModel> GetOrdersByCustomer(int id);
        IEnumerable<OrderViewModel> GetOrdersByProduct(int id);
        Task<bool> FindOrder(int id);
    }
}
