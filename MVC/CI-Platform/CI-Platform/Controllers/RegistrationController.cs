using CI_Platform.Data;
using Microsoft.AspNetCore.Mvc;

namespace CI_Platform.Controllers
{
    
    public class RegistrationController : Controller
    {
        public IActionResult Register()
        {
            return View();
        }
    }
}
