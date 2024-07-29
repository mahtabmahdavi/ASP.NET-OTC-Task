using GroceryStore;

public class Program
{
    public static void Main()
    {
        var yesterdayEntities = new List<Entity>
        {
            new Entity { EntityId = Guid.NewGuid(), Name = "Apple", Price = 1.0, FetchDate = DateTime.Now.AddDays(-1) },
            new Entity { EntityId = Guid.NewGuid(), Name = "Banana", Price = 0.5, FetchDate = DateTime.Now.AddDays(-1) },
            new Entity { EntityId = Guid.NewGuid(), Name = "Orange", Price = 0.8, FetchDate = DateTime.Now.AddDays(-1) }
        };

        var todayEntities = new List<Entity>
        {
            new Entity { EntityId = yesterdayEntities[0].EntityId, Name = "Apple", Price = 1.2, FetchDate = DateTime.Now },
            new Entity { EntityId = Guid.NewGuid(), Name = "Mango", Price = 1.5, FetchDate = DateTime.Now },
            new Entity { EntityId = yesterdayEntities[2].EntityId, Name = "Orange", Price = 0.8, FetchDate = DateTime.Now }
        };
        
        var (changedPrices,
            removedEntities,
            addedEntities) = ComparePrices(yesterdayEntities, todayEntities);
        
        Console.WriteLine("Changed Prices:");
        PrintEntities(changedPrices);

        Console.WriteLine("\nRemoved Entities:");
        PrintEntities(removedEntities);

        Console.WriteLine("\nAdded Entities:");
        PrintEntities(addedEntities);
    }
    
    public static (List<Entity> changedPrices, List<Entity> removedEntities, List<Entity> addedEntities) 
        ComparePrices(List<Entity> yesterdayEntities, List<Entity> todayEntities)
    {
        var yesterdayDict = yesterdayEntities.ToDictionary(e => e.EntityId, e => e);
        var todayDict = todayEntities.ToDictionary(e => e.EntityId, e => e);

        var changedPrices = new List<Entity>();
        var removedEntities = new List<Entity>();
        var addedEntities = new List<Entity>();

        foreach (var kvp in yesterdayDict)
        {
            if (todayDict.TryGetValue(kvp.Key, out var todayEntity))
            {
                if (!kvp.Value.Price.Equals(todayEntity.Price))
                    changedPrices.Add(todayEntity);
            }
            else
                removedEntities.Add(kvp.Value);
        }

        foreach (var kvp in todayDict)
        {
            if (!yesterdayDict.ContainsKey(kvp.Key))
                addedEntities.Add(kvp.Value);
        }

        return (changedPrices, removedEntities, addedEntities);
    }
    
    private static void PrintEntities(List<Entity> entities)
    {
        foreach (var entity in entities)
            Console.WriteLine($"ID: {entity.EntityId}, Name: {entity.Name}, Price: {entity.Price}");
    }
}
