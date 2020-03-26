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
    public class OrderRepository : IOrderRepository
    {
        private readonly CustomerContext _customerContext;
        private readonly IMapper _mapper;

        public OrderRepository(CustomerContext customerContext, IMapper mapper)
        {
            _customerContext = customerContext;
            _mapper = mapper;
        }
        public async Task AddOrder(OrderRequestModel order)
        {
            var mappedOrder = _mapper.Map<OrderRequestModel, Order>(order);
            await _customerContext.Orders.AddAsync(mappedOrder);
            _customerContext.SaveChanges();
        }

        public async Task<OrderResponseModel> GetOrder(int id)
        {
            var orderDTO = await _customerContext.Orders
                                                        .Include(o => o.Customer)
                                                        .Include(o => o.ProductOrders)
                                                            .ThenInclude(po => po.Product)
                                                        .FirstOrDefaultAsync(o => o.OrderId == id);
            return _mapper.Map<Order, OrderResponseModel>(orderDTO);
        }

        public async Task DeleteOrder(int id)
        {
            var order = await FindOrder(id);

            if (order == null)
            {
                throw new KeyNotFoundException();
            }
            _customerContext.Orders.Remove(order);
            _customerContext.SaveChanges();
        }

        public IEnumerable<OrderResponseModel> GetOrdersByCustomer(int id)
        {
            var orders = _customerContext.Orders.Include(p => p.ProductOrders).Where(c => c.CustomerId == id);
            
            return _mapper.Map<IEnumerable<Order>, IEnumerable<OrderResponseModel>>(orders);
        }

        public IEnumerable<OrderResponseModel> GetOrdersByProduct(int id)
        {
            List<OrderResponseModel> ordersDTO = new List<OrderResponseModel>();
            var orders = _customerContext.ProductOrders
                                                        .Include(po => po.Order).ThenInclude(o => o.Customer)
                                                        .Include(po => po.Order).ThenInclude(o => o.ProductOrders)
                                                        .Where(p => p.ProductId == id)
                                                        .Select(po => po.Order);


            return _mapper.Map<IEnumerable<Order>, IEnumerable<OrderResponseModel>>(orders);
        }

        public async Task<IEnumerable<OrderResponseModel>> GetAllOrders()
        {
            var orders = await _customerContext.Orders
                                            .Include(o => o.Customer)
                                            .Include(p => p.ProductOrders)
                                                .ThenInclude(po => po.Product).ToListAsync();

            return _mapper.Map<IEnumerable<Order>, IEnumerable<OrderResponseModel>>(orders);
        }

        private async Task<Order> FindOrder(int id)
        {
            return await _customerContext.Orders.FindAsync(id);
            
        }
    }
}
