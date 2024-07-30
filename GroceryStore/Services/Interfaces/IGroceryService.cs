using GroceryStore.Models.Dtos;

namespace GroceryStore.Services.Interfaces
{
    public interface IGroceryService
    {
        Task AddNewEntitiesAsync(IEnumerable<EntityDto> entities);
    }
}
