using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_Platfrom.Entities.Models.ViewModel
{
    public class resetPassword
    {
        public string email { get; set; } = string.Empty;
        public string token { get; set; } = string.Empty;
        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string password { get; set; } = string.Empty;
        [Required(ErrorMessage = "Confirm Password is required")]
        [Compare("password", ErrorMessage = "Password and confirm password must be same.")]
        [Display(Name = "Confirm Password")]
        public string confirmpassword { get; set; } = string.Empty;
    }
}
