﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shops.Models
{
    public class ShopViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string TimeSchedule { get; set; }
    }
}