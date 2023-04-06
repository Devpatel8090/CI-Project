using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_Platfrom.Entities.Models.ViewModel
{
    public class ProfileVM 
    {
        public IEnumerable<City> City { get; set; }

        public IEnumerable<Country> Country { get; set; } 

        public IEnumerable<User> Users { get; set; }

        public User user { get; set; }

        public IEnumerable<Skill> skill { get; set; }

        public IEnumerable<UserSkill> userSkill { get; set; }

        public string imageName { get; set; }

        [Required(ErrorMessage = "New Password is required")]
        public string OldPassword { get; set; }


        [Required(ErrorMessage = "New Password is required")]
       /* [Compare("OldPassword", ErrorMessage = "Current Password and New password must be different")]*/
        public string NewPassword { get; set; }


        [Required(ErrorMessage = "Confirm Password is required")]
        [Compare("NewPassword", ErrorMessage = "New Password and confirm password must be same.")]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }




        

    }
}
