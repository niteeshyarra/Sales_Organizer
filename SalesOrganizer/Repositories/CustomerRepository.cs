using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SalesOrganizer.DataContexts;
using SalesOrganizer.DataModels;
using SalesOrganizer.Repositories.Interfaces;
using SalesOrganizer.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SalesOrganizer.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly CustomerContext _customerContext;
        private readonly IMapper _mapper;

        public CustomerRepository(CustomerContext customerContext, IMapper mapper)
        {
            _customerContext = customerContext;
            _mapper = mapper;
        }



        public async Task AddCustomer(CustomerViewModel customer)
        {

            if (customer == null)
            {
                throw new ArgumentException("Customer Can't be null");
            }
            
            var mappedCustomer = _mapper.Map<CustomerViewModel, Customer>(customer);

            await _customerContext.Customers.AddAsync(mappedCustomer);
            _customerContext.SaveChanges();
        }

        public void DeleteCustomer(int id)
        {
            var customer = GetCustomer(id);
            if (customer == null)
            {
                throw new KeyNotFoundException();
            }

            var mappedCustomer = _mapper.Map<CustomerViewModel, Customer>(customer.Result);

            _customerContext.Customers.Remove(mappedCustomer);
            _customerContext.SaveChanges();
        }

        public async Task<IEnumerable<CustomerViewModel>> GetAllCustomers()
        {
            var customers = await _customerContext.Customers.ToListAsync();

            List<ViewModels.CustomerViewModel> customersDTO = new List<CustomerViewModel>();

            foreach (var customer in customers)
            {
                customersDTO.Add(_mapper.Map<Customer, CustomerViewModel>(customer));
            }

            return customersDTO;
        }

        public async Task<CustomerViewModel> GetCustomer(int id)
        {
            var foundCustomer = _customerContext.Customers.FindAsync(id);

            if (foundCustomer == null)
            {
                throw new ArgumentException("No customer with this ID Exists");
            }

            var customer = await _customerContext.Customers.FirstOrDefaultAsync(c => c.CustomerId == id);

            var customerDTO = _mapper.Map<Customer, CustomerViewModel>(customer);

            return customerDTO;
        }

        public void UpdateCustomer(CustomerViewModel customer)
        {
            var mappedCustomer = _mapper.Map<CustomerViewModel, Customer>(customer);

            var foundCustomer = _customerContext.Customers.FindAsync(mappedCustomer.CustomerId);

            if (foundCustomer == null)
            {
                throw new ArgumentException("No record Exists to update");
            }

            _customerContext.Customers.Update(mappedCustomer);
            _customerContext.SaveChanges();
        }

        public async Task<bool> FindCustomer(int id)
        {
            var customer = await _customerContext.Customers.FindAsync(id);
            return customer == null ? false : true;
        }
    }
}
