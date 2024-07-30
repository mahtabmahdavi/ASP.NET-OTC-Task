using GroceryStore.Models.Dtos;
using GroceryStore.Models.Entities;
using GroceryStore.Repositories.Interfaces;
using GroceryStore.Services.Interfaces;
using Microsoft.Extensions.Caching.Memory;

namespace GroceryStore.Services
{
    public class GroceryService : IGroceryService
    {
        private readonly IEntityRepository _entityRepository;
        private readonly IMemoryCache _memoryCache;
        private readonly string EntitiesCacheKeyPrefix = "Entities_";

        public GroceryService(IEntityRepository entityRepository, IMemoryCache memoryCache)
        {
            _entityRepository = entityRepository;
            _memoryCache = memoryCache;
        }

        public async Task AddNewEntitiesAsync(IEnumerable<EntityDto> entities)
        {
            var today = DateTime.Now.Date;
            var entitiesToAdd = new List<Entity>();

            foreach (var entityDto in entities)
            {
                if (await IsEntityExistingForTodayAsync(entityDto.Name, today))
                {
                    throw new InvalidOperationException($"Entity with name '{entityDto.Name}' already exists for today.");
                }

                var entity = new Entity
                {
                    EntityId = Guid.NewGuid(),
                    Name = entityDto.Name,
                    Description = entityDto.Description,
                    Price = entityDto.Price,
                    FetchDate = today
                };

                entitiesToAdd.Add(entity);
            }

            if (entitiesToAdd.Any())
            {
                await _entityRepository.AddEntitiesAsync(entitiesToAdd);
            }
        }

        private async Task<bool> IsEntityExistingForTodayAsync(string name, DateTime date)
        {
            var cacheKey = $"{EntitiesCacheKeyPrefix}{name}_{date:yyyyMMdd}";

            if (_memoryCache.TryGetValue(cacheKey, out Entity existingEntity))
                return true;

            existingEntity = await _entityRepository.GetEntityByNameAndDateAsync(name, date);

            if (existingEntity != null)
            {
                _memoryCache.Set(cacheKey, existingEntity);
                return true;
            }

            return false;
        }
    }
}
