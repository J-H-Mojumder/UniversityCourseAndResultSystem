using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UniversityCourseAndResultSystem.BLL;
using UniversityCourseAndResultSystem.Models;

namespace UniversityCourseAndResultSystem.Controllers
{
    public class ClassRoomController : Controller
    { 
        [HttpGet]
        public ActionResult Index()
        {
            ViewBag.Departments = new TeacherController().GetAllDepartmentList();
            ViewBag.ClassRooms = GetAllClassRoomList();
            ViewBag.Days = GetAllWeekDayList();
            ViewBag.Message = "";
            return View();
        }
        [HttpPost]
        public ActionResult Index(ClassRoomAllocation classRoomAllocation)
        {
            if (ModelState.IsValid)
            {
                ViewBag.Departments = new TeacherController().GetAllDepartmentList();
                ViewBag.ClassRooms = GetAllClassRoomList();
                ViewBag.Days = GetAllWeekDayList();
                classRoomAllocation.TimeFrom = classRoomAllocation.TimeFrom.Remove(2, 1);
                classRoomAllocation.TimeTo = classRoomAllocation.TimeTo.Remove(2, 1);
                ViewBag.Message = new ClassRoomManager().AllocateNewClassRoom(classRoomAllocation);
                ModelState.Clear();
                return View();
            }
            else
            {
                ViewBag.Message = "";
                ViewBag.Departments = new TeacherController().GetAllDepartmentList();
                ViewBag.ClassRooms = GetAllClassRoomList();
                ViewBag.Days = GetAllWeekDayList();
                return View();
            }
        }
         

        public List<ClassSchedule> GetAllClassScheduleViews(int departmentId)
        {
            List<ClassSchedule> queryClassScheduleList = new ClassScheduleManager().GetAllClassScheduleViewsByDeptId(departmentId);
            List<ClassSchedule> classScheduleList = new List<ClassSchedule>();

            for (var i = 0; i < queryClassScheduleList.Count;)
            {
                ClassSchedule classSchedule = queryClassScheduleList[i];
                ClassSchedule classScheduleTemporaryView = new ClassSchedule();
                classScheduleTemporaryView.CourseCode = classSchedule.CourseCode;
                classScheduleTemporaryView.CourseName = classSchedule.CourseName;
                classScheduleTemporaryView.ScheduleInfo = ("R. No : " + classSchedule.RoomName + ", " + classSchedule.DayShortName + ", " + classSchedule.TimeFrom + " - " + classSchedule.TimeTo) + "; </br>";
                int checkValue = 0;
                i++;

                for (var j = i; j < queryClassScheduleList.Count; j++)
                {
                    checkValue = 1;
                    ClassSchedule scheduleView = queryClassScheduleList[j - 1];
                    ClassSchedule scheduleView1 = queryClassScheduleList[j];

                    if (scheduleView.CourseCode == scheduleView1.CourseCode)
                    {
                        i++;
                        classScheduleTemporaryView.ScheduleInfo += ("R. No : " + scheduleView1.RoomName + ", " + scheduleView1.DayShortName + ", " +
                                                         scheduleView1.TimeFrom + " - " + scheduleView1.TimeTo + " </br>");

                        
                    }
                    else
                    {
                        if (classSchedule.RoomName == "")
                        {
                            classScheduleTemporaryView.ScheduleInfo = "Not Scheduled Yet";
                        }
                        classScheduleList.Add(classScheduleTemporaryView);
                        break;
                    }
                }
                if (checkValue == 0)
                {
                    if (classSchedule.RoomName == "")
                    {
                        classScheduleTemporaryView.ScheduleInfo = "Not Scheduled Yet";
                    }
                    classScheduleList.Add(classScheduleTemporaryView);
                }

            }
            return classScheduleList;
        }

        [HttpGet]
        public ActionResult ViewClassSchedule()
        {
            ViewBag.Departments = new TeacherController().GetAllDepartmentList();
            return View();
        }
        public JsonResult GetAllClassSchedule(int departmentId)
        {
            var getAllClassScheduleViews = GetAllClassScheduleViews(departmentId);
            return Json(getAllClassScheduleViews);
        }
        [HttpGet]
        public ActionResult UnallocatedClassRooms()
        {
            ViewData["Message"] = "";
            return View();
        }
        [HttpPost]
        public ActionResult UnallocatedClassRooms(string unallocatedClassRoom)
        {
            ViewData["Message"] = new ClassScheduleManager().UnAllocateClassRoom();
            return View();
        }

        public IEnumerable<SelectListItem> GetAllClassRoomList()
        {
            List<SelectListItem> selectedList = new List<SelectListItem>()
            {
                new SelectListItem()
                { Text = "---Select---",Value = ""},
            };
            List<ClassRoom> classRooms = new ClassRoomManager().GetAllClassRooms();
            foreach (ClassRoom classRoom in classRooms)
            {
                SelectListItem aList = new SelectListItem();
                aList.Text = classRoom.RoomNo;
                aList.Value = classRoom.Id.ToString();
                selectedList.Add(aList);
            }

            return selectedList;
        }
        public IEnumerable<SelectListItem> GetAllWeekDayList()
        {
            List<SelectListItem> selectedList = new List<SelectListItem>()
            {
                new SelectListItem()
                { Text = "---Select---",Value = ""},
            };
            List<WeekDay> sevenDayWeeks = new WeekDayManager().GetAllDay();
            foreach (WeekDay sevenDayWeek in sevenDayWeeks)
            {
                SelectListItem aList = new SelectListItem();
                aList.Text = sevenDayWeek.Day;
                aList.Value = sevenDayWeek.Id.ToString();
                selectedList.Add(aList);
            }

            return selectedList;
        }
    }
}