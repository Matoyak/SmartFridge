﻿using System.Collections.Generic;
using System.Linq;
using SmartFridge.Infrastructure;
using SmartFridge.Services.Models;

namespace SmartFridge.Services {

    public class ItemService {
        private ItemRepository _itemRepo;

        public ItemService(ItemRepository itemRepo) {
            _itemRepo = itemRepo;
        }

        public ICollection<ItemDTO> GetItemList() {
            return (from i in _itemRepo.List()
                    select new ItemDTO() {
                        Name = i.Name,
                        ExpDate = i.ExpDate,
                        AddedDate = i.AddedDate,
                        IsExpired = i.IsExpired,
                        Categories = i.Categories
                    }).ToList();
        }
    }
}
