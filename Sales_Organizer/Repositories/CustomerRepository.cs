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
        public void AddCustomer(Customer customer)
        {
            if(customer == null)
            {
                throw new ArgumentException("Customer Can't be null");
            }
            _customerContext.Customers.Add(customer);
            _customerContext.SaveChanges();
        }

        public void DeleteCustomer(int id)
        {
            var customer = GetCustomer(id);
            _customerContext.Customers.Remove(customer);
            _customerContext.SaveChanges();
;        }

        public IEnumerable<Customer> GetAllCustomers()
        {
            return _customerContext.Customers.ToList<Customer>();
        }

        public Customer GetCustomer(int id)
        {
            return _customerContext.Customers.FirstOrDefault(c => c.CustomerId == id);
        }

        public void UpdateCustomer(Customer customer)
        {
            _customerContext.Customers.Update(customer);
            _customerContext.SaveChanges();
        }
    }
}
