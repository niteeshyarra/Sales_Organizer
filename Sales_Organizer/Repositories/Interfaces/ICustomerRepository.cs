using Sales_Organizer.Data_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sales_Organizer.Repositories.Interfaces
{
    public interface ICustomerRepository
    {
        Task AddCustomer(Customer customer);
        Task<Customer> GetCustomer(int id);
        void UpdateCustomer(Customer customer);
        void DeleteCustomer(int id);
        Task<IEnumerable<Customer>> GetAllCustomers();
    }
}
