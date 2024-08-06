namespace GroceryStore.Models.Dtos
{
    public class ChangedPriceEntityDto
    {
        public Guid EntityId { get; set; }
        public string Name { get; set; }
        public double PriceDifferential{ get; set; }
    }
}
