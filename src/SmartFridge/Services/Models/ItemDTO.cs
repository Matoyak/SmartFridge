using SmartFridge.Models;
using System;
using System.Collections.Generic;

namespace SmartFridge.Services.Models {

    public class ItemDTO {
        public bool IsExpired { get; set; }

        public DateTime AddedDate { get; set; }
        public DateTime ExpDate { get; set; }

        public int Barcode { get; set; }

        public List<string> Categories { get; set; }

        public string Name { get; set; }
    }
}
