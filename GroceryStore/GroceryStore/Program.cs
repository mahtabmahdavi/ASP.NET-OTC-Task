using GroceryStore;

public class PriceComparison
{
    public static void Main()
    {
        var yesterdayEntities = new List<Entity>
        {
            new Entity { EntityId = Guid.NewGuid(), Name = "Apple", Price = 1.0, FetchDate = DateTime.Now.AddDays(-1) },
            new Entity
            {
                EntityId = Guid.NewGuid(), Name = "Banana", Price = 0.5, FetchDate = DateTime.Now.AddDays(-1)
            },
            new Entity { EntityId = Guid.NewGuid(), Name = "Orange", Price = 0.8, FetchDate = DateTime.Now.AddDays(-1) }
        };

        var todayEntities = new List<Entity>
        {
            new Entity
            {
                EntityId = yesterdayEntities[0].EntityId, Name = "Apple", Price = 1.2, FetchDate = DateTime.Now
            },
            new Entity { EntityId = Guid.NewGuid(), Name = "Mango", Price = 1.5, FetchDate = DateTime.Now },
            new Entity
            {
                EntityId = yesterdayEntities[2].EntityId, Name = "Orange", Price = 0.8, FetchDate = DateTime.Now
            }
        };
    }
}
