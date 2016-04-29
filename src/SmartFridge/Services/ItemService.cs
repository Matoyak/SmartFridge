using System.Collections.Generic;
using System.Linq;
using SmartFridge.Infrastructure;
using SmartFridge.Models;
using SmartFridge.Services.Models;

namespace SmartFridge.Services {

    public class ItemService {
        private CategoryRepository _catRepo;
        private ItemRepository _itemRepo;
        private UserRepository _userRepo;

        public ItemService(ItemRepository itemRepo, UserRepository userRepo, CategoryRepository catRepo) {
            _itemRepo = itemRepo;
            _userRepo = userRepo;
            _catRepo = catRepo;
        }

        /// <summary>
        /// Adds an item to the database.
        /// </summary>
        /// <param name="item">
        /// The ItemDTO info grabbed from the controller, converted to Item model
        /// </param>
        public void AddItem(ItemDTO item, string currentUser) {
            Item newItem = new Item {
                Name = item.Name,
                AddedDate = System.DateTime.Now,
                Barcode = item.Barcode,
                ExpDate = item.ExpDate,
                IsExpired = item.IsExpired,
                User = (_userRepo.FindByUserName(currentUser).FirstOrDefault())
            };

            List<Category> dbCategories = _catRepo.GetCategories(item.Categories.Select(cat => cat.Name)).ToList();
            foreach(Category newCat in (from c in item.Categories
                                        where !dbCategories.Any(db => db.Name == c.Name)
                                        select new Category() {
                                            Name = c.Name
                                        })) {
                _catRepo.Add(newCat);
                dbCategories.Add(newCat);
            }
            _catRepo.SaveChanges();

            newItem.ItemCategories = (from c in dbCategories
                                      select new ItemCategory() {
                                          Category = c,
                                          Item = newItem
                                      }).ToList();

            _itemRepo.Add(newItem);
            _itemRepo.SaveChanges();
        }

        /// <summary>
        /// Service to delete an item from the database.
        /// </summary>
        /// <param name="itemFromFront">The item to be deleted.</param>
        /// <param name="currUser">The currently logged in user.</param>
        /// <returns>Returns true if it deleted successfully.</returns>
        //should this actually delete as it does now, or just "deactivate" and hide? Might be way more work than is necessary for this project.
        public bool DeleteItem(ItemDTO itemFromFront, string currUser) {
            List<Item> items = _itemRepo.GetItemsByUsername(currUser).ToList();
            foreach(Item item in items) {
                if(itemFromFront.Name == item.Name && itemFromFront.AddedDate == item.AddedDate) {
                    bool check = _itemRepo.Delete(item.Id);
                    if(!check) {
                        return false;
                    }
                    _itemRepo.SaveChanges();
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Use only for testing purposes, in actual code use GetItemListByUser()
        /// </summary>
        /// <returns>Returns a Collection of all items in the database.</returns>
        public ICollection<ItemDTO> GetItemList() {
            return (from i in _itemRepo.List()
                    select new ItemDTO() {
                        Name = i.Name,
                        ExpDate = i.ExpDate,
                        AddedDate = i.AddedDate,
                        IsExpired = i.IsExpired,
                        Categories = i.ItemCategories.Select(ic => new KeyValueDTO<int> {
                            Name = ic.Category.Name,
                            Value = ic.CategoryId
                        }).ToList()
                    }).ToList();
        }

        /// <summary>
        /// Gets the list of items belonging to a specific user.
        /// </summary>
        /// <param name="currUser">The currently logged in user.</param>
        /// <returns>Returns a collection of ItemDTOs owned by the user.</returns>
        public ICollection<ItemDTO> GetItemListByUser(string currUser) {
            return (from i in _itemRepo.GetItemsByUsername(currUser)
                    select new ItemDTO() {
                        Name = i.Name,
                        ExpDate = i.ExpDate,
                        AddedDate = i.AddedDate,
                        IsExpired = i.IsExpired,
                        Categories = i.ItemCategories.Select(ic => new KeyValueDTO<int> {
                            Name = ic.Category.Name,
                            Value = ic.CategoryId
                        }).ToList()
                    }).ToList();
        }

        /// <summary>
        /// Service to update an item to fix or expand info.
        /// </summary>
        /// <param name="itemFromFront">The item to be updated.</param>
        /// <param name="currUser">The current user</param>
        /// <returns>Returns true if successful update.</returns>
        //double check with Eric on whether this is all being done correctly.
        public bool UpdateItem(ItemDTO itemFromFront, string currUser) {
            List<Item> items = _itemRepo.GetItemsByUsername(currUser).ToList();
            List<Category> dbCategories = _catRepo.GetCategories(itemFromFront.Categories.Select(cat => cat.Name)).ToList();
            foreach(Category newCat in (from c in itemFromFront.Categories
                                        where !dbCategories.Any(db => db.Name == c.Name)
                                        select new Category() {
                                            Name = c.Name
                                        })) {
                _catRepo.Add(newCat);
                dbCategories.Add(newCat);
            }
            _catRepo.SaveChanges();

            foreach(Item item in items) {
                if(item.AddedDate == itemFromFront.AddedDate && item.Name == itemFromFront.Name) {
                    item.Name = itemFromFront.Name;
                    item.Barcode = itemFromFront.Barcode;
                    item.ExpDate = itemFromFront.ExpDate;
                    item.ItemCategories = (from c in dbCategories
                                           select new ItemCategory() {
                                               Category = c,
                                               Item = item
                                           }).ToList();
                    _itemRepo.SaveChanges();
                    return true;
                }
            }
            return false;
        }
    }
}
