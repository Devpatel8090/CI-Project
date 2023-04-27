using CI_Platform.Model;
using CI_Platfrom.Entities.Data;
using CI_Platfrom.Entities.Models;
using CI_Platfrom.Entities.Models.ViewModel;
using CI_Platfrom.Repository.Interface;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MimeKit;
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
        private readonly SMTPConfigModel _smtpconfig;
        private IConfiguration _configuration;
        public MissionController(IMissionVMRepository missionvm, /*ICityRepository cities, IMissionRepository mission,CiPlatformContext db*/  IUnitOfWorkRepository unitOfWork, IOptions<SMTPConfigModel> smtpConfig, IConfiguration configuration)
        {
            _missionvm = missionvm;
            _unitOfWork = unitOfWork;
            _configuration = configuration;
            _smtpconfig = smtpConfig.Value;
            //_cities = cities;
            //_mission = mission;
            //_db = db;
        }
        public IActionResult LandingPage(string sort, string filter, long countryId = 0, int page = 1,string search="")
        {
            //var country = _unitOfWork.Country.GetAllCountries(countryId);
            //ViewBag.countryname = country;
            var ses = HttpContext.Session.GetString("userEmail");

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
                MissionVM missionObj = _missionvm.GetAllMissions(emailFromSession, countryId);
                missionObj.CmsPages = _unitOfWork.CMSPage.GetAllCMSPageDetails();



                //var user = userDetails.FirstOrDefault(e => e.Email == emailFromSession);
                //ViewBag.LoginUser = user;
                if (filter != null || sort != null || page > 1 || search != "")
                {
                    
                    return RedirectToAction("GetAllMissions", new { sort, filter, countryId,page,search });

                }
                missionObj.Mission = missionObj.Mission.Skip((page - 1) * 9).Take(9);
                return View(missionObj);
            }
        }

        /// <summary>
        ///  getting the all missions by filter and country id 
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public IActionResult GetAllMissions(string sort, string filter, long id = 0,int page=1,string search = "")
        {
            var sessionValue = HttpContext.Session.GetString("userEmail");

            MissionVM allmissions = _missionvm.ApplyFilter(sort, filter, id, sessionValue,page,search);

            return PartialView("_GridCards",allmissions);
            //JsonResult[] missions = new JsonResult[allmissions.Mission.ToList().Count];

            //int i = 0;
            //foreach (Mission mission in allmissions.Mission)
            //{
            //    JsonResult eachmission = new JsonResult(
            //        new
            //        {
            //            mission.Title,
            //            mission.City.Name,
            //            startDate = mission.StartDate.Value.ToShortDateString(),
            //            endDate = mission.EndDate.Value.ToShortDateString(),
            //            theme = mission.Theme.Title,
            //            mission.ShortDescription,
            //            mission.OrganizationName,
            //            deadLine = ((mission.StartDate - TimeSpan.FromDays(1)).Value.ToShortDateString())
            //        }

            //    );
            //    missions[i] = eachmission;
            //    i++;
            //}
            //return missions;
        }




        public JsonResult GetCityByCountry(long CountryId)
        {
            //var city = _cities.CityByCountry(CountryId);
            var city = _unitOfWork.City.CityByCountry(CountryId);

            return Json(city);
        }



        public IActionResult VolunteeringPage(long id)
        {
            var ses = HttpContext.Session.GetString("userEmail");

            if (ses == null)
            {
                return RedirectToAction("login", "Authentication");
            }
            else
            {
                var sessionValue = HttpContext.Session.GetString("userEmail");
                MissionVM missionpage = getmissionPage(id, sessionValue);
                missionpage.CmsPages = _unitOfWork.CMSPage.GetAllCMSPageDetails();
                return View(missionpage);
            }
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
            vm.user = _unitOfWork.User.GetFirstOrDefault(e => e.Email == sessionValue);   
            var loginuser = vm.user.UserId;
            vm.User = _unitOfWork.User.GetAll().Where(e => e.UserId != loginuser );
            vm.missionRatings = _unitOfWork.MissionRating.GetAll().Where(m => m.MissionId == id);
            vm.RelatedMission = _unitOfWork.Mission.getRelatedMissions(id);
            vm.RecentVolunteers = _unitOfWork.MissionApplication.GetUsersByMissionId(id);
            vm.missionApplications = _unitOfWork.MissionApplication.GetAll();
            vm.CmsPages = _unitOfWork.CMSPage.GetAllCMSPageDetails();
            


            int sum = 0;
            foreach (MissionRating rating in vm.missionRatings)
            {
                sum += rating.Rating;
            }
            if (sum > 0)
            {
                vm.AverageRating = sum / vm.missionRatings.Count();
            }
            else
            {
                vm.AverageRating = 0;
            }
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

            if (favouritemission != null)
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



        public void Recommendation(string inviteObj)
        {
            var parseObj = JObject.Parse(inviteObj);
            var missionId = parseObj.Value<long>("missionId");
            var userId = parseObj.Value<long>("userId");
            var toUserId = parseObj.Value<long>("toUserId");
            var mailTo = parseObj.Value<string>("toUserMail");


            var recObj = new MissionInvite()
            {
                MissionId = missionId,
                FromUserId = userId,
                ToUserId = toUserId,
                
            };

            var inviteLink = Url.Action("VolunteeringPage", "Mission", new { id = missionId }, Request.Scheme);
            var loginUrl = Url.Action("login", "Authentication", new { InviteURL = inviteLink }, Request.Scheme);
            TempData["link"] = inviteLink;

            UserEmailOptions userEmailOptions = new UserEmailOptions()
            {
                Subject = "He is invinting you in this mission ",
                Body = "Your Collegue invites you in this mission" + "<a href=" + loginUrl + ">"  + loginUrl +  "</a>"
            };

            _unitOfWork.MissionInvite.Add(recObj);
            _unitOfWork.save();
            SendEmail(mailTo, userEmailOptions);
        }

        public void SendEmail(string toEmail, UserEmailOptions userEmailOptions)
        {
            var email = new MimeMessage();

            email.From.Add(new MailboxAddress(_smtpconfig.SenderDisplayName, _smtpconfig.SenderAddress));
            email.To.Add(new MailboxAddress("Jay", toEmail));

            email.Subject = userEmailOptions.Subject;
            var bodyBuilder = new BodyBuilder();

            bodyBuilder.HtmlBody = userEmailOptions.Body;
            email.Body = bodyBuilder.ToMessageBody();

            using (var smtp = new SmtpClient())
            {
                smtp.Connect("smtp.gmail.com", 465, true);

                smtp.Authenticate(_smtpconfig.UserName, _smtpconfig.Password);

                smtp.Send(email);

                smtp.Disconnect(true);
            }
        }

        public void userStarRating(string userRatingObj)
        {
            var parseObject = JObject.Parse(userRatingObj);
            var rating = parseObject.Value<int>("rating");
            var missionId = parseObject.Value<long>("missionId");
            var userId = parseObject.Value<long>("userId");

            MissionRating rateObj = new MissionRating()
            {
                Rating = rating,
                MissionId = missionId,
                UserId = userId,

            };

            var alreadyRated = _unitOfWork.MissionRating.GetFirstOrDefault(m => m.UserId == userId && m.MissionId == missionId);

            if (alreadyRated != null)
            {
                alreadyRated.Rating = rating;
                _unitOfWork.MissionRating.Update(alreadyRated);
                _unitOfWork.save();
            }
            else
            {
                _unitOfWork.MissionRating.Add(rateObj);
                _unitOfWork.save();
            }

        }

        public IActionResult postComment(string commentObj)
        {
            var parseObject = JObject.Parse(commentObj);
            var missionId = parseObject.Value<long>("missionId");
            var userId = parseObject.Value<long>("userId");
            var commentText = parseObject.Value<string>("commenttext");

            var commentObject = new Comment()
            {
                CommentText = commentText,
                MissionId = missionId,
                UserId = userId
            };

            //var alreadyCommented = _unitOfWork.Comment.GetFirstOrDefault(m => m.UserId == userId && m.MissionId==missionId);
            //if (alreadyCommented != null)
            //{
            //    alreadyCommented.CommentText = commentText;
            //    alreadyCommented.UpdatedAt = DateTime.Now;
            //    _unitOfWork.Comment.Update(alreadyCommented);
            //    _unitOfWork.save();
            //    IEnumerable<Comment> comments = _unitOfWork.Comment.GetAllCommentsByMission(missionId);
            //    return PartialView("_Comments",comments);
            //}
            //else
            //{
                _unitOfWork.Comment.Add(commentObject);
                _unitOfWork.save();
                IEnumerable<Comment> comments = _unitOfWork.Comment.GetAllCommentsByMission(missionId).Where(comment => comment.ApprovalStatus == "PUBLISHED");
                return PartialView("_Comments", comments);
            //}
        }

        public void ApplyToMission(string applyObj)
        {
            var parseObject = JObject.Parse(applyObj);
            var missionId = parseObject.Value<long>("missionId");
            var userId = parseObject.Value<long>("userId");
            var applyObject = new MissionApplication()
            {
                MissionId = missionId,
                UserId=userId,
                AppliedAt = DateTime.Now
            };

            _unitOfWork.MissionApplication.Add(applyObject);
            _unitOfWork.save();
        }
    }
}
