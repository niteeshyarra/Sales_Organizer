using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesOrganizer.Repositories.Interfaces
{
    public interface IProductRepository
    {
        Task AddProduct(ViewModels.Product product);
        Task<ViewModels.Product> GetProduct(int id);
        void UpdateProduct(ViewModels.Product product);
        void DeleteProduct(int id);
        Task<IEnumerable<ViewModels.Product>> GetAllProducts();
    }
}
