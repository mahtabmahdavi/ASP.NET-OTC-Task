using GroceryStore.Models;
using GroceryStore.Services.Interfaces;

namespace GroceryStore.Services
{
    public class GroceryService : IGroceryService
    {
        public Task<(List<Entity> changedPrices,
            List<Entity> removedEntities,
            List<Entity> addedEntities)>
            ComparePricesAsync(DateTime yesterday, DateTime today)
        {
            throw new NotImplementedException();
        }
    }
}
