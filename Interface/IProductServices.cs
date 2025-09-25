using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace Interface
{
    public class IProductServices
    {
        Task<IEnumerable<Product>> GetAllProducts();
        Task<ProductResponse> CreateOrUpdateProductAsync(Product product);
    }
}
