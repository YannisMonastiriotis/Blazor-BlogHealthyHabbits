using Microsoft.EntityFrameworkCore;

namespace EleniBlog.Data
{
    public class EleniContext : DbContext
    {
        public DbSet<Recipe>? Recipes { get; set; }
        public DbSet<Product>? Products { get; set; }
        public DbSet<Ingredient>? Ingredients { get; set; }

      public EleniContext(DbContextOptions<EleniContext> dbContextOptions) :base( dbContextOptions )
        { 

        }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Ingredient>()
        //        .HasOne(i => i.Recipe) // Ingredient has one Recipe
        //        .WithMany(r => r.Ingredients) // Recipe has many Ingredients
        //        .HasForeignKey(i => i.RecipeId) // Ingredient has RecipeId as foreign key
        //        .OnDelete(DeleteBehavior.Cascade); // Cascade delete: if a Recipe is deleted, its Ingredients are also deleted
        //}

        public class Ingredient
        {
            public int Id { get; set; }
            public string? Name { get; set; }
            public string? Quantity { get; set; }

            // Navigation properties
            public int RecipeId { get; set; } // Foreign key
            public Recipe? Recipe { get; set; } // Reference navigation property
        }

        public class Product
        {
            public int Id { get; set; }
            public string? Name { get; set; }
            public decimal Price { get; set; }
            public string? Description { get; set; }
            public int StockQuantity { get; set; }
        }

        public class Recipe
        {
            public int Id { get; set; }
            public string? Title { get; set; }
            public string? Description { get; set; }
            public string? ImageUrl { get; set; }
            public List<Ingredient> Ingredients { get; set; } = new List<Ingredient>(); // Navigation property
        }
    }
}
