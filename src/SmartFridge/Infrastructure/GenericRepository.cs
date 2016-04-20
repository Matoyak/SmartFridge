using System;
using System.Linq;
using SmartFridge.Models;

namespace SmartFridge.Infrastructure {

    public class GenericRepository<T> : IDisposable where T : class {
        protected ApplicationDbContext _db;

        public GenericRepository(ApplicationDbContext db) {
            _db = db;
        }

        public void Add(T entity) {
            _db.Set<T>().Add(entity);
        }

        public void Dispose() {
            _db.Dispose();
        }

        public IQueryable<T> List() {
            return _db.Set<T>();
        }

        public void SaveChanges() {
            _db.SaveChanges();
        }
    }
}
