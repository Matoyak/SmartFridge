using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SmartFridge.Models {
    public class ItemCategory {

        [ForeignKey("ItemId")]
        public int ItemId { get; set; }

        [ForeignKey("CategoryId")]
        public int CategoryId { get; set; }

        public Item Item { get; set; }

        public Category Category { get; set; }
    }
}
