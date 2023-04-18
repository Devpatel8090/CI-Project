﻿using Microsoft.AspNetCore.Mvc.Rendering;
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

        public User particularUser { get; set; }
        public IEnumerable<Mission> missions { get; set; }
        public Mission particularMission { get; set; }

        public IEnumerable<CmsPage> CmsPages { get; set; }

        public CmsPage ParticularCMSPage { get; set; }

        public IEnumerable<SelectListItem> countries { get; set; }
        public IEnumerable<SelectListItem> cities { get; set; }

        public IEnumerable<SelectListItem> themes { get; set;}

        public IEnumerable<Skill> skills { get; set; }

        public IEnumerable<MissionTheme> missionThemes { get; set; }

        public IEnumerable<MissionApplication> missionApplication { get; set; }

        public IEnumerable<Story> stories { get; set; }

    }
}
