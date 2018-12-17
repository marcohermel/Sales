using System;
using System.Collections.Generic;

namespace SalesMVC.Models
{
    public partial class UserSys
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int UserRoleId { get; set; }
    }
}
