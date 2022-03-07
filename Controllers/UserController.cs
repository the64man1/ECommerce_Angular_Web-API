using EcommerceProject.Models;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using System;

namespace EcommerceProject.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        string connectionString = "server=127.0.0.1;uid=root;pwd=idplMAl7*;database=ecommerceproject";

        [HttpGet("{id}")]
        public User GetUserById(int id)
        {
            User user = new User();
            string sqlQueryString = $"SELECT * FROM ecommerceproject.user WHERE id={id}";
            MySqlConnection connection = new MySqlConnection();
            connection.ConnectionString = connectionString;
            connection.Open();

            MySqlCommand getUser = new MySqlCommand(sqlQueryString, connection);
            MySqlDataReader userData = getUser.ExecuteReader();

            while (userData.Read())
            {
                user = new User
                {
                    Id = id,
                    Username = userData.GetString(1),
                    FirstName = userData.GetString(3),
                    LastName = userData.GetString(4),
                    Address = userData.GetString(5),
                    PhoneNumber = userData.GetInt32(6)
                };
            }

            connection.Close();

            return user;
        }

        [HttpPost]
        public IActionResult PostNewUser(User user)
        {
            string sqlCommandString = $"SELECT * FROM ecommerceproject.user; INSERT INTO user (username, password, first_name, last_name, address, phone_number) VALUES (\"{user.Username}\", \"{user.Password}\", \"{user.FirstName}\", \"{user.LastName}\", \"{user.Address}\", {user.PhoneNumber});";
            MySqlConnection connection = new MySqlConnection();
            connection.ConnectionString = connectionString;
            connection.Open();

            MySqlCommand addUser = new MySqlCommand(sqlCommandString, connection);

            try
            {
                addUser.ExecuteNonQuery();
            }
            catch (Exception error)
            {
                throw error;
            }

            connection.Close();

            return Ok();
        }
    }
}
