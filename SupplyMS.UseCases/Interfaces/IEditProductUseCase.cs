using SupplyMS.Domain;

namespace SupplyMS.UseCases.Interfaces
{
    public interface IEditProductUseCase
    {
        Task ExecuteAsync(Product product);
    }
}