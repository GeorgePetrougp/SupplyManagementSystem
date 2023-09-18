using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupplyMS.Domain
{
    public class InventoryTransaction
    {
        public int InventoryTransactionId { get; set; }

        [Required]
        public int InventoryId { get; set; }

        [Required]
        public int QuantityBefore { get; set; }

        [Required]
        public InventoryTransactionType InventoryType { get; set; }

        [Required]
        public int QuantityAfter { get; set; }
        public string PoNumber { get; set; }
        public string ProductionNumber { get; set; }

        [Required]
        public DateTime TransactionDate { get; set; }

        [Required]
        public string DoneBy { get; set; }

        //Nav
        public Inventory Inventory { get; set; }

    }
}
