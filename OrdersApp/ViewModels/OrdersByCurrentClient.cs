using DBTest.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrdersApp.ViewModels
{
    public class OrdersByCurrentClient
    {
        public Client Client { get; set; }
        public List<Order> Orders { get; set; }

    }
}
