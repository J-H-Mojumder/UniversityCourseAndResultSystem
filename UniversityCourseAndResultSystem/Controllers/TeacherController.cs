using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UniversityCourseAndResultSystem.BLL;
using UniversityCourseAndResultSystem.DAL;
using UniversityCourseAndResultSystem.Models;

namespace UniversityCourseAndResultSystem.Controllers
{
    public class TeacherController : Controller
    {
        // GET: Teacher
        [HttpGet]
        public ActionResult Index()
        {
            ViewBag.Message = "";
            ViewData["Message"] = "";
            ViewBag.Departments = GetAllDepartmentList();
            ViewBag.Designation = GetAllDesignationList();
            return View();
        }
        [HttpPost]
        public ActionResult Index(Teacher teacher)
        {
            if (ModelState.IsValid)
            {
                TeacherManager teacherManager = new TeacherManager();
                teacher = BlankCheck(teacher);
                string result = teacherManager.SaveTeacher(teacher);
                ViewBag.Departments = GetAllDepartmentList();
                ViewBag.Designation = GetAllDesignationList();
                ViewBag.Message = result;
                ModelState.Clear();
                return View();
            }
            else
            {
                ViewData["Message"] = "";
                ViewBag.Departments = GetAllDepartmentList();
                ViewBag.Designation = GetAllDesignationList();
                return View();
            }
        }
        public Teacher BlankCheck(Teacher teacher)
        {
            teacher.TeacherName = teacher.TeacherName.Trim();
            teacher.TeacherAddress = teacher.TeacherAddress.Trim();
            teacher.TeacherEmail = teacher.TeacherEmail.Trim();
            teacher.TeacherContactNo = teacher.TeacherContactNo.Trim();
            return teacher;
        }
        public IEnumerable<SelectListItem> GetAllDepartmentList()
        {
            List<SelectListItem> departmentListItems = new List<SelectListItem>()
            {
                new SelectListItem(){Text = "---Select---",Value = ""},
            };
            List<Department> departmentList = new DepartmentManager().GetAllDepartments();
            foreach (Department department in departmentList)
            {
                SelectListItem departmentListItem = new SelectListItem();
                departmentListItem.Text = department.DeptName;
                departmentListItem.Value = department.Id.ToString();
                departmentListItems.Add(departmentListItem);
            }

            return departmentListItems;
        }
        public IEnumerable<SelectListItem> GetAllDesignationList()
        {
            List<SelectListItem> designationListItems = new List<SelectListItem>()
            {
                new SelectListItem(){Text = "---Select---",Value = ""},
            };
            List<Designation> designationList = new DesignationManager().GetAllDesignations();
            foreach (Designation designations in designationList)
            {
                SelectListItem designationListItem = new SelectListItem();
                designationListItem.Text = designations.DesignationName;
                designationListItem.Value = designations.Id.ToString();
                designationListItems.Add(designationListItem);
            }

            return designationListItems;
        }
        public IEnumerable<SelectListItem> GetAllCourseList()
        {
            List<SelectListItem> courseListItems = new List<SelectListItem>()
            {
                new SelectListItem(){Text = "---Select---",Value = ""},
            };
            List<Course> courseList = new CourseManager().GetAllCourses();
            foreach (Course course in courseList)
            {
                SelectListItem courseListItem = new SelectListItem();
                courseListItem.Text = course.CourseName;
                courseListItem.Value = course.Id.ToString();
                courseListItems.Add(courseListItem);
            }

            return courseListItems;
        }
        
        public JsonResult GetTeacherByDepartmentId(int departmentId)
        {
            var teacher = new TeacherManager().GetAllTeachersByDepartment(departmentId);
            var teacherList = teacher.ToList();
            return Json(teacherList);
        }
        public JsonResult GetTeacherInformationById(int teacherId)
        {
            var teacher = new TeacherManager().GetTeacherInformationById(teacherId);
            teacher.CourseTaken = new TeacherGateway().GetTotalTakenCourses(teacherId);
            return Json(teacher);
        }

        [HttpGet]
        public ActionResult CourseAssign()
        {
            ViewData["Message"] = "";
            ViewBag.Departments = GetAllDepartmentList();
            ViewBag.Courses = GetAllCourseList();
            return View();
        }
        [HttpPost]
        public ActionResult CourseAssign(CourseAssign courseAssign)
        {
            if (ModelState.IsValid)
            {
                ViewBag.Departments = GetAllDepartmentList();
                ViewBag.Designation = GetAllDesignationList();
                string result = new CourseAssignManager().InsertNewCourse(courseAssign);
                ViewData["Message"] = result;
                ModelState.Clear();
                return View();
            }
            else
            {
                ViewData["Message"] = "";
                ViewBag.Departments = GetAllDepartmentList();
                ViewBag.Courses = GetAllCourseList();
                return View();
            }

        }
    }
}