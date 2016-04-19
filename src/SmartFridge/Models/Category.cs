namespace SmartFridge.Models {
using System.ComponentModel.DataAnnotations;

    public class Category {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name cannot be empty")]
        public string Name { get; set; }
    }
}
