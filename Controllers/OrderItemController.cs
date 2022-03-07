using EcommerceProject.Models;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;

namespace EcommerceProject.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderItemController : ControllerBase
    {
        private readonly string connectionString = "server=127.0.0.1;uid=root;pwd=idplMAl7*;database=ecommerceproject";

        [HttpGet("{orderId}/{id}")]
        public OrderItem GetOrderItem(int id, int orderId)
        {
            OrderItem orderItem = new OrderItem();
            string sqlQueryString = $"SELECT * FROM ecommerceproject.order_item WHERE id={id} AND order_id={orderId}";

            MySqlConnection connection = new MySqlConnection();
            connection.ConnectionString = connectionString;
            connection.Open();

            MySqlCommand getOrderData = new MySqlCommand(sqlQueryString, connection);
            MySqlDataReader orderData = getOrderData.ExecuteReader();

            while (orderData.Read())
            {
                orderItem = new OrderItem
                {
                    Id = id,
                    OrderId = orderId,
                    ProductId = orderData.GetInt32(2),
                    Quantity = orderData.GetInt32(3),
                    CreatedAt = orderData.GetDateTime(4),
                };
            }

            connection.Close();

            return orderItem;
        }

        [HttpPost]
        public IActionResult AddOrderItem(OrderItem orderItem)
        {
            MySqlConnection connection = new MySqlConnection();
            string sqlCommandString = $"INSERT INTO ecommerceproject.order_item (order_id, product_id, quantity) VALUES ({orderItem.OrderId},{orderItem.ProductId},{orderItem.Quantity})";

            MySqlCommand addOrderItem = new MySqlCommand(sqlCommandString, connection);
            connection.ConnectionString = connectionString;
            connection.Open();

            addOrderItem.ExecuteNonQuery();

            connection.Close();

            return Ok();
        }
    }
}
