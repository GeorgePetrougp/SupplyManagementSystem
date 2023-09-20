using Microsoft.EntityFrameworkCore;
using SupplyMS.Domain;
using SupplyMS.UseCases.PluginInterfaces;

namespace SupplyMS.Plugins.EFCore
{
    public class InventoryRepository : IInventoryRepository
    {
        private readonly DataContext _context;

        public InventoryRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Inventory>> GetInventoriesByName(string name)
        {
            return await _context.Inventories.Where(i => i.InventoryName.Contains(name, StringComparison.OrdinalIgnoreCase) 
                                                        || string.IsNullOrWhiteSpace(name)).ToListAsync();
        }

        public async Task AddInventoryAsync(Inventory inventory)
        {
            //prevent different inventories from having the same name 
            if (_context.Inventories.Any(i => i.InventoryName.Equals(inventory.InventoryName, StringComparison.OrdinalIgnoreCase)))
            {
                return;
            }

            _context.Inventories.Add(inventory);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateInventoryAsync(Inventory inventory)
        {
            //prevent different inventories from having the same name 
            if(_context.Inventories.Any(i => i.InventoryId != inventory.InventoryId && i.InventoryName.Equals(inventory.InventoryName, StringComparison.OrdinalIgnoreCase)))
            {
                return;
            }
            var currentInventory = await _context.Inventories.FindAsync(inventory.InventoryId);
            if (currentInventory != null)
            {
                currentInventory.InventoryName = inventory.InventoryName;
                currentInventory.Price = inventory.Price;
                currentInventory.Quantity = inventory.Quantity;

                await _context.SaveChangesAsync();
            }

        }

        public async Task<Inventory?> GetInventoryByIdAsync(int inventoryId)
        {
           return await _context.Inventories.FindAsync(inventoryId);
        }
    }
}