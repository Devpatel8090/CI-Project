﻿using CI_Platform.Model;
using CI_Platfrom.Entities.Models;
using CI_Platfrom.Entities.Models.ViewModel;
using CI_Platfrom.Repository.Interface;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MimeKit;
using Newtonsoft.Json.Linq;

namespace CI_Platform.Controllers
{
    public class StoryController : Controller
    {
        private readonly IUnitOfWorkRepository _unitOfWork;
        private IConfiguration _configuration;
        private readonly SMTPConfigModel _smtpconfig;
        public StoryController(IUnitOfWorkRepository unitOfWork, IOptions<SMTPConfigModel> smtpConfig, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _configuration = configuration;
            _smtpconfig = smtpConfig.Value;

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



            TempData["success"] = "Hurray! Story added for review";
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
            story.User = _unitOfWork.User.GetUserDetails().ToList();
            story.storyById = _unitOfWork.Story.getStoryById(storyId);
            story.storyById.StoryViews = story.storyById.StoryViews + 1;
            _unitOfWork.Story.updateStory(story.storyById);
            _unitOfWork.save();




            return View(story);
        }


        public void Recommendation(string inviteObj)
        {
            var parseObj = JObject.Parse(inviteObj);
            var storyId = parseObj.Value<long>("storyId");
            var userId = parseObj.Value<long>("userId");
            var toUserId = parseObj.Value<long>("toUserId");
            var mailTo = parseObj.Value<string>("toUserMail");


            var recObj = new StoryInvite()
            {
                StoryId = storyId,
                FromUserId = userId,
                ToUserId = toUserId,

            };

            var inviteLink = Url.Action("storyDetailPage", "Story", new { storyId = storyId }, Request.Scheme);
            TempData["link"] = inviteLink;

            UserEmailOptions userEmailOptions = new UserEmailOptions()
            {
                Subject = "He is invinting you to read this Story ",
                Body = "<a href=" + inviteLink + ">" + inviteLink + "</a>"
            };

            _unitOfWork.StoryInvite.Add(recObj);
            _unitOfWork.save();
            SendEmail(mailTo, userEmailOptions);

            //}



        }


        public void SendEmail(string toEmail, UserEmailOptions userEmailOptions)
        {
            var email = new MimeMessage();

            email.From.Add(new MailboxAddress(_smtpconfig.SenderDisplayName, _smtpconfig.SenderAddress));
            email.To.Add(new MailboxAddress("Jay", toEmail));

            email.Subject = userEmailOptions.Subject;
            var bodyBuilder = new BodyBuilder();

            bodyBuilder.HtmlBody = userEmailOptions.Body;
            email.Body = bodyBuilder.ToMessageBody();

            using (var smtp = new SmtpClient())
            {
                smtp.Connect("smtp.gmail.com", 465, true);

                smtp.Authenticate(_smtpconfig.UserName, _smtpconfig.Password);

                smtp.Send(email);

                smtp.Disconnect(true);
            }
        }


        [HttpPost]
        public JsonResult storyByMissionID(long missionID)
        {
            StoryVM storyModel = new StoryVM();
            var sessionValue = HttpContext.Session.GetString("userEmail");
            var user = _unitOfWork.User.GetFirstOrDefault(e => e.Email == sessionValue).UserId;

            storyModel.particularStory = _unitOfWork.Story.getUserMissions(user,missionID).FirstOrDefault(e => e.Status == "DRAFT");
            var draftedStory = storyModel.particularStory;
            
            if (draftedStory == null)
            {
                return new JsonResult("EmptyStory");
            }
            else
            {
                var storyObj = new JsonResult(new
                {
                   draftedStory.Title,
                   draftedStory.Description,
                   draftedStory.CreateAt

                });
                return storyObj;
            }
        }

      


    }
}
