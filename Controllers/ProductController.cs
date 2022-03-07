using EcommerceProject.Models;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using System.Collections.Generic;

namespace EcommerceProject.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly string connectionString = "server=127.0.0.1;uid=root;pwd=idplMAl7*;database=ecommerceproject";

        [HttpGet]
        public IEnumerable<Product> GetProducts()
        {
            var listOfProducts = new List<Product>();
            string query = "SELECT * FROM ecommerceproject.product";
            MySqlConnection connection = new MySqlConnection();

            connection.ConnectionString = connectionString;
            connection.Open();
            MySqlCommand getData = new MySqlCommand(query, connection);
            MySqlDataReader getDataResult = getData.ExecuteReader();

            while (getDataResult.Read())
            {
                listOfProducts.Add(new Product
                {
                    Id = getDataResult.GetInt32(0),
                    Name = getDataResult.GetString(1),
                    Description = getDataResult.GetString(2),
                    Category_Id = getDataResult.GetInt32(3),
                    Price = getDataResult.GetDecimal(4),
                });
            }

            getDataResult.Close();

            var listArr = listOfProducts.ToArray();

            return listArr;
        }

        [HttpGet("{id}")]
        public Product GetProductById(int id)
        {
            var product = new Product();
            string query = $"SELECT * FROM ecommerceproject.product WHERE id={id}";
            MySqlConnection connection = new MySqlConnection();

            connection.ConnectionString = connectionString;
            connection.Open();
            MySqlCommand getData = new MySqlCommand(query, connection);
            MySqlDataReader getDataResult = getData.ExecuteReader();

            while (getDataResult.Read())
            {
                product = new Product
                {
                    Id = getDataResult.GetInt32(0),
                    Name = getDataResult.GetString(1),
                    Description = getDataResult.GetString(2),
                    Category_Id = getDataResult.GetInt32(3),
                    Price = getDataResult.GetDecimal(4),
                };
            }

            getDataResult.Close();

            return product;
        }

        [TokenAuthenticationFilter]
        [HttpPost]
        public IActionResult AddProduct(Product product)
        {
            string sqlCommandString = $"INSERT INTO ecommerceproject.product (name, description, category_id, price) VALUES (\"{product.Name}\", \"{product.Description}\", 1, {product.Price});";
            MySqlConnection connection = new MySqlConnection();
            connection.ConnectionString = connectionString;
            connection.Open();

            MySqlCommand addProduct = new MySqlCommand(sqlCommandString, connection);

            addProduct.ExecuteNonQuery();

            connection.Close();

            return Ok();
        }

        [TokenAuthenticationFilter]
        [HttpPut]
        public IActionResult UpdateProduct(Product product)
        {
            string sqlCommandString = $"UPDATE ecommerceproject.product SET description=\"{product.Description}\", price={product.Price} WHERE id={product.Id};";
            MySqlConnection connection = new MySqlConnection();
            connection.ConnectionString = connectionString;
            connection.Open();

            MySqlCommand updateProduct = new MySqlCommand(sqlCommandString, connection);

            updateProduct.ExecuteNonQuery();

            connection.Close();

            return Ok();
        }
    }
}
