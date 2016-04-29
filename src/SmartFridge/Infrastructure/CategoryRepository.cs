using System.Collections.Generic;
using System.Linq;
using SmartFridge.Models;

namespace SmartFridge.Infrastructure {

    public class CategoryRepository : GenericRepository<Category> {

        public CategoryRepository(ApplicationDbContext db) : base(db) { }

        public IQueryable<Category> GetCategories(IEnumerable<string> categories) {
            return from c in _db.Categories
                   where categories.Contains(c.Name)
                   select c;
        }
    }
}
