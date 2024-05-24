using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoppingPlanner.Data;
using ShoppingPlanner.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShoppingPlanner.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RecipesController : ControllerBase
    {
        private readonly ShoppingPlannerContext _context;

        public RecipesController(ShoppingPlannerContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Recipe>>> GetRecipes()
        {
            return await _context.Recipes.Include(r => r.RecipeIngredients).ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<Recipe>> AddRecipe(Recipe recipe)
        {
            _context.Recipes.Add(recipe);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetRecipes), new { id = recipe.Id }, recipe);
        }
    }
}
