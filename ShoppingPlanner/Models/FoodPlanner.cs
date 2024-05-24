using System.Collections.Generic;

namespace ShoppingPlanner.Models
{
    public class FoodPlanner
    {
        public int Id { get; set; }
        public List<Recipe> Recipes { get; set; }
    }
}
