using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_Platfrom.Entities.Models.ViewModel
{
    public class AdminVM
    {
        public IEnumerable<User> Users { get; set; } = Enumerable.Empty<User>();

        public User LoggedInUser { get; set; } = new User();
        public User particularUser { get; set; } = new User();
        public IEnumerable<Mission> missions { get; set; } = Enumerable.Empty <Mission>();
        public Mission particularMission { get; set; }

        public IEnumerable<CmsPage> CmsPages { get; set; } = Enumerable.Empty<CmsPage>();
        public Story particularStory { get; set; } = new Story();

        public CmsPage ParticularCMSPage { get; set; } = new CmsPage();

        public IEnumerable<SelectListItem> countries { get; set; } = Enumerable.Empty<SelectListItem>();
        public IEnumerable<SelectListItem> cities { get; set; } = Enumerable.Empty<SelectListItem>();

        public IEnumerable<SelectListItem> themes { get; set;} = Enumerable.Empty<SelectListItem>();

        public IEnumerable<Skill> skills { get; set; }

        public IEnumerable<MissionTheme> missionThemes { get; set; } = Enumerable.Empty<MissionTheme>();

        public IEnumerable<MissionApplication> missionApplication { get; set; } = Enumerable.Empty<MissionApplication>();

        public IEnumerable<Story> stories { get; set; } = Enumerable.Empty<Story>();

        public IEnumerable<Banner> banner { get; set; } = Enumerable.Empty<Banner>();

        public Banner particularBanner { get; set; } = new Banner();

        public GoalMission ?GoalMission { get; set; }

    }
}
