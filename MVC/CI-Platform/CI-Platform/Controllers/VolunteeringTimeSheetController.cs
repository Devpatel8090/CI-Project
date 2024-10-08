﻿using CI_Platfrom.Entities.Models;
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
            var ses = HttpContext.Session.GetString("userEmail");

            if (ses == null)
            {
                return RedirectToAction("login", "Authentication");
            }
            else
            {
                var emailFromSession = HttpContext.Session.GetString("userEmail");
                TimesheetVM timesheetDetails = new TimesheetVM();
                timesheetDetails.user = _unitOfWork.User.GetFirstOrDefault(e => e.Email == emailFromSession);
                timesheetDetails.timesheets = _unitOfWork.TimeSheet.GetTimesheetDetails();
                timesheetDetails.LogingUserMissions = _unitOfWork.MissionApplication.getUserMissions(timesheetDetails.user.UserId);
                timesheetDetails.CmsPages = _unitOfWork.CMSPage.GetAllCMSPageDetails();
                return View(timesheetDetails);
            }
        }

        [HttpPost]
        public JsonResult SavedReport(long missionId)
        {
            var mission = _unitOfWork.Mission.GetFirstOrDefault(mission => mission.MissionId == missionId);
            if(mission == null)
            {
                return Json("Empty");
            }
            else
            {
                var maxDate = mission.EndDate < DateTime.Now ? mission.EndDate.Value.ToString("yyyy-MM-dd") : DateTime.Now.ToString("yyyy-MM-dd");
                var minDate = mission.StartDate.Value.ToString("yyyy-MM-dd");
                var obj = new JsonResult(new {
                    maxDate = maxDate,
                    minDate = minDate,
                });

                return obj;

            }
        }

        [HttpPost]
        public IActionResult AddTimeSheet(TimesheetVM timesheet,int hour, int minute)
        {

           
            var emailFromSession = HttpContext.Session.GetString("userEmail");
            var userId = _unitOfWork.User.GetFirstOrDefault(e => e.Email == emailFromSession).UserId;
            var alreadyCreatedTimesheet = _unitOfWork.TimeSheet.GetAll().FirstOrDefault(e => e.TimesheetId == timesheet.particularTimesheet.TimesheetId);
            if (timesheet != null) { 

            TimeOnly time = new TimeOnly(hour, minute, 00);
                var timesheetObj = new Timesheet()
                {
                    MissionId = timesheet.particularTimesheet.MissionId,
                    UserId = userId,
                    Action = timesheet.particularTimesheet.Action,
                    Notes = timesheet.particularTimesheet.Notes,
                    DateVolunteered = timesheet.particularTimesheet.DateVolunteered,
                    Time = time,
                   
                };
                if(timesheet.particularTimesheet.TimesheetId != 0)
                {
                    alreadyCreatedTimesheet.UpdatedAt = DateTime.Now;
                    alreadyCreatedTimesheet.MissionId = timesheet.particularTimesheet.MissionId;
                    alreadyCreatedTimesheet.UserId = userId;
                    alreadyCreatedTimesheet.Action = timesheet.particularTimesheet.Action;
                    alreadyCreatedTimesheet.Notes = timesheet.particularTimesheet.Notes;
                    alreadyCreatedTimesheet.DateVolunteered = timesheet.particularTimesheet.DateVolunteered;
                    alreadyCreatedTimesheet.Time = time;
                    _unitOfWork.TimeSheet.Update(alreadyCreatedTimesheet);
                    
                }
                else
                {
                        _unitOfWork.TimeSheet.Add(timesheetObj);
                }
            
                _unitOfWork.save();
                }

            return RedirectToAction("VolunteeringTimeSheet", "VolunteeringTimesheet");

        }

        

        [HttpPost]
        public IActionResult EditTimesheetTime(long timesheetId)
        {
            var obj = _unitOfWork.TimeSheet.GetTimesheetDetails().Where(e => e.TimesheetId == timesheetId).FirstOrDefault();
            TimesheetVM timesheetObj = new TimesheetVM();
            timesheetObj.particularTimesheet = obj;
            /*  if (obj != null)
              {
                  var timesheetObj = new JsonResult(new
                  {
                      obj.TimesheetId,
                      obj.Mission.Title,
                      obj.Notes,
                      obj.Time,
                      obj.DateVolunteered,

                  });
                  return timesheetObj;
              }
              else
              {
                  return new JsonResult("Emptytimesheet"); ;
              }*/
            if(timesheetObj.particularTimesheet.Mission.MissionType == "TIME") 
            { 
                    return PartialView("_EditModalTime", timesheetObj);

            }
            else
            {
                return PartialView("_EditModalGoal",timesheetObj);
            }
        }

        [HttpPost]
        public void deleteTimesheet(long timesheetId)
        {
            var alreadyCreatedTimesheet = _unitOfWork.TimeSheet.GetFirstOrDefault(timesheet => timesheet.TimesheetId == timesheetId);

            _unitOfWork.TimeSheet.Remove(alreadyCreatedTimesheet);
            _unitOfWork.save();
            
        }
    }
}
