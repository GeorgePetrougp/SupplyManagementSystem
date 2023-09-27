using SupplyMS.Domain;
using SupplyMS.UseCases.Interfaces;
using SupplyMS.UseCases.PluginInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupplyMS.UseCases.Reports
{
    public class SearchProductTransactionsUseCase : ISearchProductTransactionsUseCase
    {
        private readonly IProductTransactionRepository _productTransactionRepository;

        public SearchProductTransactionsUseCase(IProductTransactionRepository productTransactionRepository)
        {
            _productTransactionRepository = productTransactionRepository;
        }


        public async Task<IEnumerable<ProductTransaction>> ExecuteAsync(string productName, DateTime? dateFrom, DateTime? dateTo, ProductTransactionType? transactionType)
        {
            return await _productTransactionRepository.GetProductTransactionAsync(productName, dateFrom, dateTo, transactionType);
        }
    }
}
