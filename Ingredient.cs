namespace RecipeBuilderApp
{
    public class Ingredient
    {
        public string Name { get; set; }
        public double Quantity { get; set; }
        public string Measurement { get; set; }
        public string FoodGroup { get; set; }
        public double Calories { get; set; }

        public override string ToString()
        {
            return $"{Name} - {Quantity} {Measurement}";
        }
    }
}