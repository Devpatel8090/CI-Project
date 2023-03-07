using CI_Platfrom.Entities.Models;
using CI_Platfrom.Entities.Models.ViewModel;
using CI_Platfrom.Repository.Interface;
using Microsoft.AspNetCore.Mvc;

namespace CI_Platform.Controllers
{
    public class MissionController : Controller
    {

        //private readonly ICityRepository _cities;
        //private readonly ICountryRepository _country;
        //private readonly ISkillRepository _skill;
        //private readonly IThemeRepository _theme;
        //private readonly IUserRepository _user;
        //private readonly IMissionRepository _mission;
        private readonly IMissionVMRepository _missionvm;

        public MissionController(IMissionVMRepository missionvm)
        {
           _missionvm = missionvm;

        }
        public IActionResult LandingPage(long id=0, string sortby="Date") 
        {
            var ses =  HttpContext.Session.GetString("userEmail");

            if (ses == null)
            {
                return RedirectToAction("login", "Authentication");
            }
            else
            {
                //List<City> cityDetails = _cities.GetCityDetails();
                //ViewBag.CityDetails = cityDetails;

                //List<Country> countryDetails = _country.GetCountriesDetails();
                //ViewBag.CountryDetails = countryDetails;

                //List<Skill> skillDetails = _skill.GetSkillDetails();
                //ViewBag.SkillDetails = skillDetails;

                //List<MissionTheme> themeDetails = _theme.GetThemeDetails();
                //ViewBag.ThemeDetails = themeDetails;

                //List<User> userDetails = _user.GetUserDetails();
                //ViewBag.UserDetails = userDetails;

                //List<Mission> missionDeails = _mission.GetMissionDetails();
                //ViewBag.MissionDeails = missionDeails;

                var emailFromSession = HttpContext.Session.GetString("userEmail");
                MissionVM missionObj =  _missionvm.GetAllMissions(emailFromSession, id,sortby);
                //var user = userDetails.FirstOrDefault(e => e.Email == emailFromSession);
                //ViewBag.LoginUser = user;
                return View(missionObj);
            }
        }




        public IActionResult VolunteeringPage()
        {
            return View();
        }
    }
}
