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
        public IActionResult AdminLandingPage()
        {
            var emailFromSession = HttpContext.Session.GetString("userEmail");
            AdminVM adminDetails = new AdminVM()
            {
                Users = _unitOfWork.User.GetAll(),
                user = _unitOfWork.User.GetFirstOrDefault(e => e.Email == emailFromSession),
                missionApplication = _unitOfWork.MissionApplication.GetMissionApplications(),
                stories = _unitOfWork.Story.GetStoryDetails()/*.Where(e => e.Status == "PENDING")*/,
                missions = _unitOfWork.Mission.GetMissionDetails(),
            };


            /* adminDetails.user = _unitOfWork.User.GetFirstOrDefault(e => e.Email == emailFromSession);
             adminDetails.missionApplication = _unitOfWork.MissionApplication.GetMissionApplications();*/
            return View(adminDetails);
        }

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
                if(userDetails == null)
                {
                    TempData["error"] = "User is not Registered";
                }

                if(userDetails.Password == obj.Password) {
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
    }
}
