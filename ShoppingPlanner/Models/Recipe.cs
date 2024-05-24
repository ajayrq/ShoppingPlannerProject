using System.Collections.Generic;

namespace ShoppingPlanner.Models
{
    public class Recipe
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<RecipeIngredient> RecipeIngredients { get; set; }
    }
}
