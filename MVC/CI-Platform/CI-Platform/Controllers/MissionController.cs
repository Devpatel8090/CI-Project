using Microsoft.AspNetCore.Mvc;

namespace CI_Platform.Controllers
{
    public class MissionController : Controller
    {
        public IActionResult LandingPage()
        {
            return View();
        }

        public IActionResult VolunteeringPage()
        {
            return View();
        }
    }
}
