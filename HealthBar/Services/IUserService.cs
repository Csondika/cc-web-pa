using HealthBar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HealthBar.Services
{
    public interface IUserService
    {
        User GetOne(int userid);
        User GetOne(string email);
        void InsertUser(string userName, string password, string email, string role = "user");
        void DeleteUser(int id);
        void UpdateUser(int id, string username, string password, string email, int postalCode, string city, string address);
        bool CheckIfUserExists(string email);
        bool CheckIfUserHasAddress(string email);
        bool ValidateUser(string email, string password);
    }
}
