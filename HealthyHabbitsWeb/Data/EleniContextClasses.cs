

using System.ComponentModel.DataAnnotations;

namespace EleniBlog.Data
{
    public class Ingredient
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Quantity { get; set; }

        // Navigation properties
        public int RecipeId { get; set; } // Foreign key
        public Recipe? Recipe { get; set; } // Reference navigation property
    }

    public class Product
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        public decimal Price { get; set; }
        public string? Description { get; set; }
        public int StockQuantity { get; set; }
    }

    public class Recipe
    {
        [Key]
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
        public DateTime? CreatedDateTime { get; set; }
        //public List<Ingredient> Ingredients { get; set; } = new List<Ingredient>(); // Navigation property
    }

}
