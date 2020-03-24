using AutoMapper;
using SalesOrganizer.DataModels;
using SalesOrganizer.RequestModels;
using SalesOrganizer.ResponseModels;

namespace SalesOrganizer.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CustomerRequestModel, Customer>();
            CreateMap<ProductRequestModel, Product>();
            CreateMap<OrderRequestModel, Order>();
            CreateMap<ProductOrderRequestModel, ProductOrder>();
            
            CreateMap<Customer, CustomerResponseModel>();
            CreateMap<Product, ProductResponseModel>();
            CreateMap<Order, OrderResponseModel>();
            CreateMap<ProductOrder, ProductOrderResponseModel>();
        }
    }
}
