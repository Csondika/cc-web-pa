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
    public class SqlUserService: SqlBaseService, IUserService
    {
        private static User ToExistingUser(IDataReader reader)
        {
            return new User
            (
               (int)reader["id"],
               (string)reader["name"],
               (string)reader["password"],
               (string)reader["email"],
               (string)reader["role"],
               (string)reader["city"],
               (string)reader["address"],
               (int)reader["postal_code"]
            );
        }

        private readonly IDbConnection _connection;

        public SqlUserService(IDbConnection connection)
        {
            _connection = connection;
        }

        public User GetOne(int userid)
        {
            using var command = _connection.CreateCommand();
            command.CommandText = "SELECT * FROM users WHERE userid = @id";

            var param = command.CreateParameter();
            param.ParameterName = "id";
            param.Value = userid;
            command.Parameters.Add(param);
            using var reader = command.ExecuteReader();
            reader.Read();
            return ToExistingUser(reader);
        }

        public User GetOne(string email)
        {
            using var command = _connection.CreateCommand();
            command.CommandText = $"SELECT * FROM users WHERE email = '{email}'";

            using var reader = command.ExecuteReader();
            reader.Read();
            return ToExistingUser(reader);
        }

        public void DeleteUser(int id)
        {
            using var command = _connection.CreateCommand();
            command.CommandText = "Delete * From users Where id = @id";

            var param = command.CreateParameter();
            param.ParameterName = "id";
            param.Value = id;
            command.Parameters.Add(param);
            using var reader = command.ExecuteReader();
        }

        public bool ValidateUser(string email, string password)
        {
            using var command = _connection.CreateCommand();

            var emailParam = command.CreateParameter();
            emailParam.ParameterName = "email";
            emailParam.Value = email;

            var passwordParam = command.CreateParameter();
            passwordParam.ParameterName = "password";
            passwordParam.Value = Utility.Hash(password);
            command.CommandText = $"SELECT * FROM users WHERE email = @email AND password = @password";
            command.Parameters.Add(emailParam);
            command.Parameters.Add(passwordParam);

            using var reader = command.ExecuteReader();
            
            if (reader.Read())
            {
                return true;
            }

            return false;
        }

        public void InsertUser(string userName, string password, string email, string role)
        {
            using var command = _connection.CreateCommand();

            var userNameParam = command.CreateParameter();
            userNameParam.ParameterName = "name";
            userNameParam.Value = userName;
            var passwordParam = command.CreateParameter();
            passwordParam.ParameterName = "password";
            passwordParam.Value = Utility.Hash(password);
            var emailParam = command.CreateParameter();
            emailParam.ParameterName = "email";
            emailParam.Value = email;
            var roleParam = command.CreateParameter();
            roleParam.ParameterName = "role";
            roleParam.Value = role;

            command.CommandText = $"INSERT INTO users(name, password, email, role) VALUES (@name, @password, @email, @role)";
            command.Parameters.Add(userNameParam);
            command.Parameters.Add(passwordParam);
            command.Parameters.Add(emailParam);
            command.Parameters.Add(roleParam);

            HandleExecuteNonQuery(command);
        }

        public bool CheckIfUserExists(string email)
        {
            using var command = _connection.CreateCommand();
            command.CommandText = $"SELECT true FROM users WHERE email = @email";
            var param = command.CreateParameter();
            param.ParameterName = "email";
            param.Value = email;
            bool UserExist = Convert.ToBoolean(command.ExecuteScalar());

            return UserExist;
        }

        public bool CheckIfUserHasAddress(string email)
        {
            User userToCheck = GetOne(email);

            return userToCheck.City != null && userToCheck.Address != null && userToCheck.PostalCode != null;
        }

        public void UpdateUser(int id, string username, string password, string email, int postalCode, string city, string address)
        {
            using var command = _connection.CreateCommand();
            var idParam = command.CreateParameter();
            idParam.ParameterName = "id";
            idParam.Value = id;
            var usernameParam = command.CreateParameter();
            usernameParam.ParameterName = "name";
            usernameParam.Value = username;
            var emailParam = command.CreateParameter();
            emailParam.ParameterName = "email";
            emailParam.Value = email;
            var postalCodeParam = command.CreateParameter();
            postalCodeParam.ParameterName = "postal_code";
            postalCodeParam.Value = postalCode;
            var cityParam = command.CreateParameter();
            cityParam.ParameterName = "city";
            cityParam.Value = city;
            var addressParam = command.CreateParameter();
            addressParam.ParameterName = "address";
            addressParam.Value = address;

            if (password == null)
            {
                command.CommandText = $"UPDATE users SET name = @name, email = @email, postal_code = @postal_code, city = @city, address = @address WHERE id = @id";
                command.Parameters.Add(idParam);
                command.Parameters.Add(usernameParam);
                command.Parameters.Add(emailParam);
                command.Parameters.Add(postalCodeParam);
                command.Parameters.Add(cityParam);
                command.Parameters.Add(addressParam);
            }
            else
            {
                var passwordParam = command.CreateParameter();
                passwordParam.ParameterName = "password";
                passwordParam.Value = Utility.Hash(password);

                command.CommandText = $"UPDATE users SET name = @name, password = @password, email = @email, postal_code = @postal_code, city = @city, address = @address WHERE id LIKE @id";
                command.Parameters.Add(idParam);
                command.Parameters.Add(usernameParam);
                command.Parameters.Add(passwordParam);
                command.Parameters.Add(emailParam);
                command.Parameters.Add(postalCodeParam);
                command.Parameters.Add(cityParam);
                command.Parameters.Add(addressParam);
            }

            HandleExecuteNonQuery(command);
        }
    }
}

