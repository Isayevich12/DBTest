using DBTest.Entities;
using DBTest.EntityFramework;
using DBTest.Interfaces;
using DBTest.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace DBTest
{
    public class UnitOFWork : IUnitOfWork
    {
        private Context dataBase { get; }
        private ClientRepository clientRepository;
        private OrderRepositories orderRepositories;

        public UnitOFWork()
        {
            this.dataBase = new Context();
        }



        public IRepository<Client> Clients
        {
            get
            {
                if (clientRepository == null)
                    clientRepository = new ClientRepository(dataBase);
                return clientRepository;
            }
        }

        public IRepository<Order> Orders
        {
            get
            {
                if (orderRepositories == null)
                    orderRepositories = new OrderRepositories(dataBase);
                return orderRepositories;
            }
        }

        public void Dispose()
        {
            this.dataBase.Dispose();
        }

        public void Save()
        {
            this.dataBase.SaveChanges();
        }
    }
}
