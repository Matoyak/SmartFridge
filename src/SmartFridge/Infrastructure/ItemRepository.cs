using SmartFridge.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartFridge.Infrastructure
{
    public class ItemRepository : GenericRepository<Item>
    {
        public ItemRepository(ApplicationDbContext db) : base(db) { }
    }
}
