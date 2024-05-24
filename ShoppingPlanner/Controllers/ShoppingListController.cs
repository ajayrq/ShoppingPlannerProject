using Microsoft.AspNetCore.Mvc;
using ShoppingPlanner.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShoppingPlanner.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ShoppingListController : ControllerBase
    {
        private readonly ShoppingListService _shoppingListService;

        public ShoppingListController(ShoppingListService shoppingListService)
        {
            _shoppingListService = shoppingListService;
        }

        [HttpGet("{foodPlannerId}")]
        public async Task<ActionResult<Dictionary<string, int>>> GetShoppingList(int foodPlannerId)
        {
            var shoppingList = await _shoppingListService.GenerateShoppingList(foodPlannerId);
            return Ok(shoppingList);
        }
    }
}
