using SupplyMS.Domain;

namespace SupplyMS.UseCases.Interfaces
{
    public interface IValidateInventoriesForProducingUseCase
    {
        Task<bool> ExecuteAsync(Product product, int quantity);
    }
}