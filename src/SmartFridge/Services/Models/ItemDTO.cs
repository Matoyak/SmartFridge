using SmartFridge.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartFridge.Services.Models
{
    public class ItemDTO
    {
        public string Name { get; set; }
        public DateTime ExpDate { get; set; }
        public DateTime AddedDate { get; set; }
        public bool IsExpired { get; set; }
        public List<string> Categories { get; set; }
    }
}
