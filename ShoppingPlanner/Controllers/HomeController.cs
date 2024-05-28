using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoppingPlanner.Data;
using ShoppingPlanner.Models;
using ShoppingPlanner.ViewModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShoppingPlanner.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HomeController : ControllerBase
    {
        private readonly ShoppingPlannerContext _context;

        public HomeController(ShoppingPlannerContext context)
        {
            _context = context;
        }
        [HttpGet("GetIngredients")]
        public async Task<ActionResult<IEnumerable<Ingredient>>> GetIngredients()
        {
            return await _context.Ingredients.ToListAsync();
        }

        [HttpPost("AddIngredient")]
        public async Task<ActionResult<Ingredient>> AddIngredient(Ingredient ingredient)
        {
            _context.Ingredients.Add(ingredient);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetIngredients), new { id = ingredient.Id }, ingredient);
        }
        [HttpGet("GetRecipes")]
        public async Task<ActionResult<IEnumerable<Recipe>>> GetRecipes()
        {
            return await _context.Recipes.ToListAsync();
        }
        [HttpGet("GetShoppingList")]
        public async Task<ActionResult<IEnumerable<ShoppingListViewModel>>> GetShoppingList()
        {
            var shoppingList = await _context.RecipeIngredients
            .GroupBy(i => new { i.IngredientId, i.Ingredient.Name })
            .Select(g => new ShoppingListViewModel
            {
                IngredientId = g.Key.IngredientId,
                IngredientName = g.Key.Name,
                Amount = g.Sum(i => i.Amount)
            })
            .ToListAsync();
            return shoppingList;
        }

        [HttpPost("AddRecipe")]
        public async Task<ActionResult<int>> AddRecipe(RecipeIngredientViewModel recipeIngredient)
        {
            if (recipeIngredient.RecipeId == 0)
            {
                Recipe newR = new Recipe() { Name = recipeIngredient.RecipeName };
                var recipe = _context.Recipes.Add(newR);
                await _context.SaveChangesAsync();
                foreach (var item in recipeIngredient.Ingredients)
                {
                    _context.RecipeIngredients.Add(new RecipeIngredient() { RecipeId = newR.Id, IngredientId = item.IngredientId, Amount = item.Amount });
                    await _context.SaveChangesAsync();
                }
            }
            else
            {
                var rece = _context.Recipes.First(i => i.Id == recipeIngredient.RecipeId);
                foreach (var item in recipeIngredient.Ingredients)
                {
                    var rI = _context.RecipeIngredients.FirstOrDefault(i => i.IngredientId == item.IngredientId);
                    if (rI != null)
                    {
                        rI.Amount = item.Amount;
                    }
                    else
                    {
                        _context.RecipeIngredients.Add(new RecipeIngredient() { RecipeId = rece.Id, IngredientId = item.IngredientId, Amount = item.Amount });
                    }
                    await _context.SaveChangesAsync();
                }
            }


            return await _context.SaveChangesAsync();
        }
    }
}
