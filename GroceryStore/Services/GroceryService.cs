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
    }
}
