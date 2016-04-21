using SmartFridge.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartFridge.Infrastructure
{
    public class CategoryRepository : GenericRepository<Category>
    {
        CategoryRepository(ApplicationDbContext db) : base(db) { }

        public IQueryable<Category> AddCategoryNames(ICollection<string> categories)
        {
            return from c in _db.Categories
                   where c.ItemCategories == categories
                    select c;
        }
    }
}
