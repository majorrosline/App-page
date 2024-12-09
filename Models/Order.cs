using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GroceryManagement1.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public int UserId { get; set; }
        public string Status { get; set; }
        public decimal TotalAmount { get; set; }
        public int PackedBy { get; set; }
        public int OrderBy { get; set; }    
        public int DeliveredBy { get; set; } 
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public User User { get; set; }
    }
}