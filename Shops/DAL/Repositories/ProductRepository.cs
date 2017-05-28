using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interfaces;

namespace DAL.Repositories
{
    public class ProductRepository : IRepository<Product>
    {
        private ShopsDBConnection db;

        public ProductRepository(ShopsDBConnection conn)
        {
            db = conn;
        }

        public IEnumerable<Product> GetAll()
        {
            return db.Products.Include(p => p.Shop);
        }

        public Product GetItem(int id)
        {
            return db.Products.Find(id);
        }

        public IEnumerable<Product> Find(Func<Product, bool> predicate)
        {
            return db.Products.Include(p => p.Shop).Where(predicate).ToList();
        }

        public void Create(Product item)
        {
            db.Products.Add(item);
        }

        public void Update(Product item)
        {
            db.Entry(item).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            var product = db.Products.Find(id);
            if (product != null)
            {
                db.Products.Remove(product);
            }
        }
    }
}
