using DBTest.Entities;
using DBTest.EntityFramework;
using DBTest.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DBTest.Repositories
{
    public class OrderRepositories : IRepository<Order>
    {
        private readonly Context DBContext;

        public OrderRepositories(Context context)
        {
            this.DBContext = context;
        }

        public void Create(Order order)
        {
            DBContext.Orders.Add(order);
        }

        public void Create(Order order, Client client)
        {
            DBContext.Orders.Add(order);
        }

        public void Delete(int id)
        {
            Order order = DBContext.Orders.FirstOrDefault(n => n.Id == id);
            if (order != null)
                DBContext.Orders.Remove(order);
        }

        public Order Read(int id)
        {
            return DBContext.Orders.FirstOrDefault(n => n.Id == id);
        }

        public IEnumerable<Order> ReadAll()
        {
            return DBContext.Orders;
        }

        public void Update(Order order)
        {
            var previousOrder = DBContext.Orders.FirstOrDefault(n => n.Id == order.Id);

            if (previousOrder != null)
            {
                DBContext.Orders.Remove(previousOrder);
                Order newOrder = new Order()
                {
                    Amount = order.Amount,
                    Date = order.Date,
                    Cost= order.Cost,
                    Description = order.Description,                  
                };

                DBContext.Orders.Add(newOrder);
            }
        }
    }
}
