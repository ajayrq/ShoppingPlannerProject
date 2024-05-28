using ShoppingPlanner.Models;

namespace ShoppingPlanner.ViewModel
{
    public class RecipeIngredientViewModel
    {
        public int RecipeId { get; set; }
        public string RecipeName { get; set; }
        public List<IngredientViewModel> Ingredients { get; set; }
    }
}
