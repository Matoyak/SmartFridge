﻿using SmartFridge.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartFridge.Infrastructure {
    public class ItemRepository : GenericRepository<Item> {
        public ItemRepository(ApplicationDbContext db) : base(db) { }

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
        /// Use for grabbing a list of items owned by a specified Username.
        /// </summary>
        /// <param name="userName">The owner of the item list.</param>
        /// <returns>Returns an IQueryable that will fetch all items belonging to a specified Username.</returns>
        public IQueryable<Item> GetItemsByUsername(string userName) {
            return (from i in _db.Items
                    where i.User.UserName == userName
                    //possibly order things here
                    select i);
        }

        public IQueryable<Item> GetItemByName(string name) {
            return from i in _db.Items
                   where i.Name == name
                   select i;
        }

        public IQueryable<Item> GetItemByAddedDate(DateTime addedDate) {
            return from i in _db.Items
                   where i.AddedDate == addedDate
                   select i;
        }

        /// <summary>
        /// Deletes an item from the database with a specified Id.
        /// </summary>
        /// <param name="item">The item to be deleted.</param>
        /// <returns>Returns true if the delete happened correctly.</returns>
        public bool Delete(int id) {
            Item dbItem = GetItemById(id).FirstOrDefault();
            //check if item is the same as the one passed in. Do this here or in the service?
            if(dbItem == null) {
                return false;
            }
            _db.Remove(dbItem);
            return true;
        }
    }
}
