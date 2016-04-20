using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SmartFridge.Models;

namespace SmartFridge.Infrastructure {
    public class UserRepository : GenericRepository<ApplicationUser> {
        public UserRepository(ApplicationDbContext db) : base(db) { }

        public IQueryable<ApplicationUser> FindByUserName(string userName) {
            return (from u in _db.Users
                    where u.UserName == userName
                    select u);
        }
    }
}
