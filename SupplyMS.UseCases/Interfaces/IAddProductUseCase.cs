using SupplyMS.Domain;

namespace SupplyMS.UseCases.Interfaces
{
    public interface IAddProductUseCase
    {
        Task ExecuteAsync(Product product);
    }
}