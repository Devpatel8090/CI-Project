using CI_Platfrom.Entities.Models;
using CI_Platfrom.Entities.Models.ViewModel;
using CI_Platfrom.Repository.Interface;
using Microsoft.AspNetCore.Mvc;

namespace CI_Platform.Controllers
{
    public class AdminController : Controller
    {

        private readonly IUnitOfWorkRepository _unitOfWork;

        public AdminController(IUnitOfWorkRepository unitOfWork)
        {

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

        }

        public IActionResult UserAdminTab()
        {
            AdminVM modal = new AdminVM();
            var userDetails = _unitOfWork.User.GetUserDetails().Where(user => user.DeletedAt == null);
            modal.Users = userDetails;
            return View(modal);
        }

        public IActionResult CMSPageAdminTab()
        {
            AdminVM modal = new AdminVM();
            var CMSPageDetails = _unitOfWork.CMSPage.GetAllCMSPageDetails().Where(cms => cms.DeletedAt == null);
            modal.CmsPages = CMSPageDetails;
            return View(modal);
        }

        public IActionResult MissionAdminTab()
        {
            AdminVM modal = new AdminVM();
            var missions = _unitOfWork.Mission.GetMissionDetails().Where(mission => mission.DeletedAt == null);
            modal.missions = missions;
            return View(modal);
        }

        public IActionResult MissionThemeAdminTab()
        {
            AdminVM modal = new AdminVM();
            var missionThemeDetails = _unitOfWork.Theme.GetThemeDetails();
            modal.missionThemes = missionThemeDetails;
            return View(modal);
        }
        public IActionResult MissionSkillsAdminTab()
        {
            AdminVM modal = new AdminVM();
            var skills = _unitOfWork.Skill.GetSkillDetails();
            modal.skills = skills;
            return View(modal);
        }

        public IActionResult MissionApplicationAdminTab()
        {
            AdminVM modal = new AdminVM();
            var missionApplicationsDetails = _unitOfWork.MissionApplication.GetMissionApplications();
            modal.missionApplication = missionApplicationsDetails;
            return View(modal);
        }

        public IActionResult StoryAdminTab()
        {
            AdminVM modal = new AdminVM();
            var storiesDetails = _unitOfWork.Story.GetStoryDetails().Where(story => story.DeletedAt == null);
            modal.stories = storiesDetails;
            return View(modal);

        }

        public IActionResult BannerManagementAdminTab()
        {
            return View();
        }

        public IActionResult DeleteRecord(long userId = 0, long CmsPageId = 0, long missionId = 0)
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
          

            return RedirectToAction("UserAdminTab");


        }


        public IActionResult Approve(long missionAppId = 0, long skillId = 0, long missionThemeId = 0)
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
            return RedirectToAction("MissionApplicationAdminTab", "Admin");

        }
        public IActionResult DisApprove(long missionAppId = 0,long skillId=0, long missionThemeId = 0)
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
            if(skillId != 0)
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
            var missionTheme  = new MissionTheme();
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
        public IActionResult EditCMSPage(AdminVM modal)
        {
            AdminVM admin = new AdminVM();
            if(modal.ParticularCMSPage.CmsPageId != 0)
            {
                var AlreadyExistedCMSPage = _unitOfWork.CMSPage.GetFirstOrDefault(cms => cms.CmsPageId == modal.ParticularCMSPage.CmsPageId);
                AlreadyExistedCMSPage.Description = modal.ParticularCMSPage.Description;
                AlreadyExistedCMSPage.Slug = modal.ParticularCMSPage.Slug;
                AlreadyExistedCMSPage.Title = modal.ParticularCMSPage.Title;
                AlreadyExistedCMSPage.Status = modal.ParticularCMSPage.Status;

                _unitOfWork.CMSPage.Update(AlreadyExistedCMSPage);
                _unitOfWork.save();
            }
            else
            {
                _unitOfWork.CMSPage.Add(modal.ParticularCMSPage);
                _unitOfWork.save();
            }


            return RedirectToAction("CMSPageAdminTab","Admin");
        }


        public IActionResult AddUser()
        {
            return PartialView("_UserAddAndEdit");
        }

    }
}
