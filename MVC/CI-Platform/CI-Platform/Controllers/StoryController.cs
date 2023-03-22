using CI_Platfrom.Entities.Models.ViewModel;
using CI_Platfrom.Repository.Interface;
using Microsoft.AspNetCore.Mvc;

namespace CI_Platform.Controllers
{
    public class StoryController : Controller
    {
        private readonly IUnitOfWorkRepository _unitOfWork;
        public StoryController(IUnitOfWorkRepository unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }
        public IActionResult StoryListingPage()
        {
            StoryVM GetStories = getAllStory();
            return View(GetStories);
        }


        public StoryVM getAllStory()
        {
            var emailFromSession = HttpContext.Session.GetString("userEmail");
            StoryVM story = new StoryVM();
            story.Story = _unitOfWork.Story.GetStoryDetails().ToList();
            story.Mission = _unitOfWork.Mission.GetMissionDetails().ToList();
            story.User = _unitOfWork.User.GetUserDetails().ToList();
            story.MissionTheme = _unitOfWork.Theme.GetThemeDetails().ToList();
            story.user = _unitOfWork.User.GetFirstOrDefault(e => e.Email == emailFromSession);

                return story;
        }

        public IActionResult AddYourStoryPage()
        {
            StoryVM GetStories = getAllStory();
            return View(GetStories);
        }

    }
}
