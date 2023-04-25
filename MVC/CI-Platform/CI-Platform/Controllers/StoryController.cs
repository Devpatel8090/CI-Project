using CI_Platform.Model;
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
            var ses = HttpContext.Session.GetString("userEmail");

            if (ses == null)
            {
                return RedirectToAction("login", "Authentication");
            }
            else
            {
                StoryVM GetStories = getAllStory();
                return View(GetStories);
            }
        }


        public StoryVM getAllStory()
        {
            var emailFromSession = HttpContext.Session.GetString("userEmail");
            StoryVM story = new StoryVM();
            story.Story = _unitOfWork.Story.GetPublishedStoryDetails().ToList();
            story.Mission = _unitOfWork.Mission.GetMissionDetails().ToList();
            story.user = _unitOfWork.User.GetFirstOrDefault(e => e.Email == emailFromSession);
            story.LogingUserMissions = _unitOfWork.MissionApplication.getUserMissions(story.user.UserId);
            story.User = _unitOfWork.User.GetUserDetails().ToList();
            story.MissionTheme = _unitOfWork.Theme.GetThemeDetails().ToList();
            story.CmsPages = _unitOfWork.CMSPage.GetAllCMSPageDetails();



            return story;
        }

        public IActionResult AddYourStoryPage()
        {
            var ses = HttpContext.Session.GetString("userEmail");

            if (ses == null)
            {
                return RedirectToAction("login", "Authentication");
            }
            else
            {
                StoryVM GetStories = getAllStory();
                /*   GetStories.particularStory = _unitOfWork.Story.GetFirstOrDefault(e => e.UserId == GetStories.user.UserId && e.Status == "DRAFT");*/
                return View(GetStories);
            }
        }



        [HttpPost]
        public async Task<IActionResult> AddYourStoryPage(StoryVM story, List<IFormFile> files)
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
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/StoryImages", formFile.FileName);
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



            var status = _unitOfWork.Story.GetFirstOrDefault(e => e.UserId == user.UserId && e.MissionId == story.particularStory.MissionId && e.Status == "DRAFT");
            if (status != null)
            {
                status.Status = "PENDING";
                status.Title = story.particularStory.Title;
                status.Description = story.particularStory.Description;

                _unitOfWork.Story.updateStory(status);


            }
            else
            {
                story.particularStory.UserId = user.UserId;
                story.particularStory.Status = "PENDING";
                _unitOfWork.Story.Add(story.particularStory);
                _unitOfWork.save();
            }



            TempData["success"] = "Hurray! Story added for review";
            return RedirectToAction("StoryListingPage");
        }




        [HttpPost]
        public long saveStory(IFormFileCollection totalfiles, string addStoryObj)
        {
            var sessionValue = HttpContext.Session.GetString("userEmail");
            var user = _unitOfWork.User.GetFirstOrDefault(e => e.Email == sessionValue);




            //var StoryObjcet = new Story()
            //{
            //    MissionId = MissionId,
            //    UserId = user.UserId,
            //    Status = "DRAFT",
            //    Title = StoryTitle,
            //    Description = StoryDetails,

            //};

            //var alreadyStoryUploaded = _unitOfWork.Story.GetFirstOrDefault(e => e.UserId == user.UserId && e.MissionId == MissionId);

            //if (alreadyStoryUploaded == null)
            //{
            //    _unitOfWork.Story.Add(StoryObjcet);
            //    _unitOfWork.save();
            //}
            //else
            //{
            //    alreadyStoryUploaded.Title = StoryTitle;
            //    alreadyStoryUploaded.Description = StoryDetails;
            //    alreadyStoryUploaded.UpdatedAt = DateTime.Now;
            //    _unitOfWork.Story.Update(alreadyStoryUploaded);
            //    _unitOfWork.save();
            //}

            var parseObj = JObject.Parse(addStoryObj);
            var missionId = parseObj.Value<long>("MissionId");
            var storyTitle = parseObj.Value<string>("StoryTitle");
            var storyDetails = parseObj.Value<string>("StoryDetails");
            var videourl = parseObj.Value<string>("StoryVideoUrl");
            JArray previousPhotoesArray = (JArray)parseObj["storyImages"];

            /*  for (int i = 0; i < previousPhotoesArray.Count; i++)
              {
                  Console.WriteLine(previousPhotoesArray[i]);
              }*/



            var alreadyStoryUploaded = _unitOfWork.Story.GetFirstOrDefault(e => e.UserId == user.UserId && e.MissionId == missionId && e.Status == "DRAFT");

            if (alreadyStoryUploaded != null)
            {

                var alreadyimageuploaded = _unitOfWork.StoryMedia.GetAll().Where(e => e.StoryId == alreadyStoryUploaded.StoryId).ToList();

                foreach (var image in alreadyimageuploaded)
                {
                    _unitOfWork.StoryMedia.Remove(image);
                    _unitOfWork.save();
                }

                for (int i = 0; i < previousPhotoesArray.Count; i++)
                {
                    StoryMedium storymedium = new StoryMedium();
                    storymedium.StoryId = alreadyStoryUploaded.StoryId;
                    storymedium.Type = "PNG";
                    storymedium.Path = previousPhotoesArray[i].ToString();
                    storymedium.UpdatedAt = DateTime.Now;
                    alreadyStoryUploaded.StoryMedia.Add(storymedium);
                }
                foreach (var formFile in totalfiles)
                {
                    StoryMedium mediaobj = new StoryMedium();
                    if (formFile.Length > 0)
                    {
                        // full path to file in temp location
                        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/StoryImages", formFile.FileName);

                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            formFile.CopyTo(stream);
                        }
                    }
                    mediaobj.Path = "/images/StoryImages/" + formFile.FileName;
                    mediaobj.Type = "PNG";


                    alreadyStoryUploaded.StoryMedia.Add(mediaobj);

                }

                var uploadedUrls = _unitOfWork.StoryMedia.GetAll().Where(url => url.Type == "URL" && url.StoryId == alreadyStoryUploaded.StoryId);
                
                foreach(var url in uploadedUrls)
                {
                    _unitOfWork.StoryMedia.Remove(url);
                    _unitOfWork.save();
                }
                string[] urlArray;
                if (videourl != null)
                {
                    if (videourl.Contains(" "))
                    {
                        urlArray = videourl.Split(" ", StringSplitOptions.RemoveEmptyEntries);
                    }
                    else if (videourl.Contains(","))
                    {
                        urlArray = videourl.Split(",", StringSplitOptions.RemoveEmptyEntries);
                    }
                    else
                    {
                        urlArray = videourl.Split("\n", StringSplitOptions.RemoveEmptyEntries);
                    }

                    foreach (var url in urlArray)
                    {
                        StoryMedium videoUrlObject = new StoryMedium();
                        videoUrlObject.Type = "URL";
                        videoUrlObject.Path = videourl;
                        alreadyStoryUploaded.StoryMedia.Add(videoUrlObject);
                    }

                }
                alreadyStoryUploaded.Title = storyTitle;
                alreadyStoryUploaded.Description = storyDetails;
                alreadyStoryUploaded.UpdatedAt = DateTime.Now;
                _unitOfWork.Story.Update(alreadyStoryUploaded);
                _unitOfWork.save();
                return alreadyStoryUploaded.StoryId;
            }
            else
            {   
                var StoryObjcet = new Story()
                {
                    MissionId = missionId,
                    UserId = user.UserId,
                    Status = "DRAFT",
                    Title = storyTitle,
                    Description = storyDetails,
                };
                foreach (var formFile in totalfiles)
                {
                    StoryMedium mediaobj = new StoryMedium();
                    if (formFile.Length > 0)
                    {
                        // full path to file in temp location
                        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/StoryImages", formFile.FileName);

                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            formFile.CopyTo(stream);
                        }
                    }
                    mediaobj.Path = "/images/StoryImages/" + formFile.FileName;
                    mediaobj.Type = "PNG";                               
                    StoryObjcet.StoryMedia.Add(mediaobj);
                }

                string[] arr;
                if (videourl != null)
                {
                    if (videourl.Contains(" "))
                    {
                        arr = videourl.Split(" ", StringSplitOptions.RemoveEmptyEntries);
                    }
                    else if (videourl.Contains(","))
                    {
                        arr = videourl.Split(",", StringSplitOptions.RemoveEmptyEntries);
                    }
                    else
                    {
                        arr = videourl.Split("\n", StringSplitOptions.RemoveEmptyEntries);
                    }

                    foreach (var url in arr)
                    {
                        StoryMedium videoUrlObject = new StoryMedium();
                        videoUrlObject.Type = "URL";
                        videoUrlObject.Path = videourl;                       
                        StoryObjcet.StoryMedia.Add(videoUrlObject);
                    }
                }
                _unitOfWork.Story.Add(StoryObjcet);
                _unitOfWork.save();
                return StoryObjcet.StoryId;
            }
       
        }


        public IActionResult storyDetailPage(long storyId)
        {
            var ses = HttpContext.Session.GetString("userEmail");

            if (ses == null)
            {
                return RedirectToAction("login", "Authentication");
            }
            else
            {
                StoryVM story = new StoryVM();
                var emailFromSession = HttpContext.Session.GetString("userEmail");
                story.user = _unitOfWork.User.GetFirstOrDefault(e => e.Email == emailFromSession);
                story.User = _unitOfWork.User.GetUserDetails().ToList();
                story.storyById = _unitOfWork.Story.getStoryById(storyId);
                story.CmsPages = _unitOfWork.CMSPage.GetAllCMSPageDetails();
                story.storyById.StoryViews = story.storyById.StoryViews + 1;
                _unitOfWork.Story.updateStory(story.storyById);
                _unitOfWork.save();
                return View(story);
            }
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


        /// <summary>
        /// for getting the drafted story details in json formate in view
        /// </summary>
        /// <param name="missionID"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult storyByMissionID(long missionID)
        {
            StoryVM storyModel = new StoryVM();
            var sessionValue = HttpContext.Session.GetString("userEmail");
            var user = _unitOfWork.User.GetFirstOrDefault(e => e.Email == sessionValue).UserId;

            
            storyModel.particularStory = _unitOfWork.Story.getUserMissions(user, missionID).FirstOrDefault(e => e.Status == "DRAFT");

            var draftedStory = storyModel.particularStory;
           /* var mission = _unitOfWork.Mission.GetFirstOrDefault(mission => mission.MissionId == missionID);
            var maxDate = mission.EndDate < DateTime.Now ? mission.EndDate.Value.ToString("yyyy-MM-dd") : DateTime.Now.ToString("yyyy-MM-dd");
            var minDate = mission.StartDate.Value.ToString("yyyy-MM-dd");*/
           /* var obj = new JsonResult(new
            {
                maxDate = maxDate,
                minDate = minDate,
            });*/

            if (draftedStory == null)
            {
                return new JsonResult("EmptyStory");
            }
            else
            {

                if (storyModel.particularStory.StoryMedia != null)
                {
                    var images = _unitOfWork.StoryMedia.GetStoryPhotoesByStoryID(storyModel.particularStory.StoryId).Select(e => e.Path).ToList();
                    var videos = _unitOfWork.StoryMedia.GetStoryVideosByStoryID(storyModel.particularStory.StoryId).Select(e => e.Path).ToList();
                    var storyObj = new JsonResult(new
                    {
                        draftedStory.Title,
                        draftedStory.Description,
                        draftedStory.CreateAt,
                        videos,
                        images,
                        

                    });
                    return storyObj;
                }

                else
                {
                    return new JsonResult("");
                }
            }
        }

        /*[HttpPost]
        public IActionResult PreviewStory(long storyID)
        {

            return storyDetailPage(storyID); ;
        }*/





    }
}
