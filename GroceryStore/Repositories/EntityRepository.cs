using GroceryStore.Data;
using GroceryStore.Repositories.Interfaces;
namespace GroceryStore.Repositories
{
    public class EntityRepository : IEntityRepository
    {
        private readonly ApplicationDbContext _context;

        public EntityRepository(ApplicationDbContext context)
        {
            _context = context;
        }
    }
}
