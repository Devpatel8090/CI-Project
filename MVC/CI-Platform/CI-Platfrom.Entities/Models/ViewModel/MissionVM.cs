using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_Platfrom.Entities.Models.ViewModel
{
    public class MissionVM

    {   
        
        
        public IEnumerable<Mission> Mission { get; set; }
        public Mission particularMission { get; set; }

       public IEnumerable<Country> Country { get; set; }

        public IEnumerable<Skill> skills { get; set; }


        public IEnumerable<MissionApplication> missionApplications { get; set; }

        public IEnumerable<MissionTheme> MissionTheme { get; set; }

        public IEnumerable<MissionMedium> MissionMedium { get; set; }
        public IEnumerable<Mission> RelatedMission { get; set; }

        public IEnumerable<City> City { get; set; }

        public IEnumerable<User> User { get; set; }

        public Country particularCountry { get; set; }  

        /// <summary>
        ///  for taking the login in user information 
        /// </summary>
        public User user { get; set; }

        //public IEnumerable<GoalMission> Goals { get; set; }

        public IEnumerable<MissionRating> missionRatings { get; set; }

        public IEnumerable<MissionApplication> RecentVolunteers { get; set; }

        public int TotalCount { get; set; }

        public long totalMissions { get; set; }

        public int AverageRating { get; set; }
    }
}
