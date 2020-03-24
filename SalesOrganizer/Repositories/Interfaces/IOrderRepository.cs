using SalesOrganizer.DataModels;
using SalesOrganizer.RequestModels;
using SalesOrganizer.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesOrganizer.Repositories.Interfaces
{
    public interface IOrderRepository
    {
        Task AddOrder(OrderRequestModel order);
        Task<OrderResponseModel> GetOrder(int id);
        Task<IEnumerable<OrderResponseModel>> GetAllOrders();
        void DeleteOrder(int id);
        IEnumerable<OrderResponseModel> GetOrdersByCustomer(int id);
        IEnumerable<OrderResponseModel> GetOrdersByProduct(int id);
        Task<bool> FindOrder(int id);
    }
}
