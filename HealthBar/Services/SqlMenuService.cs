using Npgsql;
using HealthBar.Domain;
using HealthBar.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace HealthBar.Services
{
    public class SqlMenuService: SqlBaseService, IMenuService
    {
        private static Menu ToMenu(IDataReader reader)
        {
            return new Menu
            (
               (int)reader["id"],
               (string)reader["name"],
               (int)reader["price_off"],
               (int)reader["user_id"]
            );
        }

        private readonly IDbConnection _connection;

        public SqlMenuService(IDbConnection connection)
        {
            _connection = connection;
        }

        public List<Menu> GetAll()
        {
            List<Menu> menuList = new List<Menu>();

            using var command = _connection.CreateCommand();
            command.CommandText = "SELECT * FROM menus;";
            using var reader = command.ExecuteReader();


            while (reader.Read())
            {
                menuList.Add(ToMenu(reader));
            }

            return menuList;
        }

        public void SetMenuAttributes(Menu menu)
        {
            float calories = 0;
            float price = 0;
            List<bool> isVegan = new List<bool>();

            using var command = _connection.CreateCommand();
            var idParam = command.CreateParameter();
            idParam.ParameterName = "menuId";
            idParam.Value = menu.Id;

            command.CommandText = "SELECT i.calories, i.unit, i.price, i.is_vegan FROM menus m " +
                                  "JOIN menus_ingredients mi ON m.id = mi.menu_id " +
                                  "JOIN ingredients i ON mi.ingredient_id = i.id " +
                                  "WHERE m.id = @menuId;";
            command.Parameters.Add(idParam);

            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                calories += (float)reader[0] * ((float)reader[1] / 100f);
                price += (float)reader[2];
                isVegan.Add((bool)reader[3]);
            }

            menu.Calories = (int)calories;
            float calc = price * ((100 - menu.PriceOff) / 100f);
            menu.Price = (int)calc;
            
            if (isVegan.Any(b => b == false))
            {
                menu.IsVegan = false;
            }
            else
            {
                menu.IsVegan = true;
            }
        }
    }
}

