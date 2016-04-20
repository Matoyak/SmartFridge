using System.Collections.Generic;
using System.Linq;
using SmartFridge.Infrastructure;
using SmartFridge.Models;
using SmartFridge.Services.Models;

namespace SmartFridge.Services {

    public class ItemService {
        private ItemRepository _itemRepo;

        public ItemService(ItemRepository itemRepo) {
            _itemRepo = itemRepo;
        }

        public ICollection<ItemDTO> GetItemList()
        {
            return (from i in _itemRepo.List()
                    select new ItemDTO()
                    {
                        Name = i.Name,
                        ExpDate = i.ExpDate,
                        AddedDate = i.AddedDate,
                        IsExpired = i.IsExpired,
                        Categories = i.Categories
                    }).ToList();
        }

        /// <summary>
        /// Adds an item to the database.
        /// </summary>
        /// <param name="item">The ItemDTO info grabbed from the controller, converted to Item model</param>
        public void AddItem(ItemDTO item) {
            Item newItem = new Item {
                Name = item.Name,
                AddedDate = item.AddedDate,
                Barcode = item.Barcode,
                Categories = item.Categories,
                ExpDate = item.ExpDate,
                IsExpired = item.IsExpired
            };

            _itemRepo.Add(newItem);
            _itemRepo.SaveChanges();
        }
    }
}
