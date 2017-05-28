using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interfaces;

namespace DAL.Repositories
{
    public class ShopRepository : IRepository<Shop>
    {
        private ShopsDBConnection db;

        public ShopRepository(ShopsDBConnection conn)
        {
            db = conn;
        }

        public IEnumerable<Shop> GetAll()
        {
            return db.Shops.OrderBy(s => s.Id);
        }

        public Shop GetItem(int id)
        {
            return db.Shops.Find(id);
        }

        public IEnumerable<Shop> Find(Func<Shop, bool> predicate)
        {
            return db.Shops.Where(predicate);
        }

        public void Create(Shop item)
        {
            db.Shops.Add(item);
        }

        public void Update(Shop item)
        {
            db.Entry(item).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            Shop shop = db.Shops.Find(id);
            if (shop != null)
            {
                db.Shops.Remove(shop);
            }
        }
    }
}
