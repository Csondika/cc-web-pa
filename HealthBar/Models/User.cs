using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Schedule_master_2000.Models
{
    public class User
    {
        public int ID { get; private set; }
        public string Username { get; private set; }
        public string Password { get; private set; }

        public string Email { get; private set; }

        public string Role { get; private set; }
        public string City { get; private set; }
        public string Address { get; private set; }
        public int? PostalCode { get; private set; }

        // User for registration
        public User(string username, string password, string email, string role = "user", int? id = null)
        {
            ID = id ?? default;
            Username = username;
            Password = password;
            Email = email;
            Role = role;
        }

        // User for existing DB queries
        public User(int id, string username, string password, string email, string role,
            string city, string address, int postalCode)
        {
            ID = id;
            Username = username;
            Password = password;
            Email = email;
            Role = role;
            City = city;
            Address = address;
            PostalCode = postalCode;
        }

        public void SetDeliveryAddress(string city, string address, int postalCode)
        {
            City = city;
            Address = address;
            PostalCode = postalCode;
        }
    }
}
