using SalesOrganizer.RequestModels;
using SalesOrganizer.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesOrganizer.Repositories.Interfaces
{
    public interface ICustomerRepository
    {
        Task AddCustomer(CustomerRequestModel customer);
        Task<CustomerResponseModel> GetCustomer(int id);
        void UpdateCustomer(CustomerRequestModel customer);
        void DeleteCustomer(int id);
        Task<IEnumerable<CustomerResponseModel>> GetAllCustomers();
        Task<bool> FindCustomer(int id);
    }
}
