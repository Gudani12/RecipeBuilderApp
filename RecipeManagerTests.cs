using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RecipeBuilderApp
{
    [TestClass]
    public class RecipeManagerTests
    {
        [TestMethod]
        public void TestEnterRecipeDetails_CaloriesBelowThreshold()
        {
            // Arrange
            RecipeManager recipeManager = new RecipeManager();
            int initialRecipeCount = recipeManager.Recipes.Count;

            // Act
            RecipeBook recipe = new RecipeBook
            {
                Name = "Test Recipe"
            };
            recipe.AddIngredient(new Ingredient { Name = "Ingredient 1", Quantity = 1, Measurement = "cup", FoodGroup = "Grains", Calories = 100 });
            recipeManager.AddRecipe(recipe);

            // Assert
            Assert.AreEqual(initialRecipeCount + 1, recipeManager.Recipes.Count, "Recipe not added correctly.");
        } 
    }
}