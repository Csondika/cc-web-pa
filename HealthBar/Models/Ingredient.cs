using HealthBar.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HealthBar.Models
{
    public class Ingredient
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IngredientType Type { get; set; }
        public int Calories { get; set; }
        public int Unit { get; set; }
        public int Price { get; set; }
        public bool IsVegan { get; set; }
    }
}
