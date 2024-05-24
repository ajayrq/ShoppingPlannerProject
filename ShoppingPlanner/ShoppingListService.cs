using Microsoft.EntityFrameworkCore;
using ShoppingPlanner.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingPlanner.Services
{
    public class ShoppingListService
    {
        private readonly ShoppingPlannerContext _context;

        public ShoppingListService(ShoppingPlannerContext context)
        {
            _context = context;
        }

        public async Task<Dictionary<string, int>> GenerateShoppingList(int foodPlannerId)
        {
            var foodPlanner = await _context.FoodPlanners
                .Include(fp => fp.Recipes)
                .ThenInclude(r => r.RecipeIngredients)
                .ThenInclude(ri => ri.Ingredient)
                .FirstOrDefaultAsync(fp => fp.Id == foodPlannerId);

            var shoppingList = new Dictionary<string, int>();

            foreach (var recipe in foodPlanner.Recipes)
            {
                foreach (var ri in recipe.RecipeIngredients)
                {
                    if (shoppingList.ContainsKey(ri.Ingredient.Name))
                    {
                        shoppingList[ri.Ingredient.Name] += ri.Amount;
                    }
                    else
                    {
                        shoppingList[ri.Ingredient.Name] = ri.Amount;
                    }
                }
            }

            return shoppingList;
        }
    }
}
