using SupplyMS.Domain;

namespace SupplyMS.UseCases.Interfaces
{
    public interface IViewProductsByNameUserCase
    {
        Task<List<Product>> ExecuteAsync(string name = "");
    }
}