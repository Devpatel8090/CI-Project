using Microsoft.AspNetCore.Mvc;

namespace CI_Platform.Controllers
{
    public class AuthenticationController : Controller
    {
        public IActionResult login()
        {
            return View();
        }

        public IActionResult forgotPassword()
        {
            return View();
        }

        public IActionResult resetPassword()
        {
            return View();
        }

        public IActionResult registration()
        {
            return View();
        }
    }
}
