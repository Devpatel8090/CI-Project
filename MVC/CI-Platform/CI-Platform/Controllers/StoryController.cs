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
            StoryVM story = new StoryVM();
            story.Story = _unitOfWork.Story.GetStoryDetails().ToList();
            story.Mission = _unitOfWork.Mission.GetMissionDetails().ToList();
            story.User = _unitOfWork.User.GetUserDetails().ToList();
            story.MissionTheme = _unitOfWork.Theme.GetThemeDetails().ToList();

                return story;
        }
    }
}
