using Microsoft.EntityFrameworkCore;
using ShoppingPlanner.Models;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace ShoppingPlanner.Data
{
    public class ShoppingPlannerContext : DbContext
    {
        public ShoppingPlannerContext(DbContextOptions<ShoppingPlannerContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<FoodPlanner> FoodPlanners { get; set; }
        public DbSet<RecipeIngredient> RecipeIngredients { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RecipeIngredient>()
                .HasKey(ri => new { ri.RecipeId, ri.IngredientId });
            modelBuilder.Entity<RecipeIngredient>()
                .HasOne(ri => ri.Recipe)
                .WithMany(r => r.RecipeIngredients)
                .HasForeignKey(ri => ri.RecipeId);
            modelBuilder.Entity<RecipeIngredient>()
                .HasOne(ri => ri.Ingredient)
                .WithMany()
                .HasForeignKey(ri => ri.IngredientId);
        }
    }
}
