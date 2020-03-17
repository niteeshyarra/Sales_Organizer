using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SalesOrganizer.DataContexts;
using SalesOrganizer.DataModels;
using SalesOrganizer.Repositories;
using SalesOrganizer.Repositories.Interfaces;

namespace SalesOrganizer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomersController(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        // GET: api/Customers
        [HttpGet]
        public async Task<IEnumerable<Customer>> GetCustomers()
        {
            return await _customerRepository.GetAllCustomers();
        }

        // GET: api/Customers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> GetCustomer(int id)
        {
            var customer = await _customerRepository.GetCustomer(id);

            if (customer == null)
            {
                return NotFound();
            }

            return customer;
        }

        // PUT: api/Customers/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut]
        public ActionResult Update(Customer customer)
        {
            var retreivedCustomer = _customerRepository.GetCustomer(customer.CustomerId);
            if(retreivedCustomer == null)
            {
                return BadRequest();
            }
            _customerRepository.UpdateCustomer(customer);
            return Ok();
        }

        // POST: api/Customers
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult> Add(Customer customer)
        {            
            await _customerRepository.AddCustomer(customer);

            return Ok();
        }

        // DELETE: api/Customers/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var customer = GetCustomer(id);
            if (customer == null)
            {
                return NotFound();
            }

            _customerRepository.DeleteCustomer(id);
            return Ok();
        }
    }
}
