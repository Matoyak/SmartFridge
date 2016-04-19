using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartFridge.Models {

    public class Item {
        public bool IsExpired { get; set; }

        [Required(ErrorMessage = "Added Date cannot be empty")]
        public DateTime AddedDate { get; set; }

        [Required(ErrorMessage = "Expire Date cannot be empty")]
        public DateTime ExpDate { get; set; }

        public ICollection<Category> Categories { get; set; }

        public int Barcode { get; set; }
        public int Id { get; set; }

        [Required(ErrorMessage ="Name cannot be empty")]
        public string Name { get; set; }

        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }
    }
}
