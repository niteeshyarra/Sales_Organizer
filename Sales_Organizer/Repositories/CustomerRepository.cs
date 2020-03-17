using Microsoft.EntityFrameworkCore;
using Sales_Organizer.Data_Contexts;
using Sales_Organizer.Data_Models;
using Sales_Organizer.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sales_Organizer.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly CustomerContext _customerContext;
        public CustomerRepository(CustomerContext customerContext)
        {
            _customerContext = customerContext;
        }
        public async Task AddCustomer(Customer customer)
        {
            if(customer == null)
            {
                throw new ArgumentException("Customer Can't be null");
            }
             await _customerContext.Customers.AddAsync(customer);
            _customerContext.SaveChanges();
        }

        public void DeleteCustomer(int id)
        {
            var customer = GetCustomer(id);
            _customerContext.Customers.Remove(customer.Result);
            _customerContext.SaveChanges();
;        }

        public async Task<IEnumerable<Customer>> GetAllCustomers()
        {
            return await _customerContext.Customers.ToListAsync<Customer>();
        }

        public async Task<Customer> GetCustomer(int id)
        {
            return await _customerContext.Customers.FirstOrDefaultAsync(c => c.CustomerId == id);
        }

        public void UpdateCustomer(Customer customer)
        {
            _customerContext.Customers.Update(customer);
            _customerContext.SaveChanges();
        }
    }
}
