using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SalesOrganizer.DataContexts;
using SalesOrganizer.DataModels;
using SalesOrganizer.Repositories.Interfaces;
using SalesOrganizer.ViewModels;
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
        public async Task AddOrder(ViewModels.OrderViewModel order)
        {
            var mappedOrder = _mapper.Map<OrderViewModel, Order>(order);
            await _customerContext.Orders.AddAsync(mappedOrder);
            _customerContext.SaveChanges();
        }

        public async Task<ViewModels.OrderViewModel> GetOrder(int id)
        {
            var orderDTO = await _customerContext.Orders.FirstOrDefaultAsync(o => o.OrderId == id);
            return _mapper.Map<Order, OrderViewModel>(orderDTO);
        }

        public void DeleteOrder(int id)
        {
            var order = GetOrder(id);

            if(order == null)
            {
                throw new ArgumentException("The record Does not Exist");
            }

            var mappedOrder = _mapper.Map<OrderViewModel, Order>(order.Result);
            _customerContext.Orders.Remove(mappedOrder);
            _customerContext.SaveChanges();
        }

        public IEnumerable<OrderViewModel> GetOrdersByCustomer(int id)
        {
            var orders = _customerContext.Orders.Where(c => c.CustomerId == id);
            List<ViewModels.OrderViewModel> orderDTO = new List<ViewModels.OrderViewModel>();

            foreach(var order in orders){
                orderDTO.Add(_mapper.Map<Order, OrderViewModel>(order));
            }
            return orderDTO;
        }

        public IEnumerable<OrderViewModel> GetOrdersByProduct(int id)
        {
            List<OrderViewModel> ordersDTO = new List<OrderViewModel>();
            var orders = _customerContext.ProductOrders.Where(p => p.ProductId == id)
                                                    .Select(p => p.Order);

            foreach(var order in orders)
            {
                ordersDTO.Add(_mapper.Map<Order, OrderViewModel>(order));
            }

            return ordersDTO;
        }

        public async Task<IEnumerable<OrderViewModel>> GetAllOrders()
        {
           var orders = await _customerContext.Orders.ToArrayAsync();

            List<OrderViewModel> ordersDTO = new List<OrderViewModel>();
            

            foreach (var order in orders)
            {
                ordersDTO.Add(_mapper.Map<Order, OrderViewModel>(order));
            }

            return ordersDTO;

        }
    }
}
