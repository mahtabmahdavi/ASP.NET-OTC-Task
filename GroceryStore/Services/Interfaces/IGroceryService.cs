using GroceryStore.Models.Dtos;
using GroceryStore.Models.Entities;

namespace GroceryStore.Services.Interfaces
{
    public interface IGroceryService
    {
        Task AddNewEntitiesAsync(IEnumerable<EntityRequestDto> entities);
        Task<IEnumerable<EntityResponseDto>> GetChangedPricesAsync();
    }
}
