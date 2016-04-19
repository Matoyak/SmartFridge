using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;

namespace SmartFridge.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        [Required(ErrorMessage = "First Name cannot be empty")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name cannot be empty")]
        public string LastName { get; set; }

        public string  FullName { 
            get { return FirstName + " " + LastName; }
            
        }

        public ICollection<Item> Items { get; set; }
    }
}
