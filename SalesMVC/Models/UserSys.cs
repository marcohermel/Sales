using Microsoft.AspNetCore.Identity;

namespace SalesMVC.Models
{
    public partial class UserSys : IdentityUser
    {
       // public int Id { get; set; }
        public string Login { get; set; }
       // public string Email { get; set; }
        public string Password { get; set; }
        public int UserRoleId { get; set; }
    }
}
