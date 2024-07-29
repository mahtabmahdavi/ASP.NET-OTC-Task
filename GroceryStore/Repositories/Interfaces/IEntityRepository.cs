using GroceryStore.Models;

namespace GroceryStore.Repositories.Interfaces
{
    public interface IEntityRepository
    {
        Task<List<Entity>> GetEntitiesByDateAsync(DateTime date);
        Task AddEntitiesAsync(List<Entity> entities);
    }
}
