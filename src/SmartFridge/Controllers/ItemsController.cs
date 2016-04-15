using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using SmartFridge.Services;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace SmartFridge.Controllers
{
    [Route("api/[controller]")]
    public class ItemsController : Controller
    {
        private ItemService _itemServ;

        public ItemsController(ItemService itemServ)
        {
            _itemServ = itemServ;
        }

        // GET: api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

       
    }
}
