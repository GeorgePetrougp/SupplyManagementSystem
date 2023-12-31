﻿using Microsoft.EntityFrameworkCore;
using SupplyMS.Domain;
using SupplyMS.UseCases.PluginInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupplyMS.Plugins.EFCore
{
    public class ProductTransactionRepository : IProductTransactionRepository
    {
        private readonly DataContext _context;
        private readonly IProductRepository _productRepository;

        public ProductTransactionRepository(DataContext context, IProductRepository productRepository)
        {
            _context = context;
            _productRepository = productRepository;
        }

        public async Task<IEnumerable<ProductTransaction>> GetProductTransactionAsync(string productName, DateTime? dateFrom, DateTime? dateTo, ProductTransactionType? transactionType)
        {
            if (dateTo.HasValue) dateTo = dateTo.Value.AddDays(1);
            var query = from pt in _context.ProductTransactions
                        join prod in _context.Products on pt.ProductId equals prod.ProductId
                        where 
                            (string.IsNullOrWhiteSpace(productName) || prod.ProductName.ToLower().IndexOf(productName.ToLower()) >= 0) && 
                            (!dateFrom.HasValue || pt.TransactionDate >= dateFrom.Value.Date) &&
                            (!dateTo.HasValue || pt.TransactionDate <= dateTo.Value.Date) &&
                            (!transactionType.HasValue || pt.ActivityType == transactionType)
                        select pt;

            return await query.Include(x => x.Product).ToListAsync();   
        }

        public async Task ProduceAsync(string productionNumber, Product product, int quantity, double price, string doneBy)
        {

            //take away the inventories
            var prod = await _productRepository.GetProductByIdAsync(product.ProductId);
            //var prod = await _context.Products.Include(x => x.ProductInventories).ThenInclude(x => x.Inventory).FirstOrDefaultAsync(x => x.ProductId == product.ProductId);

            if(prod != null)
            {
                foreach(var item in prod.ProductInventories)
                {
                    int quantityBefore = item.Inventory.Quantity;
                    item.Inventory.Quantity -= quantity * item.InventoryQuantity;

                    _context.InventoryTransactions.Add(new InventoryTransaction
                    {
                        ProductionNumber = productionNumber,
                        InventoryId = item.Inventory.InventoryId,
                        QuantityBefore = quantityBefore,
                        ActivityType = InventoryTransactionType.ProduceProduct,
                        QuantityAfter = item.Inventory.Quantity,
                        TransactionDate = DateTime.Now,
                        DoneBy = doneBy,
                        UnitPrice = price
                    });
                }
            }

            


            _context.ProductTransactions.Add(new ProductTransaction
            {
                PoNumber = productionNumber,
                ProductId = product.ProductId,
                QuantityBefore = product.Quantity,
                ActivityType = ProductTransactionType.ProduceProduct,
                QuantityAfter = product.Quantity + quantity,
                TransactionDate = DateTime.Now,
                DoneBy = doneBy,
                UnitPrice = price
            });

            await _context.SaveChangesAsync();
        }

        public async Task SellProductAsync(string salesOrderNumber, Product product, int quantity, double price, string doneBy)
        {
            _context.ProductTransactions.Add(new ProductTransaction
            {
                SalesOrderNumber = salesOrderNumber,
                ProductId = product.ProductId,
                QuantityBefore = product.Quantity,
                QuantityAfter = product.Quantity - quantity,
                TransactionDate = DateTime.Now,
                DoneBy = doneBy,
                UnitPrice = price,
                ActivityType = ProductTransactionType.SellProduct
            });

            await _context.SaveChangesAsync();
        }
    }
}
