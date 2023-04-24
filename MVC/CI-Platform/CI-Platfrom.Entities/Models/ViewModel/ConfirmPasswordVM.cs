using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_Platfrom.Entities.Models.ViewModel
{
    public class ConfirmPasswordVM
    {

        public User User { get; set; } = new User();

        [Required]
        public string ConfirmPassword { get; set; } = string.Empty;
    }

   
}
