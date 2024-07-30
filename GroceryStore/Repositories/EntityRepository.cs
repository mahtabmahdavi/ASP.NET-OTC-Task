using GroceryStore.Data;
using GroceryStore.Models.Entities;
using GroceryStore.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
namespace GroceryStore.Repositories
{
    public class EntityRepository : IEntityRepository
    {
        private readonly ApplicationDbContext _context;

        public EntityRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Entity> GetEntityByNameAndDateAsync(string name, DateTime date)
        {
            return await _context.Entities
                .FirstOrDefaultAsync(e => e.Name == name && e.FetchDate.Date == date.Date);
        }

        public async Task AddEntitiesAsync(IEnumerable<Entity> entities)
        {
            _context.Entities.AddRange(entities);
            await _context.SaveChangesAsync();
        }
    }
}
