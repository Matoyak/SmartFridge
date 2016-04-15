using SmartFridge.Infrastructure;
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
    }
}
