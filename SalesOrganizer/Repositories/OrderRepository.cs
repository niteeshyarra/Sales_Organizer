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
            var orderDTO = await _customerContext.Orders.Include(p => p.ProductOrders)
                                                        .FirstOrDefaultAsync(o => o.OrderId == id);
            return _mapper.Map<Order, OrderResponseModel>(orderDTO);
        }

        public void DeleteOrder(int id)
        {
            var order = GetOrder(id);

            if (order == null)
            {
                throw new ArgumentException("The record Does not Exist");
            }

            var mappedOrder = _mapper.Map<OrderResponseModel, Order>(order.Result);
            _customerContext.Orders.Remove(mappedOrder);
            _customerContext.SaveChanges();
        }

        public IEnumerable<OrderResponseModel> GetOrdersByCustomer(int id)
        {
            var orders = _customerContext.Orders.Include(p => p.ProductOrders).Where(c => c.CustomerId == id);
            List<OrderResponseModel> orderDTO = new List<OrderResponseModel>();

            foreach (var order in orders)
            {
                orderDTO.Add(_mapper.Map<Order, OrderResponseModel>(order));
            }
            return orderDTO;
        }

        public IEnumerable<OrderResponseModel> GetOrdersByProduct(int id)
        {
            List<OrderResponseModel> ordersDTO = new List<OrderResponseModel>();
            var orders = _customerContext.ProductOrders.Where(p => p.ProductId == id)
                                                    .Select(p => p.Order);

            foreach (var order in orders)
            {
                ordersDTO.Add(_mapper.Map<Order, OrderResponseModel>(order));
            }

            return ordersDTO;
        }

        public async Task<IEnumerable<OrderResponseModel>> GetAllOrders()
        {
            var orders = await _customerContext.Orders.Include(p => p.ProductOrders).ToArrayAsync();

            List<OrderResponseModel> ordersDTO = new List<OrderResponseModel>();


            foreach (var order in orders)
            {
                ordersDTO.Add(_mapper.Map<Order, OrderResponseModel>(order));
            }

            return ordersDTO;
        }

        public async Task<bool> FindOrder(int id)
        {
            var order = await _customerContext.Orders.FindAsync(id);
            return order == null ? false : true;
        }
    }
}
