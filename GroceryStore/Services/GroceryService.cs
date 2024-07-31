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
        private readonly TimeSpan CacheDuration = TimeSpan.FromMinutes(30);

        public GroceryService(IEntityRepository entityRepository, IMemoryCache memoryCache)
        {
            _entityRepository = entityRepository;
            _memoryCache = memoryCache;
        }

        public async Task AddNewEntitiesAsync(IEnumerable<EntityRequestDto> entities)
        {
            var today = DateTime.Now.Date;
            var entitiesToAdd = new List<Entity>();

            foreach (var entityDto in entities)
            {
                if (await IsEntityExistingForTodayAsync(entityDto.Name, today))
                    throw new InvalidOperationException($"Entity with name '{entityDto.Name}' already exists for today.");
  
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
                await _entityRepository.AddEntitiesAsync(entitiesToAdd);
        }

        public async Task<IEnumerable<EntityResponseDto>> GetChangedPricesAsync()
        {
            var yesterdayEntities = await GetEntitiesByDateWithCacheAsync(DateTime.Now.AddDays(-1).Date);
            var todayEntities = await GetEntitiesByDateWithCacheAsync(DateTime.Now.Date);

            var yesterdayDict = yesterdayEntities.ToDictionary(e => e.Name, e => e.Price);
            var todayDict = todayEntities.ToDictionary(e => e.Name);

            return ConvertEntitiesToDto(todayDict
                .Where(kvp => yesterdayDict.ContainsKey(kvp.Key) && yesterdayDict[kvp.Key] != kvp.Value.Price)
                .Select(kvp => kvp.Value), yesterdayDict);
        }

        private async Task<bool> IsEntityExistingForTodayAsync(string name, DateTime date)
        {
            var cacheKey = $"{EntitiesCacheKeyPrefix}{name}_{date:yyyyMMdd}";

            if (_memoryCache.TryGetValue(cacheKey, out Entity existingEntity))
                return existingEntity != null;

            existingEntity = await _entityRepository.GetEntityByNameAndDateAsync(name, date);

            if (existingEntity != null)
            {
                _memoryCache.Set(cacheKey, existingEntity, CacheDuration);
                return true;
            }

            return false;
        }

        private async Task<IEnumerable<Entity>> GetEntitiesByDateWithCacheAsync(DateTime date)
        {
            var cacheKey = $"{EntitiesCacheKeyPrefix}{date:yyyyMMdd}";
            if (!_memoryCache.TryGetValue(cacheKey, out List<Entity> entities))
            {
                entities = (await _entityRepository.GetEntitiesByDateAsync(date)).ToList();
                _memoryCache.Set(cacheKey, entities, CacheDuration);
            }
            return entities;
        }

        private IEnumerable<EntityResponseDto> ConvertEntitiesToDto(IEnumerable<Entity> entities, Dictionary<string, double> previousPrices)
        {
            return entities.Select(entity => new EntityResponseDto
            {
                EntityId = entity.EntityId,
                Name = entity.Name,
                PriceDifferential = previousPrices.ContainsKey(entity.Name)
                                    ? entity.Price - previousPrices[entity.Name]
                                    : entity.Price
            });
        }
    }
}
