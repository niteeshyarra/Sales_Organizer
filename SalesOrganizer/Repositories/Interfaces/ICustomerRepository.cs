using SalesOrganizer.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesOrganizer.Repositories.Interfaces
{
    public interface ICustomerRepository
    {
        Task AddCustomer(CustomerViewModel customer);
        Task<CustomerViewModel> GetCustomer(int id);
        void UpdateCustomer(CustomerViewModel customer);
        void DeleteCustomer(int id);
        Task<IEnumerable<CustomerViewModel>> GetAllCustomers();
        Task<bool> FindCustomer(int id);
    }
}
