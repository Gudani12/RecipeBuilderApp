using LiveCharts.Wpf;
using LiveCharts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace RecipeBuilderApp
{
    public partial class MainWindow : Window 
    {
        private RecipeManager recipeManager; 

        public MainWindow()
        {
            InitializeComponent();
            recipeManager = new RecipeManager();
        }

        private void SaveRecipeButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string recipeName = RecipeNameTextBox.Text;
                string ingredientName = IngredientNameTextBox.Text;
                string ingredientMeasurement = IngredientMeasurementTextBox.Text;

                if (!double.TryParse(IngredientQuantityTextBox.Text, out double ingredientQuantity))
                {
                    MessageBox.Show("Please enter a valid ingredient quantity.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return; 
                }

                if (!(FoodGroupComboBox.SelectedItem is ComboBoxItem selectedFoodGroup))
                {
                    MessageBox.Show("Please select a food group.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                string foodGroup = selectedFoodGroup.Content.ToString();
                 
                if (!double.TryParse(CaloriesTextBox.Text, out double calories))
                {
                    MessageBox.Show("Please enter a valid calorie amount.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return; 
                }

                string steps = RecipeStepsTextBox.Text; 

                var ingredient = new Ingredient
                {
                    Name = ingredientName,
                    Measurement = ingredientMeasurement,
                    Quantity = ingredientQuantity,
                    FoodGroup = foodGroup, 
                    Calories = calories
                };

                var recipe = new RecipeBook
                {
                    Name = recipeName,
                    Steps = steps
                };

                recipe.Ingredients.Add(ingredient);
                recipeManager.AddRecipe(recipe);

                RecipesListBox.Items.Add(recipe.Name);

                if (calories > 300)
                {
                    MessageBox.Show("This recipe exceeds 300 calories!", "Calorie Alert", MessageBoxButton.OK, MessageBoxImage.Warning);
                }

                ClearInputFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ClearInputFields()
        {
            RecipeNameTextBox.Clear();
            IngredientNameTextBox.Clear();
            IngredientMeasurementTextBox.Clear();
            IngredientQuantityTextBox.Clear();
            FoodGroupComboBox.SelectedIndex = -1;
            CaloriesTextBox.Clear();
            RecipeStepsTextBox.Clear();
        }

        private void DisplayButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (RecipesListBox.SelectedItem != null)
                {
                    var selectedRecipe = recipeManager.Recipes.FirstOrDefault(r => r.Name == RecipesListBox.SelectedItem.ToString());
                    if (selectedRecipe != null)
                    {
                        MessageBox.Show($"Recipe: {selectedRecipe.Name}\nIngredients: {string.Join(", ", selectedRecipe.Ingredients.Select(i => i.Name))}\nSteps: {selectedRecipe.Steps}", "Recipe Details", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Please select a recipe.", "Selection Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ScaleButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Button button = (Button)sender;
                if (double.TryParse(button.Tag.ToString(), out double scale))
                {
                    foreach (var recipe in recipeManager.Recipes)
                    {
                        recipe.ScaleIngredients(scale);
                    }

                    MessageBox.Show("Recipes scaled successfully.", "Scale", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Invalid scale value.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void GeneratePieChartButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (RecipesListBox.SelectedItems.Count == 0)
                {
                    MessageBox.Show("Please select at least one recipe.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                var selectedRecipes = RecipesListBox.SelectedItems.Cast<string>()
                    .Select(recipeName => recipeManager.Recipes.FirstOrDefault(r => r.Name == recipeName))
                    .Where(recipe => recipe != null)
                    .ToList();

                var foodGroupTotals = new Dictionary<string, double>();

                foreach (var recipe in selectedRecipes)
                {
                    foreach (var ingredient in recipe.Ingredients)
                    {
                        if (foodGroupTotals.ContainsKey(ingredient.FoodGroup))
                        {
                            foodGroupTotals[ingredient.FoodGroup] += ingredient.Quantity;
                        }
                        else
                        {
                            foodGroupTotals[ingredient.FoodGroup] = ingredient.Quantity;
                        }
                    }
                }

                PieChart.Series.Clear();

                foreach (var foodGroup in foodGroupTotals)
                {
                    PieChart.Series.Add(new PieSeries
                    {
                        Title = foodGroup.Key,
                        Values = new ChartValues<double> { foodGroup.Value },
                        DataLabels = true
                    });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
