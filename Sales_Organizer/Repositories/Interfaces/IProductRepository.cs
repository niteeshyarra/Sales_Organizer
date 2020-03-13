using Sales_Organizer.Data_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sales_Organizer.Repositories.Interfaces
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
