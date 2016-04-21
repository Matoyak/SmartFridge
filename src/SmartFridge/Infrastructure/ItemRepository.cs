using SmartFridge.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartFridge.Infrastructure {
    public class ItemRepository : GenericRepository<Item> {
        public ItemRepository(ApplicationDbContext db) : base(db) { }

        public IQueryable<Item> GetItemById(int id) {
            return from i in _db.Items
                   where i.Id == id
                   select i;
        }

        public IQueryable<Item> GetItemsByUsername(string userName) {
            return (from i in _db.Items
                    where i.User.UserName == userName
                    //possibly order things here
                    select i);
        }

        public bool Delete(int id) {
            Item dbItem = GetItemById(id).FirstOrDefault();
            if(dbItem == null) {
                return false;
            }
            _db.Remove(dbItem);
            return true;
        }
    }
}
