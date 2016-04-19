using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartFridge.Models {

    public class Item {
        public bool IsExpired { get; set; }
        public DateTime AddedDate { get; set; }
        public DateTime ExpDate { get; set; }
        public ICollection<Category> Category { get; set; }
        public int Barcode { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }
    }
}
