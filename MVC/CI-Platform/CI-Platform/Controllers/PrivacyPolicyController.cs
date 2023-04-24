using CI_Platfrom.Entities.Models.ViewModel;
using CI_Platfrom.Repository.Interface;
using Microsoft.AspNetCore.Mvc;

namespace CI_Platform.Controllers
{
    public class PrivacyPolicyController : Controller
    {
        private readonly IUnitOfWorkRepository _unitOfWork;

        public PrivacyPolicyController(IUnitOfWorkRepository unitOfWork)
        {

            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult PrivacyPolicy()
        {
            var ses = HttpContext.Session.GetString("userEmail");

            if (ses == null)
            {
                return RedirectToAction("login", "Authentication");
            }
            else
            {
                var emailFromSession = HttpContext.Session.GetString("userEmail");
                StoryVM privacyPolicyDetails = new StoryVM();
                privacyPolicyDetails.user = _unitOfWork.User.GetFirstOrDefault(e => e.Email == emailFromSession);
                privacyPolicyDetails.CmsPages = _unitOfWork.CMSPage.GetAllCMSPageDetails();
                return View(privacyPolicyDetails);
            }
        }

        public IActionResult Slug(long SlugId)
        {
            var ses = HttpContext.Session.GetString("userEmail");

            if (ses == null)
            {
                return RedirectToAction("login", "Authentication");
            }
            else
            {
                var user = _unitOfWork.User.GetFirstOrDefault(user => user.Email == ses);
                ProfileVM slug = new ProfileVM();
                var cmspage = _unitOfWork.CMSPage.GetFirstOrDefault(cms => cms.CmsPageId == SlugId && cms.DeletedAt == null);
                slug.CmsPage = cmspage;
                slug.user = user;
                return PartialView("_Slug", slug);
            }


        }
    }
}
