using DBTest.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrdersApp.ViewModels
{
    public class OrderViewModel
    {
            public IEnumerable<Client> Clients { get; set; }
            public IEnumerable<Order> Orders { get; set; }     

    }
}
