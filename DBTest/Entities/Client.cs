using System;
using System.Collections.Generic;
using System.Text;

namespace DBTest.Entities
{
    public class Client
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Adress { get; set; }
        public List<Order> Orders { get; set; }

    }
}
