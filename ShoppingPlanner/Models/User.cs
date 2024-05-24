using System.Collections.Generic;

namespace ShoppingPlanner.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public List<Recipe> Recipes { get; set; }
    }
}
