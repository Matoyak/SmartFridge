using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SmartFridge.Models;

namespace SmartFridge.Services.Models {
    public class EditItemDTO {
        public ItemDTO newItem { get; set; }
        public ItemDTO currItem { get; set; }
    }
}
