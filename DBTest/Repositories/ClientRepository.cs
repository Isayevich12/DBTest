using DBTest.Entities;
using DBTest.EntityFramework;
using DBTest.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DBTest.Repositories
{
    public class ClientRepository : IRepository<Client>
    {
        private readonly Context DBContext;

        public ClientRepository(Context context)
        {
            this.DBContext = context;
        }

        public void Create(Client client)
        {
            DBContext.Clients.Add(client);
        }

        public void Delete(int id)
        {
            Client client = DBContext.Clients.FirstOrDefault(n => n.Id == id);
            if (client != null)
                DBContext.Clients.Remove(client);
        }

        public Client Read(int id)
        {
            return DBContext.Clients.FirstOrDefault(n => n.Id == id);
        }

        public IEnumerable<Client> ReadAll()
        {
            return DBContext.Clients;
        }

        public void Update(Client client)
        {
            DBContext.Clients.Update(client);
        }
    }
}
