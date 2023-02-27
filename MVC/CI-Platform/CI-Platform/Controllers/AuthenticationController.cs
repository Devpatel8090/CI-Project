using CI_Platform.Data;
using CI_Platform.Models;
using Microsoft.AspNetCore.Mvc;

namespace CI_Platform.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly CiPlatformContext _db;
        public AuthenticationController(CiPlatformContext db)
        {
            _db = db;
        }

        [HttpGet]
        public IActionResult login()
        {
            return View();
        }


        [HttpPost]
        public IActionResult login(User user)
        {
            var userDetails = _db.Users.FirstOrDefault(e => e.Email == user.Email);
            if (userDetails == null)
            {
                return View();
            }
            else
            {
                if(userDetails.Password == user.Password)
                {
                    return RedirectToAction("LandingPage" , "Mission");
                }
            }
            return View();
        }
       

        [HttpGet]
        public IActionResult forgotPassword()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult forgotPassword(User user)
        {
            var userDetails = _db.Users.FirstOrDefault(e => e.Email == user.Email);

            if(userDetails == null)
            {
                return View();    
            }
            if (userDetails.Email == user.Email)
            {
                return RedirectToAction("resetPassword", "Authentication");
            }

            return View();
        }

        public IActionResult resetPassword()
        {
            return View();
        }

       
    }
}
