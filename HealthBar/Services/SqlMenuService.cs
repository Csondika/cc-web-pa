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
               (int)reader["price"],
               (int)reader["calories"],
               (bool)reader["is_vegan"],
               (bool)reader["is_active"],
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

        public List<Menu> GetSorted(bool isSlim, bool isCheap, bool isVegan)
        {
            List<Menu> menuList = new List<Menu>();

            using var command = _connection.CreateCommand();

            if (isSlim && isCheap && isVegan)
            {
                command.CommandText = "SELECT * FROM menus WHERE calories < 1500 AND price < 1000 AND is_vegan = true;";
            }
            else if (isSlim && isCheap)
            {
                command.CommandText = "SELECT * FROM menus WHERE calories < 1500 AND price < 1000;";
            }
            else if (isSlim && isVegan)
            {
                command.CommandText = "SELECT * FROM menus WHERE calories < 1500 AND is_vegan = true;";
            }
            else if (isCheap && isVegan)
            {
                command.CommandText = "SELECT * FROM menus WHERE price < 1000 AND is_vegan = true;";
            }
            else if (isSlim)
            {
                command.CommandText = "SELECT * FROM menus WHERE calories < 1500;";
            }
            else if (isCheap)
            {
                command.CommandText = "SELECT * FROM menus WHERE price < 1000;";
            }
            else if (isVegan)
            {
                command.CommandText = "SELECT * FROM menus WHERE is_vegan = true;";
            }
            else
            {
                return GetAll();
            }

            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                menuList.Add(ToMenu(reader));
            }

            return menuList;
        }

        public List<Menu> GetActive()
        {
            List<Menu> menuList = new List<Menu>();

            using var command = _connection.CreateCommand();
            command.CommandText = "SELECT * FROM menus WHERE is_active = true;";
            using var reader = command.ExecuteReader();


            while (reader.Read())
            {
                menuList.Add(ToMenu(reader));
            }

            return menuList;
        }

        public void RefreshActive(Dictionary<int, bool> activityDict)
        {
            using var command = _connection.CreateCommand();
            command.CommandText = string.Empty;

            foreach (KeyValuePair<int, bool> item in activityDict)
            {
                command.CommandText += $"UPDATE menus SET is_active = {item.Value} WHERE id = {item.Key};";
            }

            HandleExecuteNonQuery(command);
        }
    }
}

