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
      
        public IEnumerable<Story> Story { get; set; }

        public IEnumerable<MissionTheme> MissionTheme { get; set; }

        public IEnumerable<MissionApplication> LogingUserMissions { get; set; }


        public IEnumerable<User> User { get; set; }

        public User user { get; set; }

        /// <summary>
        ///  for adding new story 
        /// </summary>
        public Story particularStory { get; set; }

        /// <summary>
        /// for taking the story in storyListing page
        /// </summary>
        public Story storyById { get; set; }

        public StoryMedium particularStoryVideo { get; set; }

      
    }
}
