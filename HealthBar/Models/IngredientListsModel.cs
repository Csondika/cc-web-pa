using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HealthBar.Models
{
    public class IngredientListsModel
    {
        public List<Ingredient> Buns { get; set; }
        public List<Ingredient> Meats { get; set; }
        public List<Ingredient> Vegetables { get; set; }
        public List<Ingredient> Sauces { get; set; }
        public List<Ingredient> SideDishes { get; set; }
        public List<Ingredient> SoftDrinks { get; set; }

        public IngredientListsModel()
        {
            Buns = new List<Ingredient>();
            Meats = new List<Ingredient>();
            Vegetables = new List<Ingredient>();
            Sauces = new List<Ingredient>();
            SideDishes = new List<Ingredient>();
            SoftDrinks = new List<Ingredient>();
        }
    }
}
