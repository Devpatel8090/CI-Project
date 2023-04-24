using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_Platfrom.Entities.Models.ViewModel
{
    public class StoryVM
    {
        public IEnumerable<Mission> Mission { get; set; } = Enumerable.Empty<Mission>();
      
        public IEnumerable<Story> Story { get; set; } = Enumerable.Empty<Story>();
        public IEnumerable<MissionTheme> MissionTheme { get; set; } = Enumerable.Empty<MissionTheme>();

        public IEnumerable<MissionApplication> LogingUserMissions { get; set; } = Enumerable.Empty<MissionApplication>();


        public IEnumerable<User> User { get; set; } = Enumerable.Empty<User>();

        public User user { get; set; } = new User();

        /// <summary>
        ///  for adding new story 
        /// </summary>
        public Story particularStory { get; set; } = new Story();

        /// <summary>
        /// for taking the story in storyListing page
        /// </summary>
        public Story storyById { get; set; } = new Story();

        public StoryMedium particularStoryVideo { get; set; } = new StoryMedium();

        public ContactU ContactUs { get; set; } = new ContactU();
         
        public IEnumerable<CmsPage> CmsPages { get; set; } = Enumerable.Empty<CmsPage>();


    }
}
