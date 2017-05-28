using System;

namespace DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Shop> Shops { get; }  
        IRepository<Product> Products { get; }

        void Save();
    }
}