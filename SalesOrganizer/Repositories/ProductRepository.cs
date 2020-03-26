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
    public class ProductRepository : IProductRepository
    {
        private readonly CustomerContext _customerContext;
        private readonly IMapper _mapper;

        public ProductRepository(CustomerContext customerContext, IMapper mapper)
        {
            _customerContext = customerContext;
            _mapper = mapper;
        }

        public async Task AddProduct(ProductRequestModel product)
        {
            if (product == null)
            {
                throw new ArgumentException("Product can't be null");
            }
            var mappedCustomer = _mapper.Map<ProductRequestModel, Product>(product);

            await _customerContext.Products.AddAsync(mappedCustomer);
            _customerContext.SaveChanges();
        }

        public async Task DeleteProduct(int id)
        {
            var product = await FindProduct(id);
            if (product == null)
            {
                throw new KeyNotFoundException();
            }

            _customerContext.Products.Remove(product);
            _customerContext.SaveChanges();
        }

        public async Task<IEnumerable<ProductResponseModel>> GetAllProducts()
        {
            var productDTO = await _customerContext.Products.ToListAsync();

            List<ProductResponseModel> products = new List<ProductResponseModel>();
            foreach (var product in productDTO)
            {
                products.Add(_mapper.Map<Product, ProductResponseModel>(product));
            }

            return products;
        }

        public async Task<ProductResponseModel> GetProductById(int id)
        {
            var productDTO = await _customerContext.Products.FirstOrDefaultAsync(p => p.ProductId == id);

            return _mapper.Map<Product, ProductResponseModel>(productDTO);

        }

        public async Task UpdateProduct(int id, ProductRequestModel prouct)
        {
            var existingProduct = await FindProduct(id);

            if (existingProduct == null)
            {
                throw new KeyNotFoundException("No record Exists to update");
            }
            var mapperProduct = _mapper.Map<ProductRequestModel, Product>(prouct, existingProduct);
            _customerContext.Products.Update(mapperProduct);
            _customerContext.SaveChanges();
        }
        private async Task<Product> FindProduct(int id)
        {
            return await _customerContext.Products.FindAsync(id);
        }
    }
}
