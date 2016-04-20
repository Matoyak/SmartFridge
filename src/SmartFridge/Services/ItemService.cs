using System.Collections.Generic;
using System.Linq;
using SmartFridge.Infrastructure;
using SmartFridge.Models;
using SmartFridge.Services.Models;

namespace SmartFridge.Services {

    public class ItemService {
        private ItemRepository _itemRepo;
        private UserRepository _userRepo;

        public ItemService(ItemRepository itemRepo, UserRepository userRepo) {
            _itemRepo = itemRepo;
            _userRepo = userRepo;
        }

        /// <summary>
        /// Use only for testing purposes, in actual code user GetItemListByUser()
        /// </summary>
        /// <returns>Returns a Collection of all items in the database.</returns>
        public ICollection<ItemDTO> GetItemList() {
            return (from i in _itemRepo.List()
                    select new ItemDTO() {
                        Name = i.Name,
                        ExpDate = i.ExpDate,
                        AddedDate = i.AddedDate,
                        IsExpired = i.IsExpired,
                        Categories = i.ItemCategories.Select(ic => ic.Category.Name).ToList()
                    }).ToList();
        }

        public ICollection<ItemDTO> GetItemListByUser(string currUser) {
            return (from i in _itemRepo.GetItemsByUsername(currUser)
                    select new ItemDTO() {
                        Name = i.Name,
                        ExpDate = i.ExpDate,
                        AddedDate = i.AddedDate,
                        IsExpired = i.IsExpired,
                        Categories = i.ItemCategories.Select(ic => ic.Category.Name).ToList()
                    }
                    ).ToList();
        }

        /// <summary>
        /// Adds an item to the database.
        /// </summary>
        /// <param name="item">The ItemDTO info grabbed from the controller, converted to Item model</param>
        public void AddItem(ItemDTO item) {
            Item newItem = new Item {
                Name = item.Name,
                AddedDate = System.DateTime.Now,
                Barcode = item.Barcode,
                //Categories = item.Categories,
                ExpDate = item.ExpDate,
                IsExpired = item.IsExpired
            };

            _itemRepo.Add(newItem);
            _itemRepo.SaveChanges();
        }

        //public bool DeleteItem(int id) {
        //    Item item = _itemRepo.FindItemById(id).FirstOrDefault();
        //    if(item == null) {
        //        return false;
        //    }
        //    //contains method || (Where category contains c.name
        //    item.Dispose(); //create a delete method in repo
        //    //linq query that projects into strings categories.select.c.name
        //}
    }
}
