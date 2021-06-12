using System;
using System.Collections.Generic;
using System.Text;

namespace DBTest.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public string Amount { get; set; }
        public DateTime Date { get; set; }
        public int Cost { get; set; }
        public string Description { get; set; }
        public int ClientId { get; set; } 
        public Client Client { get; set; }    
    }
}
