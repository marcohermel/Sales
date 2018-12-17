using System.ComponentModel.DataAnnotations;

namespace SalesMVC.Models
{
    public class LoginViewModel
    {
        [Display(Name = "E-mail")]
        public string Email { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public string ReturnUrl { get; set; }
        public bool CheckEmailAndPassword { get; set; }
    }
}
