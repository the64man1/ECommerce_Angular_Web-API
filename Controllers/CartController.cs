using EcommerceProject.Models;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;

namespace EcommerceProject.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class CartController : ControllerBase
    {
        private readonly string connectionString = "server=127.0.0.1;uid=root;pwd=idplMAl7*;database=ecommerceproject";

        [HttpGet("{userId}")]
        public Cart GetCart(int userId)
        {
            Cart cart = new Cart();
            string sqlQueryString = $"SELECT * FROM ecommerceproject.cart WHERE user_id={userId}";

            MySqlConnection connection = new MySqlConnection();
            connection.ConnectionString = connectionString;
            connection.Open();

            MySqlCommand getCart = new MySqlCommand(sqlQueryString, connection);
            MySqlDataReader cartData = getCart.ExecuteReader();

            while (cartData.Read())
            {
                cart = new Cart
                {
                    Id = cartData.GetInt32(0),
                    UserId = cartData.GetInt32(1),
                    Total = cartData.GetDecimal(2),
                };
            }

            connection.Close();

            return cart;
        }

        [HttpPost]
        public IActionResult AddNewCart(User user)
        {
            Cart cart = new Cart();
            string sqlCommandString = $"INSERT INTO ecommerceproject.cart (user_id, total) VALUES ({user.Id}, 0.00);";
            MySqlConnection connection = new MySqlConnection();
            connection.ConnectionString = connectionString;
            connection.Open();

            MySqlCommand addNewCart = new MySqlCommand(sqlCommandString, connection);

            addNewCart.ExecuteNonQuery();

            connection.Close();

            return Ok();
        }
    }
}
