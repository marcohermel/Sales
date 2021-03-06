﻿using System;
using System.Collections.Generic;

namespace SalesMVC.Models
{
    public partial class Region
    {
        public Region()
        {
            City = new HashSet<City>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<City> City { get; set; }
    }
}
