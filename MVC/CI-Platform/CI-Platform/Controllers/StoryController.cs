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
       public async Task<IActionResult> AddYourStoryPage(StoryVM story,List<IFormFile> files)
        {
            var emailFromSession = HttpContext.Session.GetString("userEmail");
            var user = _unitOfWork.User.GetFirstOrDefault(e => e.Email == emailFromSession);
            var filePaths = new List<string>();
            foreach (var formFile in files)
            {
                StoryMedium mediaobj = new StoryMedium();
                if (formFile.Length > 0)
                {
                    // full path to file in temp location
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/StoryImages", formFile.FileName); //we are using Temp file name just for the example. Add your own file path.
                    filePaths.Add(filePath);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await formFile.CopyToAsync(stream);
                    }
                }
                mediaobj.Path = "/images/StoryImages/" + formFile.FileName;
                mediaobj.Type = "PNG";
                story.particularStory.StoryMedia.Add(mediaobj);
            }

            //Story story1 = new Story();
            StoryMedium videoMedia = new StoryMedium();
            

            var status= _unitOfWork.Story.GetFirstOrDefault(e => e.UserId == user.UserId && e.MissionId == story.particularStory.MissionId && e.Status == "DRAFT");
            if(status != null)
            {
                status.Status = "PENDING";
                status.Title = story.particularStory.Title; 
                status.Description = story.particularStory.Description;

                _unitOfWork.Story.updateStory(status);
                
                
            }
            else { 
            story.particularStory.UserId = user.UserId;
            story.particularStory.Status = "PENDING";
            _unitOfWork.Story.Add(story.particularStory);
            _unitOfWork.save();
            }




            return RedirectToAction("StoryListingPage");
        }


      

        [HttpPost]
        public void saveStory(long MissionId,string StoryTitle,string StoryDetails)
        {
            var sessionValue = HttpContext.Session.GetString("userEmail");
            var user = _unitOfWork.User.GetFirstOrDefault(e => e.Email == sessionValue);
          

            var StoryObjcet = new Story()
            {
                MissionId = MissionId,
                UserId = user.UserId,
                Status = "DRAFT",
                Title = StoryTitle,
                Description = StoryDetails,

            };

            var alreadyStoryUploaded = _unitOfWork.Story.GetFirstOrDefault(e => e.UserId == user.UserId && e.MissionId == MissionId);

            if (alreadyStoryUploaded == null)
            {
                _unitOfWork.Story.Add(StoryObjcet);
                _unitOfWork.save();
            }
            else
            {
                alreadyStoryUploaded.Title = StoryTitle;
                alreadyStoryUploaded.Description = StoryDetails;
                alreadyStoryUploaded.UpdatedAt = DateTime.Now;
                _unitOfWork.Story.Update(alreadyStoryUploaded);
                _unitOfWork.save();
            }


        }

        public IActionResult storyDetailPage(long storyId)
        {
            StoryVM story = new StoryVM();
            var emailFromSession = HttpContext.Session.GetString("userEmail");
            story.user = _unitOfWork.User.GetFirstOrDefault(e => e.Email == emailFromSession);
            story.User = _unitOfWork.User.GetUserDetails().Where(e => e.Email != emailFromSession).ToList();
            story.storyById = _unitOfWork.Story.getStoryById(storyId);
            return View(story);
        }


    }
}
