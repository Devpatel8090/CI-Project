using CI_Platfrom.Entities.Data;
using CI_Platfrom.Entities.Models;
using CI_Platfrom.Entities.Models.ViewModel;
using CI_Platfrom.Repository.Interface;
using Microsoft.AspNetCore.Mvc;

namespace CI_Platform.Controllers
{
    public class MissionController : Controller
    {
        private readonly CiPlatformContext _db;
        private readonly ICityRepository _cities;
        //private readonly ICountryRepository _country;
        //private readonly ISkillRepository _skill;
        //private readonly IThemeRepository _theme;
        //private readonly IUserRepository _user;
        private readonly IMissionRepository _mission;
        private readonly IMissionVMRepository _missionvm;

        public MissionController(IMissionVMRepository missionvm, ICityRepository cities, IMissionRepository mission,CiPlatformContext db)
        {
            _missionvm = missionvm;
            _cities = cities;
            _mission = mission;
            _db = db;
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
                MissionVM missionObj =  _missionvm.GetAllMissions(emailFromSession);
                //var user = userDetails.FirstOrDefault(e => e.Email == emailFromSession);
                //ViewBag.LoginUser = user;
                return View(missionObj);
            }
        }

        public JsonResult GetCityByCountry(long CountryId)
        {
            var city = _cities.CityByCountry(CountryId);
            return Json(city);
        }

        public JsonResult[] DateSort(string sort)
        {

            //var session_details = HttpContext.Session.GetString("Login");
            var missiondata = _mission.GetBySort(sort);
            var missionlist = new JsonResult[missiondata.ToList().Count];

            int i = 0;

            foreach (Mission mission in missiondata)
            {
                var missionObj = new JsonResult(new
                {
                    mission.MissionId,
                    mission.Title,
                    mission.City.Name,
                    mission.ShortDescription,
                    Theme = mission.Theme.Title,
                    mission.OrganizationName,
                    mission.OrganizationDetails,
                    StartDate = mission.StartDate.Value.ToShortDateString(),
                    EndDate = mission.EndDate.Value.ToShortDateString(),
                    Deadline = (mission.StartDate - TimeSpan.FromDays(1)).Value.ToShortDateString(),
                    mission.MissionType
                });
                missionlist[i] = missionObj;
                i++;
            }

            return missionlist;


            
        }


        public IActionResult VolunteeringPage()
        {
            return View();
        }
    }
}
