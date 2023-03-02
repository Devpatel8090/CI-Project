
using CI_Platfrom.Entities.Data;
using CI_Platfrom.Entities.Models;
using CI_Platfrom.Entities.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace CI_Platform.Controllers
{
    
    public class RegistrationController : Controller
    {

        private readonly CiPlatformContext _db;

        public RegistrationController(CiPlatformContext db)
        {
            _db = db;
        }
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
    
       
        public IActionResult Register(ConfirmPasswordVM obj)
        
        {
            var userDetails = _db.Users.FirstOrDefault(e => e.Email == obj.User.Email);
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
                        _db.Users.Add(obj.User);
                        _db.SaveChanges();
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
    }
}
