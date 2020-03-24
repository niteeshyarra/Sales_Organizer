using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SalesOrganizer.Repositories.Interfaces;
using SalesOrganizer.RequestModels;
using SalesOrganizer.ResponseModels;

namespace SalesOrganizer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;

        public OrdersController(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        // GET: api/Orders
        [HttpGet]
        [Route("Orders/GetOrders")]
        public async Task<IEnumerable<OrderResponseModel>> GetOrders()
        {
            return await _orderRepository.GetAllOrders();
        }

        // GET: api/Orders/5
        [HttpGet]
        [Route("Orders/GetOrder/{id}")]
        public async Task<ActionResult<OrderResponseModel>> GetOrder(int id)
        {
            var order = await _orderRepository.GetOrder(id);

            if (order == null)
            {
                return NotFound();
            }

            return Ok(order);
        }
        

        // POST: api/Orders
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult> PostOrder(OrderRequestModel order)
        {
            await _orderRepository.AddOrder(order);
            return Ok();
        }

        // DELETE: api/Orders/5
        [HttpDelete("{id}")]
        public ActionResult DeleteOrder(int id)
        {
            try
            {
                _orderRepository.DeleteOrder(id);
                return Ok();
            }
            catch(Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpGet]
        [Route("Orders/GetOrdersByCustomer/{id}")]
        public IEnumerable<OrderResponseModel> GetOrdersByCustomer(int id)
        {
            return _orderRepository.GetOrdersByCustomer(id);
        }
        
        [HttpGet]
        [Route("Orders/GetOrdersByProduct/{id}")]
        public IEnumerable<OrderResponseModel> GetOrdersByProduct(int id)
        {
            return _orderRepository.GetOrdersByProduct(id);
        }
    }
}
