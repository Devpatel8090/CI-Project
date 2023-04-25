using CI_Platfrom.Entities.Models;
using CI_Platfrom.Entities.Models.ViewModel;
using CI_Platfrom.Repository.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CI_Platform.Controllers
{
    public class AdminController : Controller
    {

        private readonly IUnitOfWorkRepository _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public AdminController(IUnitOfWorkRepository unitOfWork , IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
            _unitOfWork = unitOfWork;
        }
        /* public IActionResult AdminLandingPage()
         {
             var emailFromSession = HttpContext.Session.GetString("userEmail");
             AdminVM adminDetails = new AdminVM()
             {
                 Users = _unitOfWork.User.GetAll(),
                 user = _unitOfWork.User.GetFirstOrDefault(e => e.Email == emailFromSession),
                 missionApplication = _unitOfWork.MissionApplication.GetMissionApplications(),
                 stories = _unitOfWork.Story.GetStoryDetails()*//*.Where(e => e.Status == "PENDING")*//*,
                 missions = _unitOfWork.Mission.GetMissionDetails(),
             };


             *//* adminDetails.user = _unitOfWork.User.GetFirstOrDefault(e => e.Email == emailFromSession);
              adminDetails.missionApplication = _unitOfWork.MissionApplication.GetMissionApplications();*//*
             return View(adminDetails);
         }*/
/*
        public IActionResult AdminLoginPage()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AdminLoginPage(Admin obj)
        {
            var userDetails = _unitOfWork.Admin.GetFirstOrDefault(e => e.Email == obj.Email);
            if (ModelState.IsValid)
            {
                if (userDetails == null)
                {
                    TempData["error"] = "User is not Registered";
                }

                if (userDetails.Password == obj.Password)
                {
                    TempData["success"] = "Login As Admin Successfully";
                    return RedirectToAction("AdminLandingPage", "Admin");
                }
                else
                {
                    TempData["error"] = "opps! Password is wrong";
                    return View(obj);
                }
            }
            else
            {
                TempData["error"] = "Please Enter proper Details";
                return View(obj);
            }

        }*/

        public IActionResult UserAdminTab()
        {
            AdminVM model = new AdminVM();
            var userDetails = _unitOfWork.User.GetUserDetails().Where(user => user.DeletedAt == null);
            var userEmail = HttpContext.Session.GetString("userEmail");
            var loggedinUser = _unitOfWork.User.GetFirstOrDefault(user => user.Email == userEmail);
            if(userEmail == null)
            {  
                return RedirectToAction("login", "Authentication");
            }
            else if(loggedinUser.Role == "USER")
            {
                return RedirectToAction("UnAuthorize", "Authentication");
            }
            else
            {
                model.LoggedInUser = loggedinUser;
                model.Users = userDetails;
                return View(model);
            }
            
        }

        public IActionResult CMSPageAdminTab()
        {
            var model = new AdminVM();
            var CMSPageDetails = _unitOfWork.CMSPage.GetAllCMSPageDetails().Where(cms => cms.DeletedAt == null);
            var userEmail = HttpContext.Session.GetString("userEmail");
            var loggedinUser = _unitOfWork.User.GetFirstOrDefault(user => user.Email == userEmail);
            if (userEmail == null)
            {
                return RedirectToAction("login", "Authentication");
            }
            else if (loggedinUser.Role == "USER")
            {
                return RedirectToAction("UnAuthorize", "Authentication");
            }
            else
            {
                model.LoggedInUser = loggedinUser;
                model.CmsPages = CMSPageDetails;
                return View(model);
            }
          
        }

        public IActionResult MissionAdminTab()
        {
            AdminVM model = new AdminVM();
            var missions = _unitOfWork.Mission.GetMissionDetails().Where(mission => mission.DeletedAt == null);
            var userEmail = HttpContext.Session.GetString("userEmail");
            var loggedinUser = _unitOfWork.User.GetFirstOrDefault(user => user.Email == userEmail);
            if (userEmail == null)
            {
                return RedirectToAction("login", "Authentication");
            }
            else if (loggedinUser.Role == "USER")
            {
                return RedirectToAction("UnAuthorize", "Authentication");
            }
            else
            {
                model.LoggedInUser = loggedinUser;
                model.missions = missions;
                return View(model);
            }
            
        }

        public IActionResult MissionThemeAdminTab()
        {
            AdminVM model = new AdminVM();
            var missionThemeDetails = _unitOfWork.Theme.GetThemeDetails();
            var userEmail = HttpContext.Session.GetString("userEmail");
            var loggedinUser = _unitOfWork.User.GetFirstOrDefault(user => user.Email == userEmail);
            if (userEmail == null)
            {
                return RedirectToAction("login", "Authentication");
            }
            else if (loggedinUser.Role == "USER")
            {
                return RedirectToAction("UnAuthorize", "Authentication");
            }
            else
            {
                model.LoggedInUser = loggedinUser;
                model.missionThemes = missionThemeDetails;
                return View(model);
            }
           
        }
        public IActionResult MissionSkillsAdminTab()
        {
            AdminVM model = new AdminVM();
            var skills = _unitOfWork.Skill.GetSkillDetails();
            var userEmail = HttpContext.Session.GetString("userEmail");
            var loggedinUser = _unitOfWork.User.GetFirstOrDefault(user => user.Email == userEmail);
            if (userEmail == null)
            {
                return RedirectToAction("login", "Authentication");
            }
            else if (loggedinUser.Role == "USER")
            {
                return RedirectToAction("UnAuthorize", "Authentication");
            }
            else
            {
                model.LoggedInUser = loggedinUser;
                model.skills = skills;
                return View(model);
            }
        }

        public IActionResult MissionApplicationAdminTab()
        {
            AdminVM model = new AdminVM();
            var missionApplicationsDetails = _unitOfWork.MissionApplication.GetMissionApplications();
            var userEmail = HttpContext.Session.GetString("userEmail");
            var loggedinUser = _unitOfWork.User.GetFirstOrDefault(user => user.Email == userEmail);
            if (userEmail == null)
            {
                return RedirectToAction("login", "Authentication");
            }
            else if (loggedinUser.Role == "USER")
            {
                return RedirectToAction("UnAuthorize", "Authentication");
            }
            else
            {
                model.LoggedInUser = loggedinUser;
                model.missionApplication = missionApplicationsDetails;
                return View(model);
            }
        }

        public IActionResult StoryAdminTab()
        {
            AdminVM model = new AdminVM();
            var storiesDetails = _unitOfWork.Story.GetStoryDetails().Where(story => story.DeletedAt == null);
            var userEmail = HttpContext.Session.GetString("userEmail");
            var loggedinUser = _unitOfWork.User.GetFirstOrDefault(user => user.Email == userEmail);
            if (userEmail == null)
            {
                return RedirectToAction("login", "Authentication");
            }
            else if (loggedinUser.Role == "USER")
            {
                return RedirectToAction("UnAuthorize", "Authentication");
            }
            else
            {
                model.LoggedInUser = loggedinUser;
                model.stories = storiesDetails;
                return View(model);
            }
        }

        public IActionResult BannerManagementAdminTab()
        {
            AdminVM model = new AdminVM();
            var userEmail = HttpContext.Session.GetString("userEmail");
            var loggedinUser = _unitOfWork.User.GetFirstOrDefault(user => user.Email == userEmail);
            if (userEmail == null)
            {
                return RedirectToAction("login", "Authentication");
            }
            else if (loggedinUser.Role == "USER")
            {
                return RedirectToAction("UnAuthorize", "Authentication");
            }
            else
            {
                model.LoggedInUser = loggedinUser;
                model.banner = _unitOfWork.Banner.GetAll().Where(story => story.DeletedAt == null);
                return View(model);
            }
        }

        public IActionResult DeleteRecord(long userId = 0, long CmsPageId = 0, long missionId = 0,long storyId = 0, long bannerId=0)
        {
            if (userId != 0)
            {
                var user = _unitOfWork.User.GetFirstOrDefault(user => user.UserId == userId);
                user.Status = 0;
                user.DeletedAt = DateTime.Now;
                _unitOfWork.User.Update(user);
                _unitOfWork.save();

                return RedirectToAction("UserAdminTab");
            }
            if (CmsPageId != 0)
            {
                var cmspageDetail = _unitOfWork.CMSPage.GetFirstOrDefault(cms => cms.CmsPageId == CmsPageId);
                cmspageDetail.Status = 0;
                cmspageDetail.DeletedAt = DateTime.Now;
                _unitOfWork.CMSPage.Update(cmspageDetail);
                _unitOfWork.save();

                return RedirectToAction("CMSPageAdminTab");
            }
            if (missionId != 0)
            {
                var missionDetail = _unitOfWork.Mission.GetFirstOrDefault(mission => mission.MissionId == missionId);
                missionDetail.Status = 0;
                missionDetail.DeletedAt = DateTime.Now;
                _unitOfWork.Mission.Update(missionDetail);
                _unitOfWork.save();

                return RedirectToAction("MissionAdminTab");
            }
            if(storyId != 0)
            {
                var storyDetails = _unitOfWork.Story.GetFirstOrDefault(story => story.StoryId == storyId);
                storyDetails.Status = "DECLINED";
                storyDetails.DeletedAt = DateTime.Now;
                _unitOfWork.Story.Update(storyDetails);
                _unitOfWork.save();
                return RedirectToAction("StoryAdminTab");
            }
            if (bannerId != 0)
            {
                var bannerDetails = _unitOfWork.Banner.GetFirstOrDefault(banner => banner.BannerId == bannerId);              
                bannerDetails.DeletedAt = DateTime.Now;
                _unitOfWork.Banner.Update(bannerDetails);
                _unitOfWork.save();
                return RedirectToAction("BannerManagementAdminTab");
            }



            return RedirectToAction("UserAdminTab");


        }


        public IActionResult Approve(long missionAppId = 0, long skillId = 0, long missionThemeId = 0,long storyId = 0, long bannerId = 0)
        {
            if (missionAppId != 0)
            {
                var missionApplicationDetail = _unitOfWork.MissionApplication.GetFirstOrDefault(missionapp => missionapp.MissionApplicationId == missionAppId);
                missionApplicationDetail.ApprovalStatus = "APPROVED";
                missionApplicationDetail.UpdatedAt = DateTime.Now;
                _unitOfWork.MissionApplication.Update(missionApplicationDetail);
                _unitOfWork.save();
                return RedirectToAction("MissionApplicationAdminTab", "Admin");
            }
            if (skillId != 0)
            {
                var skillDetail = _unitOfWork.Skill.GetFirstOrDefault(skill => skill.SkillId == skillId);
                skillDetail.Status = true;
                skillDetail.UpdatedAt = DateTime.Now;
                _unitOfWork.Skill.Update(skillDetail);
                _unitOfWork.save();
                return RedirectToAction("MissionSkillsAdminTab", "Admin");
            }

            if (missionThemeId != 0)
            {
                var missionThemeDetails = _unitOfWork.Theme.GetFirstOrDefault(missiontheme => missiontheme.MissionThemeId == missionThemeId);
                missionThemeDetails.Status = true;
                missionThemeDetails.UpdatedAt = DateTime.Now;
                _unitOfWork.Theme.Update(missionThemeDetails);
                _unitOfWork.save();
                return RedirectToAction("MissionThemeAdminTab");

            }
            if (storyId != 0)
            {
                var storyDetails = _unitOfWork.Story.GetFirstOrDefault(story => story.StoryId == storyId);
                storyDetails.Status = "PUBLISHED";
                storyDetails.UpdatedAt = DateTime.Now;
                _unitOfWork.Story.Update(storyDetails);
                _unitOfWork.save();
                return RedirectToAction("StoryAdminTab");
            }
            return RedirectToAction("MissionApplicationAdminTab", "Admin");

        }
        public IActionResult DisApprove(long missionAppId = 0, long skillId = 0, long missionThemeId = 0, long storyId=0)
        {
            if (missionAppId != 0)
            {
                var missionApplicationDetail = _unitOfWork.MissionApplication.GetFirstOrDefault(missionapp => missionapp.MissionApplicationId == missionAppId);
                missionApplicationDetail.ApprovalStatus = "DECLINE";
                missionApplicationDetail.UpdatedAt = DateTime.Now;
                _unitOfWork.MissionApplication.Update(missionApplicationDetail);
                _unitOfWork.save();
                return RedirectToAction("MissionApplicationAdminTab", "Admin");

            }
            if (skillId != 0)
            {
                var skillDetail = _unitOfWork.Skill.GetFirstOrDefault(skill => skill.SkillId == skillId);
                skillDetail.Status = false;
                skillDetail.UpdatedAt = DateTime.Now;
                _unitOfWork.Skill.Update(skillDetail);
                _unitOfWork.save();
                return RedirectToAction("MissionSkillsAdminTab", "Admin");
            }
            if (missionThemeId != 0)
            {
                var missionThemeDetails = _unitOfWork.Theme.GetFirstOrDefault(missiontheme => missiontheme.MissionThemeId == missionThemeId);
                missionThemeDetails.Status = false;
                missionThemeDetails.UpdatedAt = DateTime.Now;
                _unitOfWork.Theme.Update(missionThemeDetails);
                _unitOfWork.save();

                return RedirectToAction("MissionThemeAdminTab");

            }
            if(storyId != 0)
            {
                var storyDetails = _unitOfWork.Story.GetFirstOrDefault(story => story.StoryId== storyId);
                storyDetails.Status = "DECLINED";
                storyDetails.UpdatedAt = DateTime.Now;
                _unitOfWork.Story.Update(storyDetails);
                _unitOfWork.save();
                return RedirectToAction("StoryAdminTab");
            }
            return RedirectToAction("MissionApplicationAdminTab", "Admin");
        }

        public void AddSkill(string skillName)
        {
            var skill = new Skill();
            skill.SkillName = skillName;

            _unitOfWork.Skill.Add(skill);
            _unitOfWork.save();


        }

        public void AddMissionThemeName(string MissionThemeName)
        {
            var missionTheme = new MissionTheme();
            missionTheme.Title = MissionThemeName;

            _unitOfWork.Theme.Add(missionTheme);
            _unitOfWork.save();



        }

        public IActionResult AddCMSPage()
        {
            return PartialView("_CMSPageAddAndEdit");
        }

        public IActionResult EditCMSPage(long cmspageID)
        {
            AdminVM admin = new AdminVM();
            admin.ParticularCMSPage = _unitOfWork.CMSPage.GetFirstOrDefault(cms => cms.CmsPageId == cmspageID);

            return PartialView("_CMSPageAddAndEdit", admin);
        }
        [HttpPost]
        public IActionResult EditCMSPage(AdminVM model)
        {
            AdminVM admin = new AdminVM();
            if (model.ParticularCMSPage.CmsPageId != 0)
            {
                var AlreadyExistedCMSPage = _unitOfWork.CMSPage.GetFirstOrDefault(cms => cms.CmsPageId == model.ParticularCMSPage.CmsPageId);
                AlreadyExistedCMSPage.Description = model.ParticularCMSPage.Description;
                AlreadyExistedCMSPage.Slug = model.ParticularCMSPage.Slug;
                AlreadyExistedCMSPage.Title = model.ParticularCMSPage.Title;
                AlreadyExistedCMSPage.Status = model.ParticularCMSPage.Status;

                _unitOfWork.CMSPage.Update(AlreadyExistedCMSPage);
                _unitOfWork.save();
            }
            else
            {
                _unitOfWork.CMSPage.Add(model.ParticularCMSPage);
                _unitOfWork.save();
            }


            return RedirectToAction("CMSPageAdminTab", "Admin");
        }


        public IActionResult AddUser()
        {
            AdminVM adduser = new AdminVM();
            adduser.cities = _unitOfWork.City.GetAll().Select(city => new SelectListItem
            {
                Text = city.Name,
                Value = city.CityId.ToString(),
            });
            adduser.countries = _unitOfWork.Country.GetCountriesDetails().Select(country => new SelectListItem
            {
                Text = country.Name,
                Value = country.CountryId.ToString()
,
            });
            return PartialView("_UserAddAndEdit", adduser);
        }


        [HttpPost]
        public IActionResult AddUser(AdminVM model)
        {
            if (model != null)
            {
                if (model.particularUser.UserId != 0)
                {
                    var editUser = _unitOfWork.User.GetFirstOrDefault(user => user.UserId == model.particularUser.UserId);
                    editUser.FirstName = model.particularUser.FirstName;
                    editUser.LastName = model.particularUser.LastName;
                    editUser.Email = model.particularUser.Email;
                    editUser.PhoneNumber = model.particularUser.PhoneNumber;
                    editUser.EmployeeId = model.particularUser.EmployeeId;
                    editUser.Department = model.particularUser.Department;
                    editUser.CityId = model.particularUser.CityId;
                    editUser.CountryId = model.particularUser.CountryId;
                    editUser.Status = model.particularUser.Status;
                    editUser.Role = model.particularUser.Role;
                    editUser.UpdatedAt = DateTime.Now;
                    _unitOfWork.User.Update(editUser);

                }
                else
                { 
                    var userEmails = _unitOfWork.User.GetAll().Select(user => user.Email);
                    if(userEmails.Contains(model.particularUser.Email) ){
                        TempData["error"] = "Ops !Email Id is Already Registered!";
                        return RedirectToAction("UserAdminTab", "Admin");
                    }
                    else
                    {
                        model.particularUser.Password = @model.particularUser.FirstName + "@CI123";
                        _unitOfWork.User.Add(model.particularUser);
                    }
                   
                }
                _unitOfWork.save();
            }
            return RedirectToAction("UserAdminTab", "Admin");
        }

        public IActionResult EditUser(long UserId)
        {
            

            var AlreadyUser = _unitOfWork.User.GetFirstOrDefault(user => user.UserId == UserId);
            AdminVM user = new AdminVM();
            user.cities = _unitOfWork.City.GetAll().Select(city => new SelectListItem
            {
                Text = city.Name,
                Value = city.CityId.ToString(),
            });
            user.countries = _unitOfWork.Country.GetCountriesDetails().Select(country => new SelectListItem
            {
                Text = country.Name,
                Value = country.CountryId.ToString()
,
            });
            user.particularUser = AlreadyUser;

            return PartialView("_UserAddAndEdit", user);
        }

        [HttpGet]
        public IActionResult AddMission()
        {
            AdminVM mission = new AdminVM();
            mission.cities = _unitOfWork.City.GetAll().Select(city => new SelectListItem
            {
                Text = city.Name,
                Value = city.CityId.ToString(),
            });
            mission.countries = _unitOfWork.Country.GetCountriesDetails().Select(country => new SelectListItem
            {
                Text = country.Name,
                Value = country.CountryId.ToString()
,
            });
            mission.themes = _unitOfWork.Theme.GetThemeDetails().Select(theme => new SelectListItem
            {
                Text = theme.Title,
                Value = theme.MissionThemeId.ToString()
            });
            mission.skills = _unitOfWork.Skill.GetAll();
            return PartialView("_MissionPageAddAndEdit", mission);
        }


        public IActionResult EditMission(long MissionId)
        {
            

            AdminVM mission = new AdminVM();
            mission.particularMission = _unitOfWork.Mission.GetMissionByMissionId(MissionId);
            mission.skills = _unitOfWork.Skill.GetAll();
            mission.cities = _unitOfWork.City.GetAll().Select(city => new SelectListItem
            {
                Text = city.Name,
                Value = city.CityId.ToString(),
            });
            mission.countries = _unitOfWork.Country.GetCountriesDetails().Select(country => new SelectListItem
            {
                Text = country.Name,
                Value = country.CountryId.ToString()
,
            });
            mission.GoalMission = _unitOfWork.GoalMission.GetFirstOrDefault(goal => goal.MissionId == MissionId);
            mission.themes = _unitOfWork.Theme.GetThemeDetails().Select(theme => new SelectListItem
            {
                Text = theme.Title,
                Value = theme.MissionThemeId.ToString()
            });
          
            return PartialView("_MissionPageAddAndEdit", mission);
        }




        [HttpPost]
        public IActionResult AddMission(AdminVM model, List<int> skills, string videourls, List<IFormFile> files, List<IFormFile> documents)
        {
            var rootpath = _webHostEnvironment.WebRootPath;
            var imagefilePaths = new List<string>();
            var documentfilePaths = new List<string>();            
            var images = _unitOfWork.MissionMedia.GetAll();
          
            foreach (var formFile in files)
            {
                var imagename = formFile.FileName;
                MissionMedium mediaobj = new MissionMedium();
                if (!images.Any(image => image.MediaPath.Contains(imagename)))
                {
                    // full path to file in temp location
                    var newName = "MissionMedia -" + DateTime.Now.ToString("ddMMyyyyffffff") + "." + formFile.ContentType.Split("/")[1];
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/MissionImages", newName);
                    imagefilePaths.Add(filePath);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        formFile.CopyTo(stream);
                    }
                
                mediaobj.MediaPath = "/images/MissionImages/" + newName;
                mediaobj.MediaType = "PNG";
                mediaobj.DefaultMissionMedia = 1;
                mediaobj.MediaName = newName;
                /*mediaobj.MissionId = model.particularMission.MissionId;*/
                /*story.particularStory.StoryMedia.Add(mediaobj);*/
                model.particularMission.MissionMedia.Add(mediaobj);
                }
            }
            var docs = _unitOfWork.MissionDocument.GetAll();

            foreach (var documentfiles in documents)
            {
                var docfile = documentfiles.FileName;
                MissionDocument documentObj = new MissionDocument();
                if (!docs.Any(document => document.DocumentPath.Contains(docfile)))
                {
                    var newName = "MissionDocument -" + DateTime.Now.ToString("ddMMyyyyffffff") + "." + documentfiles.ContentType.Split("/")[1];
                    // full path to file in temp location
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/MissionDocuments", newName);
                    documentfilePaths.Add(filePath);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        documentfiles.CopyTo(stream);
                    }
               
                documentObj.DocumentPath = "/images/MissionDocuments/" + newName;
                documentObj.DocumentType = documentfiles.ContentType.Split("/")[1];
                documentObj.DocumentName = newName;
                /*mediaobj.MissionId = model.particularMission.MissionId;*/
                /*story.particularStory.StoryMedia.Add(mediaobj);*/

                model.particularMission.MissionDocuments.Add(documentObj);
                }
            }

            foreach (var skill in skills)
            {
                MissionSkill missionSkill = new MissionSkill();              
                missionSkill.SkillId = skill;

                if (model.particularMission.MissionId > 0)
                {
                    var missionSkills = _unitOfWork.MissionSkill.GetAll().Where(m => m.MissionId == model.particularMission.MissionId);
                    if (!missionSkills.Any(s => s.SkillId == skill))
                    {
                        model.particularMission.MissionSkills.Add(missionSkill);
                    }
                }
                else
                {
                    model.particularMission.MissionSkills.Add(missionSkill);
                }
            }

            if(model.particularMission.MissionId > 0)
            {
                var missionSkills = _unitOfWork.MissionSkill.GetAll().Where(m => m.MissionId == model.particularMission.MissionId);
                foreach (var skill in missionSkills)
                {
                    if (!skills.Contains(skill.SkillId))
                    {
                        _unitOfWork.MissionSkill.Remove(skill);
                    }
                }
            

            /*if (model.particularMission.MissionId > 0)
            {*/
                foreach (var image in images.Where(i => i.MissionId == model.particularMission.MissionId && i.MediaType != "URL"))
                {
                   /* if (!documents.Any(i => image.MediaPath.Contains(i.FileName)))*/
                        if (!files.Any(i => i.FileName.Contains(image.MediaPath)))
                        {
                        var filepath = rootpath + image.MediaPath;
                        FileInfo fileInfo = new FileInfo(filepath);
                        fileInfo.Delete();
                        _unitOfWork.MissionMedia.Remove(image);
                    }

                }
            /*}
*/

            /*if (model.particularMission.MissionId > 0)
            {*/
                    foreach (var doc in docs.Where(i => i.MissionId == model.particularMission.MissionId))
                    {
                        if (!documents.Any(i => doc.DocumentPath.Contains(i.FileName)))
                        {
                            var filepath = rootpath + doc.DocumentPath;
                            FileInfo fileInfo = new FileInfo(filepath);
                            fileInfo.Delete();
                            _unitOfWork.MissionDocument.Remove(doc);
                        }

                    }
                /*}*/
            }



            string[] arr;
            if (videourls != null)
            {
                if (videourls.Contains(" "))
                {
                    arr = videourls.Split(" ", StringSplitOptions.RemoveEmptyEntries);
                }
                else if (videourls.Contains(","))
                {
                    arr = videourls.Split(",",StringSplitOptions.RemoveEmptyEntries);
                }
                else
                {
                    arr = videourls.Split("\n", StringSplitOptions.RemoveEmptyEntries);
                }
                foreach (var video in arr)
                {
                    MissionMedium missionvideos = new MissionMedium
                    {
                        MediaPath = video,
                        MediaType = "URL",
                        MediaName = "YoutubeVideo"
                    };
                    model.particularMission.MissionMedia.Add(missionvideos);
                }
              
            }


            if (model.particularMission.MissionId != 0)
            {
                
                
                var alreadyMission = _unitOfWork.Mission.GetMissionByMissionId(model.particularMission.MissionId);
                alreadyMission.ThemeId = model.particularMission.ThemeId;
                alreadyMission.CityId = model.particularMission.CityId;
                alreadyMission.CountryId = model.particularMission.CountryId;
                alreadyMission.Title = model.particularMission.Title;
                alreadyMission.ShortDescription = model.particularMission.ShortDescription;
                alreadyMission.Description = model.particularMission.Description;
                alreadyMission.StartDate = model.particularMission.StartDate;
                alreadyMission.EndDate = model.particularMission.EndDate;
                alreadyMission.Deadline = model.particularMission.Deadline;
                alreadyMission.TotalSeats = model.particularMission.TotalSeats;
                alreadyMission.MissionType = model.particularMission.MissionType;
                alreadyMission.Status = model.particularMission.Status;
                alreadyMission.OrganizationName = model.particularMission.OrganizationName;
                alreadyMission.OrganizationDetails = model.particularMission.OrganizationDetails;
                alreadyMission.Availability = model.particularMission.Availability;
                alreadyMission.UpdatedAt = DateTime.Now;

                
                if(model.particularMission.MissionType =="GOAL")
                {
                    var goalmission = _unitOfWork.GoalMission.GetFirstOrDefault(mission => mission.MissionId == model.particularMission.MissionId);
                    goalmission.GoalObjectiveText = model.GoalMission.GoalObjectiveText;
                    goalmission.GoalValue = model.GoalMission.GoalValue;
                    goalmission.UpdatedAt = DateTime.Now;
                    alreadyMission.GoalMissions.Add(goalmission);
                }
                
                foreach(var image in model.particularMission.MissionMedia)
                {   
                    alreadyMission.MissionMedia.Add(image);
                }

                foreach(var document in model.particularMission.MissionDocuments)
                {
                    alreadyMission.MissionDocuments.Add(document);
                }
                foreach(var skill in model.particularMission.MissionSkills)
                {
                    alreadyMission.MissionSkills.Add(skill);
                }
                _unitOfWork.Mission.Update(alreadyMission);
            }
            else
            {
                GoalMission goalmission = new GoalMission
                {
                    GoalObjectiveText = model.GoalMission.GoalObjectiveText,
                    GoalValue = model.GoalMission.GoalValue
                };
                model.particularMission.GoalMissions.Add(goalmission);
                _unitOfWork.Mission.Add(model.particularMission);
            }
            _unitOfWork.save();
            return RedirectToAction("MissionAdminTab", "Admin");
        }

        public IActionResult AddBanner()
        {
            return PartialView("_BannerManagementAddAndEdit");
        }
        public IActionResult EditBanner(long BannerId)
        {
            AdminVM model = new AdminVM();
            var BannerDetail = _unitOfWork.Banner.GetFirstOrDefault(banner => banner.BannerId == BannerId);
            model.particularBanner = BannerDetail;
            return PartialView("_BannerManagementAddAndEdit",model);
        }


        [HttpPost]
        public IActionResult AddBanner(AdminVM banner,IFormFile files)
        {
            if(banner != null)
            {
                if(files != null) { 
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/BannerImages", files.FileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        files.CopyTo(stream);
                    }
                        if (banner.particularBanner.BannerId != 0)
                        {
                            var bannerdetail = _unitOfWork.Banner.GetFirstOrDefault(ban => ban.BannerId == banner.particularBanner.BannerId);
                            bannerdetail.Text = banner.particularBanner.Text;
                            bannerdetail.SortOrder = banner.particularBanner.SortOrder;
                            bannerdetail.Image = "/images/BannerImages/" + files.FileName;
                            bannerdetail.UpdatedAt = DateTime.Now;
                            _unitOfWork.Banner.Update(bannerdetail);

                        }
                        else
                        {
                            banner.particularBanner.Image = "/images/BannerImages/" + files.FileName;
                            _unitOfWork.Banner.Add(banner.particularBanner);

                        }
                }
                else
                {
                        var bannerdetail = _unitOfWork.Banner.GetFirstOrDefault(banner => banner.BannerId == banner.BannerId);
                        bannerdetail.Text = banner.particularBanner.Text;
                        bannerdetail.SortOrder = banner.particularBanner.SortOrder;
                        bannerdetail.UpdatedAt = DateTime.Now;
                        _unitOfWork.Banner.Update(bannerdetail);
                }
               
                _unitOfWork.save();
                
            }

            return RedirectToAction("BannerManagementAdminTab", "Admin");
        }


        public IActionResult ViewStoryAdmin(long StoryId)
        {
            /* return PartialView("_")*/
            var storyDetail = _unitOfWork.Story.getStoryById(StoryId);

            AdminVM story = new AdminVM();
            story.particularStory = storyDetail;
            return PartialView("_StoryViewAdminPage", story);
        /*    return RedirectToAction("BannerManagementAdminTab", "Admin");*/
        }
       

    }
}
