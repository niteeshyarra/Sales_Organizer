using SalesOrganizer.RequestModels;
using SalesOrganizer.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesOrganizer.Repositories.Interfaces
{
    public interface IProductRepository
    {
        Task AddProduct(ProductRequestModel product);
        Task<ProductResponseModel> GetProduct(int id);
        void UpdateProduct(ProductRequestModel product);
        void DeleteProduct(int id);
        Task<IEnumerable<ProductResponseModel>> GetAllProducts();
        Task<bool> FindProduct(int id);
    }
}
