using System.Collections.Generic;
using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Mvc;
using SmartFridge.Services;
using SmartFridge.Services.Models;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace SmartFridge.Controllers {

    [Route("api/Items")]
    [Authorize]
    public class ItemsController : Controller {
        private ItemService _itemServ;

        public ItemsController(ItemService itemServ) {
            _itemServ = itemServ;
        }

        /// <summary>
        /// Use only for testing purposes, in actual code use GetItemsByUser
        /// </summary>
        /// <returns>List of all items</returns>
        //[HttpGet]
        //public ICollection<ItemDTO> GetAllItems() {
        //    return _itemServ.GetItemList();
        //}

        /// <summary>
        /// Deletes item from the database.
        /// </summary>
        /// <param name="id">The item to be deleted.</param>
        [HttpPost("Delete")]
        public IActionResult Delete([FromBody]ItemDTO item) {
            if(ModelState.IsValid) {
                //add new item to db
                _itemServ.DeleteItem(item, User.Identity.Name);
                return Ok(item);
            }
            return HttpBadRequest(ModelState);
        }

        /// <summary>
        /// Calls the Item Service to return all items owned by a user
        /// </summary>
        /// <returns>Returns all items owned by the user.</returns>
        [HttpGet]
        public ICollection<ItemDTO> GetItemsByUser() {
            return _itemServ.GetItemListByUser(User.Identity.Name);
        }

        /// <summary>
        /// Calls the Item Service to add an Item to the Database
        /// </summary>
        /// <param name="item">Item info from angular input</param>
        /// <returns>IActionResult based on a valid or invalid model state.</returns>
        [HttpPost]
        public IActionResult Post([FromBody]ItemDTO item) {
            //_itemServ.AddItem(item, User.Identity.Name);
            if(ModelState.IsValid) {
                //add new item to db
                _itemServ.AddItem(item, User.Identity.Name);
                return Ok(item);
            }
            return HttpBadRequest(ModelState);
        }

        /// <summary>
        /// Put function to allow updating an item.
        /// </summary>
        /// <param name="itemToUpdate">The item to be updated.</param>
        /// <returns>Returns OK if successful.</returns>
        [HttpPut("Edit")]
        public IActionResult Put([FromBody]EditItemDTO items) {

            if(ModelState.IsValid) {
                //add new item to db
                _itemServ.UpdateItem(items, User.Identity.Name);
                return Ok(items);
            }
            return HttpBadRequest(ModelState);
        }
    }
}
