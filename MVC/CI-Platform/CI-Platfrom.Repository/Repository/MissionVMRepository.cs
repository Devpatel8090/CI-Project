using CI_Platfrom.Entities.Models;
using CI_Platfrom.Entities.Models.ViewModel;
using CI_Platfrom.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_Platfrom.Repository.Repository
{
    public class MissionVMRepository : IMissionVMRepository
    {

        private readonly ICityRepository _cities;
        private readonly ICountryRepository _country;
        private readonly ISkillRepository _skill;
        private readonly IThemeRepository _theme;
        private readonly IUserRepository _user;
        private readonly IMissionRepository _mission;
        

        

        public MissionVMRepository(ICityRepository cities, ICountryRepository country, ISkillRepository skill, IThemeRepository theme, IUserRepository user, IMissionRepository mission)
        {
            _cities = cities;
            _country = country;
            _skill = skill;
            _theme = theme;
            _user = user;
            _mission = mission;
          
        }


        public MissionVM GetAllMissions(string emailFromSession, long CountryId = 0, string sortby="Date")
        {

            MissionVM missionVM = new();

            

            IEnumerable<Country> countryDetails = _country.GetCountriesDetails();
            missionVM.Country = countryDetails;

            IEnumerable<Skill> skillDetails = _skill.GetSkillDetails();
            missionVM.skills = skillDetails;

            IEnumerable<MissionTheme> themeDetails = _theme.GetThemeDetails();
            missionVM.MissionTheme = themeDetails;

            IEnumerable<User> userDetails = _user.GetUserDetails();
            missionVM.User = userDetails;

            IEnumerable<Mission> missionDetails = _mission.GetMissionDetails();
            missionVM.Mission = missionDetails;


            var user = userDetails.FirstOrDefault(e => e.Email == emailFromSession);
            missionVM.user = user;


            if(CountryId == 0)
            {
                IEnumerable<City> cityDetails = _cities.GetCityDetails();
                missionVM.City = cityDetails;
            }
            else
            {

                missionVM.City = _cities.CityByCountry(CountryId);
            }

            if(sortby == "Date")
            {
               
                missionVM.Mission = missionDetails.OrderBy(u => u.StartDate);
            }
            else if(sortby == "Time")
            {
                missionVM.Mission = missionDetails.OrderBy(u => u.CreateAt);
            }
            else if(sortby == "Missiontype")
            {
                missionVM.Mission = missionDetails.OrderBy(u => u.MissionType);
            }







            return missionVM;
        }
    }
}
