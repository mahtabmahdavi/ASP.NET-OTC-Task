namespace GroceryStore.Models.Entities
{
    public class Entity
    {
        public Guid EntityId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public DateTime FetchDate { get; set; }
    }
}
