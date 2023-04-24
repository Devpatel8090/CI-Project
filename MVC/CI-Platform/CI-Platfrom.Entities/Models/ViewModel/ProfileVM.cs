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
        public IEnumerable<City> City { get; set; } = Enumerable.Empty<City>();

        public IEnumerable<Country> Country { get; set; } = Enumerable.Empty<Country>();

        public IEnumerable<User> Users { get; set; } = Enumerable.Empty<User>();

        public User user { get; set; } = new User();

        public IEnumerable<Skill> skill { get; set; } = Enumerable.Empty<Skill>();

        public IEnumerable<UserSkill> userSkill { get; set; } = Enumerable.Empty<UserSkill>();

        public string imageName { get; set; } = string.Empty;

        [Required(ErrorMessage = "New Password is required")]
        public string OldPassword { get; set; } = string.Empty;

        public ContactU ContactUs { get; set; } = new ContactU();
        public CmsPage CmsPage { get; set; } = new CmsPage();
        public IEnumerable<CmsPage> CmsPages { get; set; } = Enumerable.Empty<CmsPage>();


        [Required(ErrorMessage = "New Password is required")]
        /* [Compare("OldPassword", ErrorMessage = "Current Password and New password must be different")]*/
        public string NewPassword { get; set; } = string.Empty;


        [Required(ErrorMessage = "Confirm Password is required")]
        [Compare("NewPassword", ErrorMessage = "New Password and confirm password must be same.")]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; } = string.Empty;




        

    }
}
