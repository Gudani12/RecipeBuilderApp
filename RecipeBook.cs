using RecipeBuilderApp;
using System.Collections.Generic;

namespace RecipeBuilderApp
{
    public class RecipeBook
    {
        public string Name { get; set; }
        public List<Ingredient> Ingredients { get; set; }
        public string Steps { get; set; }

        public RecipeBook()
        {
            Ingredients = new List<Ingredient>();
        }

        public void AddIngredient(Ingredient ingredient)
        {
            Ingredients.Add(ingredient);
        }

        public void ScaleIngredients(double factor)
        {
            foreach (var ingredient in Ingredients)
            {
                ingredient.Quantity *= factor;
            }
        }

        public override string ToString()
        {
            return $"Recipe: {Name}";
        }
    }
}