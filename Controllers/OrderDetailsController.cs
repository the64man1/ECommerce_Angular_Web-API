using EcommerceProject.Models;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;

namespace EcommerceProject.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderDetailsController : ControllerBase
    {
        private readonly string connectionString = "server=127.0.0.1;uid=root;pwd=idplMAl7*;database=ecommerceproject";

        [HttpGet("{userId}/{id}")]
        public OrderDetails GetOrderDetails(int userId, int id)
        {
            OrderDetails orderDetails = new OrderDetails();
            string sqlQueryString = $"SELECT * FROM ecommerceproject.order_details WHERE id={id} AND user_id={userId}";

            MySqlConnection connection = new MySqlConnection();
            connection.ConnectionString = connectionString;
            connection.Open();

            MySqlCommand getOrderDetails = new MySqlCommand(sqlQueryString, connection);
            MySqlDataReader orderDetailsData = getOrderDetails.ExecuteReader();

            while (orderDetailsData.Read())
            {
                orderDetails = new OrderDetails
                {
                    Id = id,
                    UserId = userId,
                    Total = orderDetailsData.GetDecimal(2),
                    CreatedAt = orderDetailsData.GetDateTime(3)
                };
            }

            return orderDetails;
        }

        [HttpPost("{userId}")]
        public IActionResult AddOrder(int userId)
        {
            OrderDetails orderDetails = new OrderDetails();
            string sqlCommandString = $"INSERT INTO ecommerceproject.order_details (user_id, total) VALUES ({userId},0.00)";

            MySqlConnection connection = new MySqlConnection();
            connection.ConnectionString = connectionString;
            connection.Open();

            MySqlCommand addOrder = new MySqlCommand(sqlCommandString, connection);
            addOrder.ExecuteNonQuery();
            connection.Close();

            return Ok();
        }
    }
}
