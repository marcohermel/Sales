﻿using System;
using System.Collections.Generic;

namespace PraticalTest.Models
{
    public partial class UserRole
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsAdmin { get; set; }
    }
}
