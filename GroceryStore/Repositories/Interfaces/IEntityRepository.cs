using GroceryStore.Models.Entities;

namespace GroceryStore.Repositories.Interfaces
{
    public interface IEntityRepository
    {
        Task<Entity> GetEntityByNameAndDateAsync(string name, DateTime date);
        Task AddEntitiesAsync(IEnumerable<Entity> entities);
    }
}
