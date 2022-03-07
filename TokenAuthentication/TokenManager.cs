using EcommerceProject.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EcommerceProject.TokenAuthentication
{
    public class TokenManager : ITokenManager
    {
        private readonly string connectionString = "server=127.0.0.1;uid=root;pwd=idplMAl7*;database=ecommerceproject";
        private List<Token> listTokens;

        public TokenManager()
        {
            listTokens = new List<Token>();
        }
        public bool Authenticate(string userName, string password)
        {
            var user = new User();
            string sqlQueryString = $"SELECT * FROM ecommerceproject.user WHERE username=\"{userName}\";";

            MySqlConnection conn = new MySqlConnection();
            conn.ConnectionString = connectionString;
            conn.Open();

            MySqlCommand getUser = new MySqlCommand(sqlQueryString, conn);
            MySqlDataReader getUserData = getUser.ExecuteReader();

            while (getUserData.Read())
            {
                user = new User
                {
                    Username = getUserData.GetString(1),
                    Password = getUserData.GetString(2)
                };
            }

            if (!string.IsNullOrEmpty(userName) &&
                !string.IsNullOrEmpty(password) &&
                userName.ToLower() == user.Username.ToLower() &&
                password == user.Password)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public Token NewToken()
        {
            var token = new Token
            {
                Value = Guid.NewGuid().ToString(),
                ExpirationDateTime = DateTime.Now.AddMinutes(60)
            };

            string sqlCommandString = $"INSERT INTO ecommerceproject.tokens (value, expiration_datetime) VALUES (\"{token.Value}\",\"{token.ExpirationDateTime.ToString("yyyy-MM-dd HH:mm:ss")}\")";

            MySqlConnection connection = new MySqlConnection();
            connection.ConnectionString = connectionString;
            connection.Open();

            MySqlCommand addToken = new MySqlCommand(sqlCommandString, connection);
            addToken.ExecuteNonQuery();

            listTokens.Add(token);
            return token;
        }

        public bool VerifyToken(string userToken)
        {
            var token = new Token();
            string sqlQueryString = $"SELECT * FROM ecommerceproject.tokens WHERE value=\"{userToken}\";";

            MySqlConnection conn = new MySqlConnection();
            conn.ConnectionString = connectionString;
            conn.Open();

            MySqlCommand getTokens = new MySqlCommand(sqlQueryString, conn);
            MySqlDataReader getTokensData = getTokens.ExecuteReader();
           
            while (getTokensData.Read())
            {
                token = new Token
                {
                    Value = getTokensData.GetString(1),
                    ExpirationDateTime = getTokensData.GetDateTime(2),
                };
            }

            if (userToken == token.Value && token.ExpirationDateTime > DateTime.Now)
            {
                return true;
            }

            return false;
        }
    }
}
