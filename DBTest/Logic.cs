using System;
using System.Collections.Generic;
using System.Text;
using DBTest;
using DBTest.Entities;
using DBTest.Interfaces;
using System.Linq;

namespace DBTest
{
    public class Logic : IDisposable
    {
        private UnitOFWork DB {get;}

        public Logic()
        {
            this.DB = new UnitOFWork();
        }

        public void AddClient(Client element)
        {
            var clients = DB.Clients.ReadAll();
            var currentClient = clients.Where(n => n.Name == element.Name && n.Adress == element.Adress).FirstOrDefault();
            
            if( currentClient != null)
            {
                throw new ArgumentException($"Клиент {element.Name} {element.Adress} уже существует! ");
            }
             else
            {
                DB.Clients.Create(element);
                DB.Save();
            }       
        }

        public void AddOrderByClient(Client element, Order order)
        {
            var clients = DB.Clients.ReadAll();
            var currentClient = clients.Where(n => n.Name == element.Name && n.Adress == element.Adress).FirstOrDefault();

            if (currentClient == null)
            {
                throw new ArgumentException($"Клиент {element.Name} {element.Adress} не существует! ");
            }
            else
            {
                if (currentClient.Orders != null)
                {
                    currentClient.Orders.Add(order);
                }
                else
                {
                    currentClient.Orders = new List<Order> {order };
                }
                
                DB.Clients.Update(currentClient);
                DB.Save();
            }
        }



        public void RemoveClient(Client element)
        {
            var clients = DB.Clients.ReadAll();
            var currentClient = clients.Where(n => n.Name == element.Name && n.Adress == element.Adress).FirstOrDefault();

            if (currentClient == null)
            {
                throw new ArgumentException($"Клиент {element.Name} {element.Adress} не существует! Невозможно удалить");
            }
            else
            {
                DB.Clients.Delete(currentClient.Id);
                DB.Save();
            }
        }

        public void RemoveOrderByClient(Client element, Order order)
        {
            var clients = DB.Clients.ReadAll();
            var currentClient = clients.Where(n => n.Name == element.Name && n.Adress == element.Adress).FirstOrDefault();
            

            if (currentClient == null)
            {
                throw new ArgumentException($"Клиент {element.Name} {element.Adress} не существует! Невозможно удалить");
            }
            else
            {

                var queryOrder = DB.Orders.Read(order.Id);              

                if (queryOrder != null)
                {
                    DB.Orders.Delete(queryOrder.Id);
                }
                else
                {
                    throw new ArgumentException($"Невозможно удалить заказ {order.Id} {order.Description}. Заказ не существует ");
                }             

                DB.Clients.Update(currentClient);
                DB.Save();
            }
        }

        public void ChangeInfoByClient(Client currentClient, Client newClient)
        {
            var clients = DB.Clients.ReadAll();
            var queryClient = clients.Where(n => n.Name == currentClient.Name && n.Adress == currentClient.Adress).FirstOrDefault();

            if (currentClient == null)
            {
                throw new ArgumentException($"Клиент {currentClient.Name} {currentClient.Adress} не существует! Невозможно удалить");
            }
            else
            {
                queryClient.Name = newClient.Name;
                queryClient.Adress = newClient.Adress; 

                DB.Clients.Update(queryClient);
                DB.Save();
            }

        }

        public IEnumerable<Client> GetAllClients()
        {          
            return DB.Clients.ReadAll();
        }

        public IEnumerable<Order> GetAllOrders()
        {
            return DB.Orders.ReadAll();
        }



        public void Dispose()
        {
            DB.Dispose();
        }
    }
}
