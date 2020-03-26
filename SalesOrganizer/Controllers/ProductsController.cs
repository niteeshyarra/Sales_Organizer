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
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly IOrderRepository _orderRepository;

        public ProductsController(IProductRepository productRepository, IOrderRepository orderRepository)
        {
            _productRepository = productRepository;
            _orderRepository = orderRepository;
        }

        // GET: api/Products
        [HttpGet]
        public async Task<IEnumerable<ProductResponseModel>> GetProducts()
        {
            return await _productRepository.GetAllProducts();
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductResponseModel>> GetProduct(int id)
        {

            var product = await _productRepository.GetProductById(id);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        [HttpPost]
        public ActionResult PostProduct(ProductRequestModel product)
        {
            _productRepository.AddProduct(product);
            return Ok();
        }

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            try
            {
                await _productRepository.DeleteProduct(id);
                return Ok();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (Exception)
            {
                return NotFound();
            }

        }

        [HttpPut]
        public async Task<ActionResult> UpdateProduct(int id, ProductRequestModel product)
        {
            try
            {
                await _productRepository.UpdateProduct(id, product);
                return Ok();
            }
            catch(KeyNotFoundException)
            {
                return NotFound();
            }
            
        }

        [HttpGet]
        [Route("{id}/ProductOrders")]
        public IEnumerable<ProductOrderResponseModel> GetOrdersByProduct(int id)
        {
            return _orderRepository.GetProductOrdersByProductId(id);
        }
    }
}