using CI_Platfrom.Entities.Models;
using CI_Platfrom.Entities.Models.ViewModel;
using CI_Platfrom.Repository.Interface;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

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
            story.user = _unitOfWork.User.GetFirstOrDefault(e => e.Email == emailFromSession);
            story.LogingUserMissions = _unitOfWork.MissionApplication.getUserMissions(story.user.UserId);
            story.User = _unitOfWork.User.GetUserDetails().ToList();
            story.MissionTheme = _unitOfWork.Theme.GetThemeDetails().ToList();
           

                return story;
        }

        public IActionResult AddYourStoryPage()
        {
            StoryVM GetStories = getAllStory();
            return View(GetStories);
        }

        [HttpPost]
        public void saveStory(Story addStoryObj)
        {
            var sessionValue = HttpContext.Session.GetString("userEmail");
            var user = _unitOfWork.User.GetFirstOrDefault(e => e.Email == sessionValue);
            //var parseObj = JObject.Parse(addStoryObj);
            //var missionId = parseObj.Value<long>("MissionId");
            //var storyTitle = parseObj.Value<string>("StoryTitle");
            //var storyDetails = parseObj.Value<string>("StoryDetails");

            //var StoryObjcet = new Story()
            //{
            //    MissionId = missionId,
            //    UserId = user.UserId,
            //    Status = "DRAFT",
            //    Title = storyTitle,
            //    Description = storyDetails,

            //};

            //var alreadyStoryUploaded = _unitOfWork.Story.GetFirstOrDefault(e => e.UserId == user.UserId && e.MissionId == missionId);

            //if(alreadyStoryUploaded == null)
            //{
            //    _unitOfWork.Story.Add(StoryObjcet);
            //    _unitOfWork.save();
            //}
            //else
            //{   alreadyStoryUploaded.Title = storyTitle;
            //    alreadyStoryUploaded.Description = storyDetails;
            //    alreadyStoryUploaded.UpdatedAt = DateTime.Now;
            //    _unitOfWork.Story.Update(alreadyStoryUploaded);
            //    _unitOfWork.save();
            //}

       
        }

    }
}
