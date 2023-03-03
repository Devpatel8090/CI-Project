using CI_Platfrom.Entities.Models;
using CI_Platfrom.Repository.Interface;
using Microsoft.AspNetCore.Mvc;

namespace CI_Platform.Controllers
{
    public class MissionController : Controller
    {

        private readonly ICityRepository _cities;
        private readonly ICountryRepository _country;
        private readonly ISkillRepository _skill;
        private readonly IThemeRepository _theme;
        private readonly IUserRepository _user;
        private readonly IMissionRepository _mission;

        public MissionController(ICityRepository cities, ICountryRepository country, ISkillRepository skill, IThemeRepository theme,IUserRepository user, IMissionRepository mission)
        {
            _cities = cities;
            _country = country;
            _skill = skill;
            _theme = theme;
            _user = user;
            _mission = mission;
        }
        public IActionResult LandingPage() 
        {
            var ses =  HttpContext.Session.GetString("userEmail");

            if (ses == null)
            {
                return RedirectToAction("login", "Authentication");
            }
            else
            {
                List<City> cityDetails = _cities.GetCityDetails();
                ViewBag.CityDetails = cityDetails;

                List<Country> countryDetails = _country.GetCountriesDetails();
                ViewBag.CountryDetails = countryDetails;

                List<Skill> skillDetails = _skill.GetSkillDetails();
                ViewBag.SkillDetails = skillDetails;

                List<MissionTheme> themeDetails = _theme.GetThemeDetails();
                ViewBag.ThemeDetails = themeDetails;

                List<User> userDetails = _user.GetUserDetails();
                ViewBag.UserDetails = userDetails;

                List<Mission> missionDeails = _mission.GetMissionDetails();
                ViewBag.MissionDeails = missionDeails;

                return View();
            }
        }




        public IActionResult VolunteeringPage()
        {
            return View();
        }
    }
}
