using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupplyMS.Domain
{
    public class ProductInventory
    {
        public int InventoryId { get; set; }
        public Inventory? Inventory { get; set; }
        public int ProductId { get; set; }
        public Product? Product { get; set; }

        //property that cannot be managed by the navigational relationships
        public int InventoryQuantity { get; set; }
        
    }
}
