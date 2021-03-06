﻿using System;
using System.Collections.Generic;

namespace SmartFridge.Services.Models {

    public class ItemDTO {

        public DateTime AddedDate { get; set; }

        public int Barcode { get; set; }

        public ICollection<KeyValueDTO<int>> Categories { get; set; }

        public DateTime ExpDate { get; set; }

        public bool IsExpired { get; set; }

        public string Name { get; set; }
    }

    public class KeyValueDTO<T> {

        public string Name { get; set; }

        public T Value { get; set; }
    }
}
