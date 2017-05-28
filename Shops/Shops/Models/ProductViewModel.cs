﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shops.Models
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public int ShopId { get; set; }
    }
}