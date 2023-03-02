using CI_Platfrom.Entities.Data;
using CI_Platfrom.Entities.Models;
using CI_Platfrom.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_Platfrom.Repository.Repository
{
     public class ThemeRepository : IThemeRepository
    {
        private readonly CiPlatformContext _db;
        public ThemeRepository(CiPlatformContext db)
        {
            _db = db;
        }
        public List<MissionTheme> GetThemeDetails()
        {
            List<MissionTheme> themeList = _db.MissionThemes.ToList();
            return themeList;
        }
    }
}
