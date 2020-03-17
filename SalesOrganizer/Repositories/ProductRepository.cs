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
        public ProductRepository(CustomerContext customerContext)
        {
            _customerContext = customerContext;
        }        

        public void AddProduct(Product product)
        {
            if(product == null)
            {
                throw new ArgumentException("Product can't be null");
            }
            _customerContext.Products.Add(product);
            _customerContext.SaveChanges();
        }

        public void DeleteProduct(int id)
        {
            var product = GetProduct(id);
            _customerContext.Products.Remove(product);
            _customerContext.SaveChanges();
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return _customerContext.Products.ToList();
        }

        public Product GetProduct(int id)
        {
            return _customerContext.Products.FirstOrDefault(p => p.ProductId == id);
        }

        public void UpdateProduct(Product prouct)
        {
            _customerContext.Products.Update(prouct);
            _customerContext.SaveChanges();
        }
    }
}
