using System.ComponentModel.DataAnnotations;

namespace SmartFridge.Models {
    public class Category {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name cannot be empty")]
        public string Name { get; set; }
    }
}
