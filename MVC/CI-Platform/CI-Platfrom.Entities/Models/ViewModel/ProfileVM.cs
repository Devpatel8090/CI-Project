using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_Platfrom.Entities.Models.ViewModel
{
    public class ProfileVM : resetPassword
    {
        public IEnumerable<City> City { get; set; }

        public IEnumerable<Country> Country { get; set; } 

        public IEnumerable<User> users { get; set; }
        public User user { get; set; }

        public IEnumerable<Skill> skill { get; set; }

        public string imageName { get; set; }




        

    }
}
