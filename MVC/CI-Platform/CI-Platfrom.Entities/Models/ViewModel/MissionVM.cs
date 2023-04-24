using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_Platfrom.Entities.Models.ViewModel
{
    public class MissionVM

    {   
        
        
        public IEnumerable<Mission> Mission { get; set; } = Enumerable.Empty<Mission>();
        public Mission particularMission { get; set; } = new Mission();

       public IEnumerable<Country> Country { get; set; } = Enumerable.Empty<Country>();

        public IEnumerable<Skill> skills { get; set; } = Enumerable.Empty<Skill>();


        public IEnumerable<MissionApplication> missionApplications { get; set; } = Enumerable.Empty<MissionApplication>();

        public IEnumerable<MissionTheme> MissionTheme { get; set; } = Enumerable.Empty<MissionTheme>();

        public IEnumerable<MissionMedium> MissionMedium { get; set; } = Enumerable.Empty<MissionMedium>();
        public IEnumerable<Mission> RelatedMission { get; set; } = Enumerable.Empty<Mission>();

        public IEnumerable<City> City { get; set; } = Enumerable.Empty<City>();

        public IEnumerable<User> User { get; set; } = Enumerable.Empty<User>();

        public IEnumerable<CmsPage> CmsPages { get; set; } = Enumerable.Empty<CmsPage>();

        public Country particularCountry { get; set; }   = new Country();

        /// <summary>
        ///  for taking the login in user information 
        /// </summary>
        public User user { get; set; } = new User();

        //public IEnumerable<GoalMission> Goals { get; set; }

        public IEnumerable<MissionRating> missionRatings { get; set; } = Enumerable.Empty<MissionRating>();

        public IEnumerable<MissionApplication> RecentVolunteers { get; set; } = Enumerable.Empty<MissionApplication>();

        public ContactU ContactUs { get; set; } = new ContactU();

        public int TotalCount { get; set; } = 0;

        public long totalMissions { get; set; } 

        public int AverageRating { get; set; }

        public IEnumerable<Timesheet> timesheets { get; set; } = Enumerable.Empty<Timesheet>();
    }
}
