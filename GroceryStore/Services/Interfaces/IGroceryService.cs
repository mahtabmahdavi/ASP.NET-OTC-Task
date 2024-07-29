using GroceryStore.Models;

namespace GroceryStore.Services.Interfaces
{
    public interface IGroceryService
    {
        Task<(List<Entity> changedPrices,
            List<Entity> removedEntities,
            List<Entity> addedEntities)>
            ComparePricesAsync(DateTime yesterday, DateTime today);
    }
}
