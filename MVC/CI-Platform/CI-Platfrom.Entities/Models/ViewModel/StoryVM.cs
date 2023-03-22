using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_Platfrom.Entities.Models.ViewModel
{
    public class StoryVM
    {
        public IEnumerable<Mission> Mission { get; set; }
        public Mission particularMission { get; set; }

        public IEnumerable<Country> Country { get; set; }

        public IEnumerable<Skill> skills { get; set; }
        
        public IEnumerable<Story> Story { get; set; }


        //public IEnumerable<MissionRating> MissionRating { get; set; }

        public IEnumerable<MissionTheme> MissionTheme { get; set; }

        //public IEnumerable<MissionMedium> MissionMedium { get; set; }
        public IEnumerable<Mission> RelatedMission { get; set; }

        public IEnumerable<City> City { get; set; }

        public IEnumerable<User> User { get; set; }

        public User user { get; set; }
    }
}
