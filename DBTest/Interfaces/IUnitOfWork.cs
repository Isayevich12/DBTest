using DBTest.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DBTest.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Client> Clients { get; }
        IRepository<Order> Orders { get; }      
        void Save();
    }
}
