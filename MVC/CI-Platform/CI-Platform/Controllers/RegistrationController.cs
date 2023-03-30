
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

        public RegistrationController(/*CiPlatformContext db*/IUnitOfWorkRepository unitOfWork)
        {
            //_db = db;
            _unitOfWork = unitOfWork;
        }

        public IActionResult Register()
        {
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
            return View();
        }


        public IActionResult UserProfile()
        {
            ProfileVM profileDetails = new ProfileVM();
            var emailFromSession = HttpContext.Session.GetString("userEmail");
            profileDetails.user= _unitOfWork.User.GetFirstOrDefault(e => e.Email == emailFromSession);
            profileDetails.users = _unitOfWork.User.GetUserDetails().ToList();
            profileDetails.City = _unitOfWork.City.GetCityDetails();
            profileDetails.Country = _unitOfWork.Country.GetAll();
            return View(profileDetails);
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
            if(files != null) { 

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
                _unitOfWork.User.Add(user);
                _unitOfWork.save();
            }
            }
            return RedirectToAction("UserProfile", "Registration");



        }

    }
}
