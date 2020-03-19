using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SalesOrganizer.DataContexts;
using SalesOrganizer.DataModels;
using SalesOrganizer.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesOrganizer.Repositories
{    
    public class OrderRepository : IOrderRepository
    {
        private readonly CustomerContext _customerContext;
        private readonly MapperConfiguration _dataModelConfig;
        private readonly MapperConfiguration _viewModelConfig;

        public OrderRepository(CustomerContext customerContext)
        {
            _customerContext = customerContext;
            _dataModelConfig = new MapperConfiguration(cfg => {
                cfg.CreateMap<ViewModels.Order, DataModels.Order>();
            });
            _viewModelConfig = new MapperConfiguration(cfg => {
                cfg.CreateMap<DataModels.Order, ViewModels.Order>();
            });
        }
        public async Task AddOrder(ViewModels.Order order)
        {
            IMapper mapper = _dataModelConfig.CreateMapper();
            var mappedOrder = mapper.Map<ViewModels.Order, DataModels.Order>(order);
            await _customerContext.Orders.AddAsync(mappedOrder);
            _customerContext.SaveChanges();
        }

        public async Task<ViewModels.Order> GetOrder(int id)
        {
            var orderDTO = await _customerContext.Orders.FirstOrDefaultAsync(o => o.OrderId == id);
            IMapper mapper = _viewModelConfig.CreateMapper();
            return mapper.Map<DataModels.Order, ViewModels.Order>(orderDTO);
        }

        public void DeleteOrder(int id)
        {
            var order = GetOrder(id);

            if(order == null)
            {
                throw new ArgumentException("The record Does not Exist");
            }

            IMapper mapper = _dataModelConfig.CreateMapper();
            var mappedOrder = mapper.Map<ViewModels.Order, DataModels.Order>(order.Result);
            _customerContext.Orders.Remove(mappedOrder);
            _customerContext.SaveChanges();
        }

        public IEnumerable<ViewModels.Order> GetOrdersByCustomer(int id)
        {
            var orders = _customerContext.Orders.Where(c => c.CustomerId == id);
            List<ViewModels.Order> orderDTO = new List<ViewModels.Order>();

            IMapper mapper = _viewModelConfig.CreateMapper();
            foreach(var order in orders){
                orderDTO.Add(mapper.Map<DataModels.Order, ViewModels.Order>(order));
            }
            return orderDTO;
        }

        public IEnumerable<ViewModels.Order> GetOrdersByProduct(int id)
        {
            List<ViewModels.Order> ordersDTO = new List<ViewModels.Order>();
            var orders = _customerContext.ProductOrders.Where(p => p.ProductId == id)
                                                    .Select(p => p.Order);

            IMapper mapper = _viewModelConfig.CreateMapper();
            foreach(var order in orders)
            {
                ordersDTO.Add(mapper.Map<DataModels.Order, ViewModels.Order>(order));
            }

            return ordersDTO;
        }

        public async Task<IEnumerable<ViewModels.Order>> GetAllOrders()
        {
           var orders = await _customerContext.Orders.ToArrayAsync();

            List<ViewModels.Order> ordersDTO = new List<ViewModels.Order>();
            

            IMapper mapper = _viewModelConfig.CreateMapper();
            foreach (var order in orders)
            {
                ordersDTO.Add(mapper.Map<DataModels.Order, ViewModels.Order>(order));
            }

            return ordersDTO;

        }
    }
}
