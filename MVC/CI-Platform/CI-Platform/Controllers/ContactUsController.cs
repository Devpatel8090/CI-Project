using CI_Platfrom.Entities.Models;
using CI_Platfrom.Repository.Interface;
using Microsoft.AspNetCore.Mvc;

namespace CI_Platform.Controllers
{
    public class ContactUsController : Controller
    {

        private readonly IUnitOfWorkRepository _unitOfWork;

        public ContactUsController(IUnitOfWorkRepository unitOfWork)
        {

            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        public void ContactUs(ContactU obj)
        {
            
             
                ContactU contactus = new ContactU();
                contactus.Name = obj.Name;
                contactus.Message = obj.Message;
                contactus.Subject = obj.Subject;
                contactus.EmailAddress = obj.EmailAddress;
                _unitOfWork.ContactUs.Add(contactus);
                _unitOfWork.save();
                
                
            
        }
    }
}
