using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SalesOrganizer.DataContexts;
using SalesOrganizer.DataModels;
using SalesOrganizer.Repositories.Interfaces;
using SalesOrganizer.RequestModels;
using SalesOrganizer.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task AddCustomer(CustomerRequestModel customer)
        {

            if (customer == null)
            {
                throw new ArgumentException("Customer Can't be null");
            }
            
            var mappedCustomer = _mapper.Map<CustomerRequestModel, Customer>(customer);

            await _customerContext.Customers.AddAsync(mappedCustomer);
            _customerContext.SaveChanges();
        }

        public async Task DeleteCustomer(int id)
        {
            var customer = await FindCustomer(id);
            if (customer == null)
            {
                throw new KeyNotFoundException();
            }
            _customerContext.Customers.Remove(customer);
            _customerContext.SaveChanges();
        }

        public async Task<IEnumerable<CustomerResponseModel>> GetAllCustomers()
        {
            var customers = await _customerContext.Customers.ToListAsync();

            List<CustomerResponseModel> customersDTO = new List<CustomerResponseModel>();

            foreach (var customer in customers)
            {
                customersDTO.Add(_mapper.Map<Customer, CustomerResponseModel>(customer));
            }

            return customersDTO;
        }

        public async Task<CustomerResponseModel> GetCustomerById(int id)
        {
            var foundCustomer = await _customerContext.Customers.FindAsync(id);

            if (foundCustomer == null)
            {
                return null;
            }

            var customer = await _customerContext.Customers.FirstOrDefaultAsync(c => c.CustomerId == id);

            var customerDTO = _mapper.Map<Customer, CustomerResponseModel>(customer);

            return customerDTO;
        }

        public async Task UpdateCustomer(int id, CustomerRequestModel customer)
        {
            var existingCustomer = await FindCustomer(id);
            if (existingCustomer == null)
            {
                throw new KeyNotFoundException("No record Exists to update");
            }
            var mappedCustomer = _mapper.Map<CustomerRequestModel, Customer>(customer, existingCustomer);           

            _customerContext.Customers.Update(mappedCustomer);
            _customerContext.SaveChanges();
        }

        private async Task<Customer> FindCustomer(int id)
        {
            return await _customerContext.Customers.FindAsync(id);
        }
    }
}
