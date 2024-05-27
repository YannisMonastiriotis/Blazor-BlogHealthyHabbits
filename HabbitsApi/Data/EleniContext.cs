using Microsoft.EntityFrameworkCore;

namespace HabbitsApi.Data
{
    public class EleniContext : DbContext
    {
        public DbSet<Recipe>? Recipes { get; set; }
        public DbSet<Product>? Products { get; set; }
        public DbSet<Ingredient>? Ingredients { get; set; }

        public EleniContext(DbContextOptions<EleniContext> dbContextOptions) : base(dbContextOptions)
        {

        }


    }
}
