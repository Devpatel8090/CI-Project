﻿

using CI_Platform.Model;
using CI_Platfrom.Entities.Data;
using CI_Platfrom.Entities.Models;
using CI_Platfrom.Entities.Models.ViewModel;
using CI_Platfrom.Repository.Interface;
using Microsoft.AspNetCore.Mvc;

namespace CI_Platform.Controllers
{
    
    public class RegistrationController : Controller
    {

        //private readonly CiPlatformContext _db;
        private readonly IUnitOfWorkRepository _unitOfWork;
        private readonly Utilities _utility ;

        public RegistrationController(/*CiPlatformContext db*/IUnitOfWorkRepository unitOfWork,Utilities utility)
        {
            //_db = db;
            _unitOfWork = unitOfWork;
            _utility = utility;
        }

        public IActionResult Register()
        {
            ViewBag.Banners = _unitOfWork.Banner.GetAll().Where(banner => banner.DeletedAt == null);
            return View();
        }


        [HttpPost]
        public IActionResult Register(ConfirmPasswordVM obj)
        
        {
            var userDetails = _unitOfWork.User.GetFirstOrDefault(e => e.Email == obj.User.Email);
            if(ModelState.IsValid)
            {
                if(userDetails != null)
                {
                    TempData["error"] = "Email is already Registered!";
                }
                else
                {
                    if (obj.ConfirmPassword == obj.User.Password)
                    {
                        obj.User.Password = _utility.Encodepass(obj.ConfirmPassword);
                        _unitOfWork.User.Add(obj.User);
                        _unitOfWork.save();
                        TempData["success"] = "Congratulation! You are Registered Now login with the same Email and Password";
                        return RedirectToAction("login", "Authentication");
                    }
                    else
                    {
                        TempData["error"] = "Password doesn't match";
                    }
                }
            }
            ViewBag.Banners = _unitOfWork.Banner.GetAll().Where(banner => banner.DeletedAt == null);
            return View();
        }


        public IActionResult UserProfile()
        {

            var ses = HttpContext.Session.GetString("userEmail");

            if (ses == null)
            {
                return RedirectToAction("login", "Authentication");
            }
            else
            {
                ProfileVM profileDetails = new ProfileVM();
                List<int> UserSkillIds = new List<int>();
                var emailFromSession = HttpContext.Session.GetString("userEmail");
                profileDetails.user = _unitOfWork.User.GetFirstOrDefault(e => e.Email == emailFromSession);
                profileDetails.Users = _unitOfWork.User.GetUserDetails().ToList();
                profileDetails.City = _unitOfWork.City.GetCityDetails();
                profileDetails.Country = _unitOfWork.Country.GetAll();
                profileDetails.CmsPages = _unitOfWork.CMSPage.GetAllCMSPageDetails();

                profileDetails.userSkill = _unitOfWork.UserSkills.GetUserSkillsByUserId(profileDetails.user.UserId);

                foreach (var userskilllist in profileDetails.userSkill)
                {
                    UserSkillIds.Add(userskilllist.SkillId);
                }
                profileDetails.skill = _unitOfWork.Skill.GetAll().Where(m => !UserSkillIds.Contains(m.SkillId));
                return View(profileDetails);
            }
        }

        [HttpPost]
        public IActionResult  UserProfile(ProfileVM userProfile, long CountryNiId, long CityNiId)
        {
            var emailFromSession = HttpContext.Session.GetString("userEmail");
            var user = _unitOfWork.User.GetFirstOrDefault(e => e.Email == emailFromSession);

            if (user != null)
            {
                user.FirstName = userProfile.user.FirstName;
                user.LastName = userProfile.user.LastName;
                user.EmployeeId = userProfile.user.EmployeeId;
                user.Title = userProfile.user.Title;
                user.Department = userProfile.user.Department;
                user.ProfileText = userProfile.user.ProfileText;
                user.WhyIVolunteer = userProfile.user.WhyIVolunteer;
                user.CountryId = CountryNiId;
                user.CityId = CityNiId;
                user.LinkedInUrl = userProfile.user.LinkedInUrl;
                user.UpdatedAt = DateTime.Now;

                _unitOfWork.User.Update(user);
                _unitOfWork.save();
                TempData["success"] = "yey!! Data has been updated Successfully";
            }
            else
            {
                _unitOfWork.User.Add(user);
                _unitOfWork.save();
                TempData["success"] = "yey! Data has been added Successfully";
            }
            return RedirectToAction("UserProfile", "Registration");
        }

