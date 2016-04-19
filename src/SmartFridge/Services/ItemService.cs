using SmartFridge.Infrastructure;
using SmartFridge.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartFridge.Services
{
    public class ItemService
    {
        private ItemRepository _itemRepo;

        public ItemService(ItemRepository itemRepo)
        {
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
                        IsExpired = i.IsExpired
                        
                    }).ToList();
        }

    }
}
