using CI_Platfrom.Entities.Data;
using CI_Platfrom.Entities.Models;
using CI_Platfrom.Entities.Models.ViewModel;
using CI_Platfrom.Repository.Interface;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace CI_Platform.Controllers
{
    public class MissionController : Controller
    {
        //private readonly CiPlatformContext _db;
        //private readonly ICityRepository _cities;
        ////private readonly ICountryRepository _country;
        ////private readonly ISkillRepository _skill;
        ////private readonly IThemeRepository _theme;
        ////private readonly IUserRepository _user;
        //private readonly IMissionRepository _mission;
        private readonly IUnitOfWorkRepository _unitOfWork;

        private readonly IMissionVMRepository _missionvm;

        public MissionController(IMissionVMRepository missionvm, /*ICityRepository cities, IMissionRepository mission,CiPlatformContext db*/  IUnitOfWorkRepository unitOfWork)
        {
            _missionvm = missionvm;
            _unitOfWork = unitOfWork;
            //_cities = cities;
            //_mission = mission;
            //_db = db;
        }
        public IActionResult LandingPage(string sort,string filter,long countryId=0) 
        {
            //var country = _unitOfWork.Country.GetAllCountries(countryId);
            //ViewBag.countryname = country;
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
                MissionVM missionObj =  _missionvm.GetAllMissions(emailFromSession,countryId);
          
                //var user = userDetails.FirstOrDefault(e => e.Email == emailFromSession);
                //ViewBag.LoginUser = user;
                if (filter != null || sort != null)
                {
                    return RedirectToAction("GetAllMissions",new {sort, filter,countryId});
                }

                return View(missionObj);
            }
        }

        /// <summary>
        ///  getting the all missions by filter and country id 
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public JsonResult[] GetAllMissions(string sort,string filter, long id = 0)
        {
            var sessionValue = HttpContext.Session.GetString("userEmail");

            IEnumerable<Mission> allmissions = _missionvm.ApplyFilter(sort,filter, id , sessionValue);
            JsonResult[] missions = new JsonResult[allmissions.ToList().Count];

            int i = 0;
            foreach (Mission mission in allmissions)
            {
                JsonResult eachmission = new JsonResult(
                    new
                    {
                        mission.Title,
                        mission.City.Name,
                        startDate = mission.StartDate.Value.ToShortDateString(),
                        endDate = mission.EndDate.Value.ToShortDateString(),
                        theme = mission.Theme.Title,
                        mission.ShortDescription,
                        mission.OrganizationName,
                        deadLine = ((mission.StartDate - TimeSpan.FromDays(1)).Value.ToShortDateString())
                    }

                );
                missions[i] = eachmission;
                i++;
            }
            return missions;
        }

        


        public JsonResult GetCityByCountry(long CountryId)
        {
            //var city = _cities.CityByCountry(CountryId);
            var city = _unitOfWork.City.CityByCountry(CountryId);
            
            return Json(city);
        }



        public IActionResult VolunteeringPage(long id)
         {
            var sessionValue = HttpContext.Session.GetString("userEmail");
            MissionVM missionpage =   getmissionPage(id, sessionValue);
            return View(missionpage);
        }

        public MissionVM getmissionPage(long id, string sessionValue)
        {
            MissionVM vm = new MissionVM();
            //vm.skills = _db.Skills.ToList(); 
            //vm.particularMission = _mission.GetMissionByMissionId(id);
            //vm.User = _db.Users;
            //vm.user =   _db.Users.FirstOrDefault(e => e.Email == sessionValue);

            vm.skills = _unitOfWork.Skill.GetAll().ToList();
            vm.particularMission = _unitOfWork.Mission.GetMissionByMissionId(id);
            vm.User = _unitOfWork.User.GetAll();
            vm.user = _unitOfWork.User.GetFirstOrDefault(e => e.Email == sessionValue);
            return vm;

        }

        public void AddToFavourite(string favObj)
        {
            var parseObj = JObject.Parse(favObj);
            var missionId = parseObj.Value<long>("missionId");
            var userId = parseObj.Value<long>("userId");

            var obj = new FavoriteMission()
            {
               MissionId = missionId,
               UserId = userId,
            };
            var favouritemission = _unitOfWork.FavoriteMission.GetFirstOrDefault(m => m.MissionId == missionId && m.UserId == userId);

            if(favouritemission != null)
            {
                _unitOfWork.FavoriteMission.Remove(favouritemission);
                _unitOfWork.save();
            }
            else
            {
                _unitOfWork.FavoriteMission.Add(obj);
                _unitOfWork.save();
            }

            
        }



    }
}
