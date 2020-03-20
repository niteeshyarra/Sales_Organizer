using SalesOrganizer.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesOrganizer.Repositories.Interfaces
{
    public interface IOrderRepository
    {
        Task AddOrder(ViewModels.OrderViewModel order);
        Task<ViewModels.OrderViewModel> GetOrder(int id);
        Task<IEnumerable<ViewModels.OrderViewModel>> GetAllOrders();
        void DeleteOrder(int id);
        IEnumerable<ViewModels.OrderViewModel> GetOrdersByCustomer(int id);
        IEnumerable<ViewModels.OrderViewModel> GetOrdersByProduct(int id);
    }
}
