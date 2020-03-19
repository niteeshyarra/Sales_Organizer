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
    public class ProductRepository : IProductRepository
    {
        private readonly CustomerContext _customerContext;
        private readonly MapperConfiguration _dataModelConfig;
        private readonly MapperConfiguration _viewModelConfig;
        public ProductRepository(CustomerContext customerContext)
        {
            _customerContext = customerContext;
            _dataModelConfig = new MapperConfiguration(cfg => {
                cfg.CreateMap<ViewModels.Product, DataModels.Product>();
            });
            _viewModelConfig = new MapperConfiguration(cfg => {
                cfg.CreateMap<DataModels.Product, ViewModels.Product>();
            });
        }        

        public async Task AddProduct(ViewModels.Product product)
        {
            if(product == null)
            {
                throw new ArgumentException("Product can't be null");
            }
            IMapper mapper = _dataModelConfig.CreateMapper();
            var mappedCustomer = mapper.Map<ViewModels.Product, DataModels.Product>(product);

            await _customerContext.Products.AddAsync(mappedCustomer);
            _customerContext.SaveChanges();
        }

        public void DeleteProduct(int id)
        {
            var product = GetProduct(id);
            if(product == null)
            {
                throw new ArgumentException("Record Doesn't Exist");
            }

            IMapper mapper = _dataModelConfig.CreateMapper();
            var mappedProduct = mapper.Map<ViewModels.Product, DataModels.Product>(product.Result);
            _customerContext.Products.Remove(mappedProduct);
            _customerContext.SaveChanges();
        }

        public async Task<IEnumerable<ViewModels.Product>> GetAllProducts()
        {
            var productDTO = await _customerContext.Products.ToListAsync();
            IMapper mapper = _viewModelConfig.CreateMapper();

            List<ViewModels.Product> products = new List<ViewModels.Product>();
            foreach(var product in productDTO)
            {
                products.Add(mapper.Map<DataModels.Product, ViewModels.Product>(product));
            }

            return products;
        }

        public async Task<ViewModels.Product> GetProduct(int id)
        {
            var productDTO = await _customerContext.Products.FirstOrDefaultAsync(p => p.ProductId == id);
            IMapper mapper = _viewModelConfig.CreateMapper();

            return mapper.Map<DataModels.Product, ViewModels.Product>(productDTO);

        }

        public void UpdateProduct(ViewModels.Product prouct)
        {
            IMapper mapper = _dataModelConfig.CreateMapper();
            var productDTO = mapper.Map<ViewModels.Product, DataModels.Product>(prouct);
            var foundProduct = _customerContext.Products.FindAsync(productDTO.ProductId);

            if (foundProduct == null)
            {
                throw new ArgumentException("No record Exists to update");
            }
            _customerContext.Products.Update(productDTO);
            _customerContext.SaveChanges();
        }
    }
}