        [HttpPost]
        public IActionResult saveUserProfilePhoto(IFormFile files)
        {
            var emailFromSession = HttpContext.Session.GetString("userEmail");
            var user = _unitOfWork.User.GetFirstOrDefault(e => e.Email == emailFromSession);
            if(files != null)
            { 

                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/UserAvtarImages", files.FileName); //we are using Temp file name just for the example. Add your own file path.
            
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                     files.CopyTo(stream);
                }

                if(user.Avatar != null)
                {
                    user.Avatar = "/images/UserAvtarImages/" + files.FileName;
                    _unitOfWork.User.Update(user);
                    _unitOfWork.save();
                }
                else
                {
                    user.Avatar = "/images/UserAvtarImages/" + files.FileName;
                    _unitOfWork.User.Update(user);
                    _unitOfWork.save();
                }
            }
            return RedirectToAction("UserProfile", "Registration");
        }

        [HttpPost]
        public IActionResult ChangePassword(ProfileVM profile)
        {
            
                var emailFromSession = HttpContext.Session.GetString("userEmail");
                var user = _unitOfWork.User.GetFirstOrDefault(e => e.Email == emailFromSession);
               
                var encryptedOldPassword = _utility.Encodepass(profile.OldPassword);
                var encryptedNewPassword = _utility.Encodepass(profile.NewPassword);
                var encryptedConfirmPassword = _utility.Encodepass(profile.NewPassword);

                if (encryptedOldPassword == user.Password &&  user != null)
                {
                    if (encryptedConfirmPassword == encryptedNewPassword)
                    {
                        user.Password = encryptedConfirmPassword;
                        user.UpdatedAt = DateTime.Now;
                        _unitOfWork.User.Update(user);
                        _unitOfWork.save();
                        TempData["success"] = "Yey! Password has been Changed Successfully";
                    }
                    else
                    {
                    TempData["error"] = "New Password and Cofirm Password Doesn't match";
                    }
               
                }
                else
                {
                TempData["error"] = "Please Enter Correct Current Password";
                
                }
            return RedirectToAction("UserProfile", "Registration");

        }

        public string[] AddSkills(int[] SkillIDs)
        {
            string[] skills = {}; 
            var emailFromSession = HttpContext.Session.GetString("userEmail");
            var user = _unitOfWork.User.GetFirstOrDefault(e => e.Email == emailFromSession);

            var alreadySkilledUser = _unitOfWork.UserSkills.GetUserSkillsByUserId(user.UserId);

            if(alreadySkilledUser.Count > 0)
            {
                foreach(var skill in alreadySkilledUser)
                {
                   
                    _unitOfWork.UserSkills.Remove(skill);
                }
                _unitOfWork.save();
               
            }
         
                for (var i = 0; i < SkillIDs.Length; i++)
                {
                    UserSkill userSkill = new UserSkill();
                    userSkill.UserId = user.UserId;
                    userSkill.SkillId = SkillIDs[i];
                    _unitOfWork.UserSkills.Add(userSkill);
                }
                _unitOfWork.save();
            
               var userSkills = _unitOfWork.UserSkills.GetUserSkillsByUserId(user.UserId);
               for(var i = 0; i< userSkills.Count; i++)
               {
                skills = skills.Append(userSkills[i].Skill.SkillName).ToArray();
               }

               return skills;
        }

    }
}
