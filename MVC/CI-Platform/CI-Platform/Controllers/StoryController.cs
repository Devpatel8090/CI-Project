using Microsoft.AspNetCore.Mvc;

namespace CI_Platform.Controllers
{
    public class StoryController : Controller
    {
        public IActionResult StoryListingPage()
        {
            return View();
        }
    }
}
