using System.Collections.Generic;

namespace RecipeBuilderApp
{
    public class RecipeManager
    {
        public List<RecipeBook> Recipes { get; internal set; }

        public RecipeManager()
        {
            Recipes = new List<RecipeBook>();
        }

        public void AddRecipe(RecipeBook recipe)
        {
            Recipes.Add(recipe);
        }

        public void DisplayRecipes()
        {
            // Display all recipes
        }

        public void ScaleRecipe(double factor)
        {
            foreach (var recipe in Recipes)
            {
                recipe.ScaleIngredients(factor);
            }
        }

        public void ResetQuantities()
        {
            // Reset quantities of all recipes
        }

        public void ClearRecipe()
        {
            Recipes.Clear();
        }
    }
}