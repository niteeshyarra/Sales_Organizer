using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesOrganizer.Repositories.Interfaces
{
    public interface IProductRepository
    {
        Task AddProduct(ViewModels.ProductViewModel product);
        Task<ViewModels.ProductViewModel> GetProduct(int id);
        void UpdateProduct(ViewModels.ProductViewModel product);
        void DeleteProduct(int id);
        Task<IEnumerable<ViewModels.ProductViewModel>> GetAllProducts();
    }
}
