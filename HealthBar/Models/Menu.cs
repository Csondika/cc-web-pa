using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HealthBar.Models
{
    public class Menu
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public int PriceOff { get; private set; }
        public int UserId { get; private set; }
        public int Calories { get; set; }
        public int Price { get; set; }
        public bool IsVegan { get; set; }

        public Menu(int id, string name, int priceOff, int price, int calories, bool isVegan, int userId)
        {
            Id = id;
            Name = name;
            PriceOff = priceOff;
            Price = price;
            Calories = calories;
            IsVegan = isVegan;
            UserId = userId;
        }
    }
}
