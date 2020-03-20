using AutoMapper;
using SalesOrganizer.DataModels;
using SalesOrganizer.ViewModels;

namespace SalesOrganizer.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CustomerViewModel, Customer>().ReverseMap();
            CreateMap<ProductViewModel, Product>().ReverseMap();
            CreateMap<OrderViewModel, Order>().ReverseMap();
        }
    }
}
