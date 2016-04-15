using SmartFridge.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartFridge.Infrastructure
{
    public class FridgeRepository : GenericRepository<Fridge>
    {
        public FridgeRepository(ApplicationDbContext db) : base(db) { }
    }
}
