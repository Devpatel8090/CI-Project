using CI_Platfrom.Entities.Models;
using CI_Platfrom.Entities.Models.ViewModel;
using CI_Platfrom.Repository.Interface;
using Microsoft.AspNetCore.Mvc;

namespace CI_Platform.Controllers
{
    public class VolunteeringTimeSheetController : Controller
    {

        private readonly IUnitOfWorkRepository _unitOfWork;

        public VolunteeringTimeSheetController(IUnitOfWorkRepository unitOfWork)
        {
            
            _unitOfWork = unitOfWork;
        }

        public IActionResult VolunteeringTimesheet()
        {
            var emailFromSession = HttpContext.Session.GetString("userEmail");
            
            TimesheetVM timesheetDetails =  new TimesheetVM();
            timesheetDetails.user = _unitOfWork.User.GetFirstOrDefault(e => e.Email == emailFromSession);
            timesheetDetails.LogingUserMissions = _unitOfWork.MissionApplication.getUserMissions(timesheetDetails.user.UserId);

            return View(timesheetDetails);
        }

        [HttpPost]
        public JsonResult SavedReport(long missionId)
        {
            return null;
        }

        [HttpPost]
        public IActionResult AddTimeSheet(TimesheetVM timesheet)
        {   var hour = timesheet.hour;
            var minute = timesheet.minute;

           /* var time = Time(hour + (minute / 60));*/
            var timesheetObj = new Timesheet()
            {
                MissionId = timesheet.particularTimesheet.MissionId,
                UserId = timesheet.user.UserId,
                Notes = timesheet.particularTimesheet.Notes,
                DateVolunteered = timesheet.particularTimesheet.DateVolunteered,
                Action = timesheet.particularTimesheet.Action,
                /*Time = time*/


            };
            
            return RedirectToAction("VolunteeringTimesheet", " VolunteeringTimeSheetController");

        }
    }
}
