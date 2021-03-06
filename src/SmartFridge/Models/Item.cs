﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartFridge.Models {

    public class Item {

        [Required(ErrorMessage = "Added Date cannot be empty")]
        public DateTime AddedDate { get; set; }

        public int Barcode { get; set; }

        [Required(ErrorMessage = "Expire Date cannot be empty")]
        public DateTime ExpDate { get; set; }

        public int Id { get; set; }

        public bool IsExpired { get; set; }

        public ICollection<ItemCategory> ItemCategories { get; set; }

        [Required(ErrorMessage = "Name cannot be empty")]
        public string Name { get; set; }

        public ApplicationUser User { get; set; }

        [ForeignKey("UserId")]
        public string UserId { get; set; }
    }
}
