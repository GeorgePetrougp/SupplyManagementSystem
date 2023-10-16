using Microsoft.EntityFrameworkCore;
using SupplyMS.Domain;
using SupplyMS.UseCases.PluginInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupplyMS.Plugins.EFCore
{
    public class ProductRepository : IProductRepository
    {
        private readonly DataContext _context;

        public ProductRepository(DataContext context)
        {
            _context = context;
        }

        public async Task AddProductAsync(Product product)
        {
            //if (_context.Products.Any(p => p.ProductName.Equals(product.ProductName, StringComparison.OrdinalIgnoreCase))) return;
            if (_context.Products.Any(p => p.ProductName.ToLower() == product.ProductName.ToLower())) return;

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            //test if it really adds the product
            //var prods = _context.Products.Include(x => x.ProductInventories).ThenInclude(x => x.Inventory).ToList();

        }

        public async Task<Product> GetProductByIdAsync(int productId)
        {
            return await _context.Products.Include(p => p.ProductInventories).ThenInclude(P => P.Inventory).FirstOrDefaultAsync(p => p.ProductId == productId);
        }

        public async Task<List<Product>> GetProductsByNameAsync(string name)
        {
            return await _context.Products.Where(p => (p.ProductName.ToLower().IndexOf(name.ToLower()) >= 0
                                                        || string.IsNullOrWhiteSpace(name)) && p.IsActive == true).ToListAsync();
        }

        public async Task RemoveProductAsync(int productId)
        {
            //soft delete
            var product = await _context.Products.FindAsync(productId);
            if(product != null)
            {
                product.IsActive = false;
                await _context.SaveChangesAsync();
            }
        }

        public async Task UpdateProductAsync(Product product)
        {
           //prevent samename
          if( _context.Products.Any(p => p.ProductName.ToLower() == product.ProductName.ToLower())) return;

            var pro = await _context.Products.FindAsync(product.ProductId);
            if(pro != null)
            {
                pro.ProductName = product.ProductName;
                pro.Price = product.Price;
                pro.Quantity = product.Quantity;
                pro.ProductInventories = product.ProductInventories;

                await _context.SaveChangesAsync();
            }
        }
    }
}
