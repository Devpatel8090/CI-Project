using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_Platfrom.Entities.Models.ViewModel
{
    public class AdminVM
    {
        public IEnumerable<User> Users { get; set; }

        public User user { get; set; }
        public IEnumerable<Mission> missions { get; set; }

        public IEnumerable<MissionApplication> missionApplication { get; set; }

        public IEnumerable<Story> stories { get; set; }

    }
}
