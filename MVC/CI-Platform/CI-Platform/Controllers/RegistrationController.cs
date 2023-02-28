
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
            if(ModelState.IsValid)
            {
                if (obj.ConfirmPassword == obj.User.Password)
                {
                    _db.Users.Add(obj.User) ;
                    _db.SaveChanges();
                    return RedirectToAction("login", "Authentication");
                }
                
            }
            return View();

        }
    }
}
