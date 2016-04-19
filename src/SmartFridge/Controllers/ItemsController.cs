using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.Mvc;
using SmartFridge.Services;
using SmartFridge.Services.Models;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace SmartFridge.Controllers {

    [Route("api/[controller]")]
    public class ItemsController : Controller {
        private ItemService _itemServ;

        public ItemsController(ItemService itemServ) {
            _itemServ = itemServ;
        }

        // GET: api/values
        [HttpGet]
        public ICollection<ItemDTO> GetAllItems() {
            return _itemServ.GetItemList().ToList();
        }

        /// <summary>
        /// Calls the Item Service to add an Item to the Database
        /// </summary>
        /// <param name="item">Item info from angular input</param>
        /// <returns>IActionResult based on a valid or invalid model state.</returns>
        [HttpPost]
        public IActionResult Post([FromBody]ItemDTO item) {
            if(ModelState.IsValid) {
                //add new item to db
                _itemServ.AddItem(item);
                return Ok(item);
            }
            return HttpBadRequest(ModelState);
        }
    }
}
