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
        public async Task<IEnumerable<OrderResponseModel>> GetOrders()
        {
            return await _orderRepository.GetAllOrders();
        }

        // GET: api/Orders/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderResponseModel>> GetOrder(int id)
        {
            var order = await _orderRepository.GetOrder(id);

            if (order == null)
            {
                return NotFound();
            }
            return Ok(order);
        }
        
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
            catch(Exception)
            {
                return BadRequest();
            }
        }
    }
}
