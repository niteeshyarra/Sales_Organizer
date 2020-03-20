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
    public class ProductRepository : IProductRepository
    {
        private readonly CustomerContext _customerContext;
        private readonly IMapper _mapper;

        public ProductRepository(CustomerContext customerContext, IMapper mapper)
        {
            _customerContext = customerContext;
            _mapper = mapper;
        }        

        public async Task AddProduct(ViewModels.ProductViewModel product)
        {
            if(product == null)
            {
                throw new ArgumentException("Product can't be null");
            }
            var mappedCustomer = _mapper.Map<ProductViewModel, Product>(product);

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

            var mappedProduct = _mapper.Map<ProductViewModel, Product>(product.Result);
            _customerContext.Products.Remove(mappedProduct);
            _customerContext.SaveChanges();
        }

        public async Task<IEnumerable<ViewModels.ProductViewModel>> GetAllProducts()
        {
            var productDTO = await _customerContext.Products.ToListAsync();

            List<ProductViewModel> products = new List<ProductViewModel>();
            foreach(var product in productDTO)
            {
                products.Add(_mapper.Map<Product, ProductViewModel>(product));
            }

            return products;
        }

        public async Task<ViewModels.ProductViewModel> GetProduct(int id)
        {
            var productDTO = await _customerContext.Products.FirstOrDefaultAsync(p => p.ProductId == id);

            return _mapper.Map<Product, ProductViewModel>(productDTO);

        }

        public void UpdateProduct(ProductViewModel prouct)
        {
            var productDTO = _mapper.Map<ProductViewModel, Product>(prouct);
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
