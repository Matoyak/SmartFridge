﻿using System.Collections.Generic;
using System.Linq;
using SmartFridge.Infrastructure;
using SmartFridge.Models;
using SmartFridge.Services.Models;

namespace SmartFridge.Services {

    public class ItemService {
        private ItemRepository _itemRepo;
        private UserRepository _userRepo;
        private CategoryRepository _catRepo;

        public ItemService(ItemRepository itemRepo, UserRepository userRepo, CategoryRepository catRepo) {
            _itemRepo = itemRepo;
            _userRepo = userRepo;
            _catRepo = catRepo;
        }

        /// <summary>
        /// Use only for testing purposes, in actual code user GetItemListByUser()
        /// </summary>
        /// <returns>Returns a Collection of all items in the database.</returns>
        public ICollection<ItemDTO> GetItemList() {
            return (from i in _itemRepo.List()
                    select new ItemDTO()
                    {
                        Name = i.Name,
                        ExpDate = i.ExpDate,
                        AddedDate = i.AddedDate,
                        IsExpired = i.IsExpired,
                        Categories = i.ItemCategories.Select(ic => new KeyValueDTO<int>
                        {
                            Name = ic.Category.Name,
                            Value = ic.CategoryId
                        }).ToList()
                    }).ToList();
        }

        public ICollection<ItemDTO> GetItemListByUser(string currUser) {
            return (from i in _itemRepo.GetItemsByUsername(currUser)
                    select new ItemDTO() {
                        Name = i.Name,
                        ExpDate = i.ExpDate,
                        AddedDate = i.AddedDate,
                        IsExpired = i.IsExpired,
                        Categories = i.ItemCategories.Select(ic => new KeyValueDTO<int>
                        {
                            Name = ic.Category.Name,
                            Value = ic.CategoryId
                        }).ToList()
                    }
                    ).ToList();
        }

        /// <summary>
        /// Adds an item to the database.
        /// </summary>
        /// <param name="item">The ItemDTO info grabbed from the controller, converted to Item model</param>
        public void AddItem(ItemDTO item, string currentUser) {

            Item newItem = new Item {
                Name = item.Name,
                AddedDate = System.DateTime.Now,
                ItemCategories = new List<ItemCategory>(),
                Barcode = item.Barcode,
                ExpDate = item.ExpDate,
                IsExpired = item.IsExpired,
                User = (_userRepo.FindByUserName(currentUser).FirstOrDefault())
            };

            foreach(var c in item.Categories)
            {
                newItem.ItemCategories.Add(new ItemCategory
                {
                    CategoryId = c.Value
                });
            }

            _itemRepo.Add(newItem);
            _itemRepo.SaveChanges();
        }

        public bool DeleteItem(int id) {
            bool check = _itemRepo.Delete(id);
            if(!check) {
                return false;
            }
            _itemRepo.SaveChanges();
            return true;
        }
    }
}
