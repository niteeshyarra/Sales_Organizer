using SalesOrganizer.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesOrganizer.Repositories.Interfaces
{
    public interface IProductRepository
    {
        void AddProduct(Product product);
        Product GetProduct(int id);
        void UpdateProduct(Product product);
        void DeleteProduct(int id);
        IEnumerable<Product> GetAllProducts();
    }
}
