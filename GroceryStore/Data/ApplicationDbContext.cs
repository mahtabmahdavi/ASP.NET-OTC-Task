using GroceryStore.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace GroceryStore.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Entity> Entities { get; set; }
    }
}
