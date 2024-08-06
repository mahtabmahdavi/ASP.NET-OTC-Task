namespace GroceryStore.Models.Dtos
{
    public class EntityServiceResponseDto
    {
        public IEnumerable<ChangedPriceEntityDto> ChangedPriceEntities { get; set; }
        public IEnumerable<EntityDto> RemovedEntities { get; set; }
        public IEnumerable<EntityDto> AddedEntities { get; set; }
    }
}
