using SupplyMS.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupplyMS.UseCases.PluginInterfaces
{
    public interface IProductRepository
    {
       Task<List<Product>> GetProductsByNameAsync(string name);
        Task AddProductAsync(Product product);
        Task<Product> GetProductByIdAsync(int productId);
        Task UpdateProductAsync(Product product);
        Task RemoveProductAsync(int productId);
    }
}
