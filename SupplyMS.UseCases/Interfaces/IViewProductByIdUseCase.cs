using SupplyMS.Domain;

namespace SupplyMS.UseCases.Interfaces
{
    public interface IViewProductByIdUseCase
    {
        Task<Product> ExecuteAsync(int productId);
    }
}