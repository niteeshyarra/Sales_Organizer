using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SalesOrganizer.DataContexts;
using SalesOrganizer.Repositories.Interfaces;
using SalesOrganizer.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesOrganizer.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly CustomerContext _customerContext;
        private readonly MapperConfiguration _dataModelConfig;
        private readonly MapperConfiguration _viewModelConfig;
        public CustomerRepository(CustomerContext customerContext)
        {
            _customerContext = customerContext;
           _dataModelConfig = new MapperConfiguration(cfg => {
                cfg.CreateMap<ViewModels.Customer, DataModels.Customer>();
            });
            _viewModelConfig = new MapperConfiguration(cfg => {
                cfg.CreateMap<DataModels.Customer, ViewModels.Customer>();
            });
        }
        public async Task AddCustomer(Customer customer)
        {

            if(customer == null)
            {
                throw new ArgumentException("Customer Can't be null");
            }

            IMapper mapper = _dataModelConfig.CreateMapper();
            var mappedCustomer = mapper.Map<ViewModels.Customer, DataModels.Customer>(customer);

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

            IMapper mapper = _dataModelConfig.CreateMapper();
            var mappedCustomer = mapper.Map<ViewModels.Customer, DataModels.Customer>(customer.Result);

            _customerContext.Customers.Remove(mappedCustomer);
            _customerContext.SaveChanges();
;        }

        public async Task<IEnumerable<Customer>> GetAllCustomers()
        {
            var customers =  await _customerContext.Customers.ToListAsync();

            List<ViewModels.Customer> customersDTO = new List<ViewModels.Customer>();
            IMapper mapper = _viewModelConfig.CreateMapper();
            foreach(var customer in customers)
            {
                customersDTO.Add(mapper.Map<DataModels.Customer, ViewModels.Customer>(customer));
            }

            return customersDTO;
        }

        public async Task<ViewModels.Customer> GetCustomer(int id)
        {
            var foundCustomer = _customerContext.Customers.FindAsync(id);

            if (foundCustomer == null)
            {
                throw new ArgumentException("No customer with this ID Exists");
            }

            var customer = await _customerContext.Customers.FirstOrDefaultAsync(c => c.CustomerId == id);

            IMapper mapper = _viewModelConfig.CreateMapper();
            var customerDTO = mapper.Map<DataModels.Customer, ViewModels.Customer>(customer);

            return customerDTO;
        }

        public void UpdateCustomer(Customer customer)
        {
            IMapper mapper = _dataModelConfig.CreateMapper();
            var mappedCustomer = mapper.Map<ViewModels.Customer, DataModels.Customer>(customer);

            var foundCustomer = _customerContext.Customers.FindAsync(mappedCustomer.CustomerId);

            if(foundCustomer == null)
            {
                throw new ArgumentException("No record Exists to update");
            }

            _customerContext.Customers.Update(mappedCustomer);
            _customerContext.SaveChanges();
        }
    }
}
