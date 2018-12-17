using Microsoft.AspNetCore.Identity;

namespace SalesMVC.Models
{
    public partial class UserRole : IdentityRole
    {
        //public int Id { get; set; }
       // public string Name { get; set; }
        public bool IsAdmin { get; set; }
    }
}
