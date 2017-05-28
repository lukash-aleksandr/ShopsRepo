using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interfaces;
using DAL.Repositories;

namespace DAL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private ShopsDBConnection db = new ShopsDBConnection();
        private ShopRepository shopRepository;
        private ProductRepository productRepository;

        public IRepository<Shop> Shops
        {
            get
            {
                if (shopRepository == null)
                {
                    shopRepository = new ShopRepository(db);
                }
                return shopRepository;
            }
        }

        public IRepository<Product> Products
        {
            get
            {
                if (productRepository == null)
                {
                    productRepository = new ProductRepository(db);
                }
                return productRepository;
            }
        }

        public void Save()
        {
            db.SaveChanges();
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
                disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
