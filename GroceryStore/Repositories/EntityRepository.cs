using GroceryStore.Data;
using GroceryStore.Models;
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

        public async Task<List<Entity>> GetEntitiesByDateAsync(DateTime date)
        {
            return await _context.Entities.Where(e => e.FetchDate.Date == date.Date).ToListAsync();
        }

        public async Task AddEntitiesAsync(List<Entity> entities)
        {
            _context.Entities.AddRange(entities);
            await _context.SaveChangesAsync();
        }
    }
}
