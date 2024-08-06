using GroceryStore.Models.Dtos;
using GroceryStore.Models.Entities;

namespace GroceryStore.Services.Interfaces
{
    public interface IGroceryService
    {
        Task AddNewEntitiesAsync(IEnumerable<EntityDto> entities);
        Task<EntityServiceResponseDto> GetComparisonAsync();
    }
}
