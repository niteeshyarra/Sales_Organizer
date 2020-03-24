using Microsoft.AspNetCore.Mvc;
using SalesOrganizer.Repositories.Interfaces;
using SalesOrganizer.RequestModels;
using SalesOrganizer.ResponseModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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
        public async Task<IEnumerable<CustomerResponseModel>> GetCustomers()
        {
            return await _customerRepository.GetAllCustomers();
        }

        // GET: api/Customers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerResponseModel>> GetCustomer(int id)
        {
            var customer = await _customerRepository.GetCustomer(id);
            if(customer == null)
            {
                return NotFound();
            }
            return customer;
        }

        // PUT: api/Customers/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, CustomerRequestModel customer)
        {
            try
            {
                await _customerRepository.UpdateCustomer(id, customer);
                return Ok();
            }
            catch(Exception e)
            {
                return BadRequest(e);
            }
            
        }

        // POST: api/Customers
        [HttpPost]
        public async Task<ActionResult> Add(CustomerRequestModel customer)
        {
            await _customerRepository.AddCustomer(customer);

            return Ok();
        }

        // DELETE: api/Customers/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await _customerRepository.DeleteCustomer(id);
                return Ok();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (Exception)
            {
                return StatusCode(500);
            }            
        }        
    }
}
