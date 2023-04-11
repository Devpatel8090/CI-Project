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
            var emailFromSession = HttpContext.Session.GetString("userEmail");
            StoryVM privacyPolicyDetails = new StoryVM();
            privacyPolicyDetails.user = _unitOfWork.User.GetFirstOrDefault(e => e.Email == emailFromSession);
            privacyPolicyDetails.CmsPages = _unitOfWork.CMSPage.GetAllCMSPageDetails();
            return View(privacyPolicyDetails);
        }
    }
}
