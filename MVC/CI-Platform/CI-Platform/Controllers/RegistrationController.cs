
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
    }
}
