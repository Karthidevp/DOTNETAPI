using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using Services;
using static Model.ProductModel;

namespace Interfaces
{
    public interface IProductServices
    {
        Task<IEnumerable<Product>> GetAllProducts();
        Task<ProductResponse> CreateOrUpdateProductAsync(Product product);
    }
}
