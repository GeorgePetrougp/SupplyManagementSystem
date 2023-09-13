using SupplyMS.Domain;
using SupplyMS.UseCases.Interfaces;
using SupplyMS.UseCases.PluginInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupplyMS.UseCases.Inventories
{
    public class AddInventoryUseCase : IAddInventoryUseCase
    {
        private readonly IInventoryRepository _repo;

        public AddInventoryUseCase(IInventoryRepository repo)
        {
            _repo = repo;
        }
        public async Task ExecuteAsync(Inventory inventory)
        {

            await _repo.AddInventoryAsync(inventory);
        }
    }
}
