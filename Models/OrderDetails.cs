using System;

namespace EcommerceProject.Models
{
    public class OrderDetails
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public decimal Total { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
