using GroceryStore.Models.Entities;
using GroceryStore.Repositories.Interfaces;

namespace GroceryStore.Services
{
    public class TestDataGenerator
    {
        private readonly IEntityRepository _entityRepository;

        public TestDataGenerator(IEntityRepository entityRepository)
        {
            _entityRepository = entityRepository;
        }

        public async Task GenerateTestDataAsync(int count)
        {
            var today = DateTime.Now.Date;
            var yesterday = today.AddDays(-1);

            var random = new Random();
            var entitiesToAdd = new List<Entity>();

            for (int i = 0; i < count; i++)
            {
                var name = $"Entity_{i}";
                var price = random.NextDouble() * 100;

                entitiesToAdd.Add(CreateEntity(name, price, today));
                entitiesToAdd.Add(CreateEntity(name, price * 0.9, yesterday));
            }

            const int batchSize = 1000;
            for (int i = 0; i < entitiesToAdd.Count; i += batchSize)
            {
                var batch = entitiesToAdd.Skip(i).Take(batchSize).ToList();
                await _entityRepository.AddEntitiesAsync(batch);
            }
        }

        private static Entity CreateEntity(string name, double price, DateTime date)
        {
            return new Entity
            {
                EntityId = Guid.NewGuid(),
                Name = name,
                Description = $"Description for {name}",
                Price = price,
                FetchDate = date
            };
        }
    }
}
