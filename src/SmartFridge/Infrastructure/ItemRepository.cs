using System;
using System.Linq;
using SmartFridge.Models;

namespace SmartFridge.Infrastructure {

    public class ItemRepository : GenericRepository<Item> {

        public ItemRepository(ApplicationDbContext db) : base(db) { }

        /// <summary>
        /// Deletes an item from the database with a specified Id.
        /// </summary>
        /// <param name="item">The item to be deleted.</param>
        /// <returns>Returns true if the delete happened correctly.</returns>
        public bool Delete(int id) {
            Item dbItem = GetItemById(id).FirstOrDefault();
            if(dbItem == null) {
                return false;
            }
            _db.Remove(dbItem);
            return true;
        }

        /// <summary>
        /// Deletes a specified item from the database.
        /// </summary>
        /// <param name="item">The item to be deleted.</param>
        /// <returns>Returns true if upon successful deletion.</returns>
        public bool Delete(Item item) {
            if(item == null) {
                return false;
            }
            _db.Remove(item);
            return true;
        }

        /// <summary>
        /// Use for grabbing an item / item list of a specific Added Date. (For use in conjunction
        /// with other queries).
        /// </summary>
        /// <param name="addedDate">The added date of the item(s) to return.</param>
        /// <returns>Returns an IQueryable that grabs all items with the same creation date.</returns>
        public IQueryable<Item> GetItemByAddedDate(DateTime addedDate) {
            return from i in _db.Items
                   where i.AddedDate == addedDate
                   select i;
        }

        /// <summary>
        /// Use for grabbing an item with specified Id.
        /// </summary>
        /// <param name="id">The Id of the item to be grabbed.</param>
        /// <returns>Returns an IQueryable that will fetch an item with specified Id.</returns>
        public IQueryable<Item> GetItemById(int id) {
            return from i in _db.Items
                   where i.Id == id
                   select i;
        }

        /// <summary>
        /// Use for grabbing an item / item list of a specific name. (For use in conjunction with
        /// other queries).
        /// </summary>
        /// <param name="name">The name of the item(s) to return.</param>
        /// <returns>Returns an IQueryable that grabs all items of a specific name.</returns>
        public IQueryable<Item> GetItemByName(string name) {
            return from i in _db.Items
                   where i.Name == name
                   select i;
        }

        /// <summary>
        /// Gets a single item belonging to a specified user that has a specific name and added date.
        /// </summary>
        /// <param name="userName">The user the item belongs to.</param>
        /// <param name="itemName">The name of the item.</param>
        /// <param name="dateAdded">The creation date of the item.</param>
        /// <returns>Returns  an IQueryable that will fetch a specific item based on the correct criteria.</returns>
        public IQueryable<Item> GetItemByUsername(string userName, string itemName, DateTime dateAdded) {
            return from i in _db.Items
                   where i.User.UserName == userName && i.AddedDate == dateAdded && i.Name == itemName
                   //possibly order things here
                   select i;
        }

        /// <summary>
        /// Use for grabbing a list of items owned by a specified Username.
        /// </summary>
        /// <param name="userName">The owner of the item list.</param>
        /// <returns>
        /// Returns an IQueryable that will fetch all items belonging to a specified Username.
        /// </returns>
        public IQueryable<Item> GetItemsByUsername(string userName) {
            return from i in _db.Items
                   where i.User.UserName == userName
                   //possibly order things here
                   select i;
        }
    }
}
