using Microsoft.AspNetCore.Mvc;

namespace CI_PLATFORM.Controllers
{
    public class AuthenticationController : Controller
    {
        public IActionResult login()
        {
            return View();
        }
    }
}
