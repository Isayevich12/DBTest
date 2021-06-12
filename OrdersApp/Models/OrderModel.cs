using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrdersApp.Models
{
    public class OrderModel
    {
        public int Id { get; set; }
        public string Amount { get; set; }
        public DateTime Date { get; set; }
        public int Cost { get; set; }
        public string Description { get; set; }
        public int ClientId { get; set; } 
        public ClientModel Client { get; set; }    
    }
}
