using EcommerceProject.Models;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;

namespace EcommerceProject.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CartItemController : ControllerBase
    {
        private readonly string connectionString = "server=127.0.0.1;uid=root;pwd=idplMAl7*;database=ecommerceproject";

        [HttpGet("{id}/{cartId}")]
        public CartItem GetCartItem(int id, int cartId)
        {
            CartItem cartItem = new CartItem();
            string sqlQueryString = $"SELECT * FROM ecommerceproject.cart_item WHERE id={id} AND cart_id={cartId}";

            MySqlConnection connection = new MySqlConnection();
            connection.ConnectionString = connectionString;
            connection.Open();

            MySqlCommand getCartItemData = new MySqlCommand(sqlQueryString, connection);
            MySqlDataReader cartIdemData = getCartItemData.ExecuteReader();

            while (cartIdemData.Read())
            {
                cartItem = new CartItem
                {
                    Id = cartIdemData.GetInt32(0),
                    CartId = cartIdemData.GetInt32(1),
                    ProductId = cartIdemData.GetInt32(2),
                    Quantity = cartIdemData.GetInt32(3)
                };
            }

            connection.Close();

            return cartItem;
        }

        [HttpPost]
        public IActionResult AddCartItem(CartItem cartItemInfo)
        {
            //CartItem cartItem = new CartItem();
            string sqlCommandString = $"INSERT INTO ecommerceproject.cart_item (cart_id, product_id, quantity) VALUES ({cartItemInfo.CartId},{cartItemInfo.ProductId},{cartItemInfo.Quantity});";

            MySqlConnection connection = new MySqlConnection();
            connection.ConnectionString = connectionString;
            connection.Open();

            MySqlCommand addCartItem = new MySqlCommand(sqlCommandString, connection);

            addCartItem.ExecuteNonQuery();

            connection.Close();

            return Ok();
        }
    }
}
