using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UniversityCourseAndResultSystem.BLL;
using UniversityCourseAndResultSystem.Models;

namespace UniversityCourseAndResultSystem.Controllers
{
    public class CourseController : Controller
    { 
        [HttpGet]
        public ActionResult Index()
        {
            ViewBag.Message = "";
            ViewBag.Departments = GetAllDepartmentList();
            ViewBag.Semesters = GetAllSemesterList();
            return View();
        }
        [HttpPost]
        public ActionResult Index(Course course)
        {
            if (ModelState.IsValid)
            {
                CourseManager courseManager = new CourseManager();
                course.CourseCode = course.CourseCode.ToUpper();
                course = BlankCheck(course);
                string courseItem = courseManager.SaveCourse(course);
                ViewBag.Departments = GetAllDepartmentList();
                ViewBag.Semesters = GetAllSemesterList();
                ViewBag.Message  = courseItem;
                ModelState.Clear();
                return View();
            }
            else
            {
                ViewBag.Message = "";
                ViewBag.Departments = GetAllDepartmentList();
                ViewBag.Semesters = GetAllSemesterList();
                return View();
            }
        }
       

        [HttpGet]
        public ActionResult UnassignCourse()
        {
            ViewBag.Message = "";
            return View();
        }
        [HttpPost]
        public ActionResult UnassignCourse(string unAssignCourse)
        {
            ViewBag.Message = new CourseAssignManager().UnAssignCourses();
            return View();
        }


        public IEnumerable<SelectListItem> GetAllDepartmentList()
        {
            List<SelectListItem> departmentLists = new List<SelectListItem>()
            {
                new SelectListItem(){Text = "---Select---",Value = ""},
            };
            List<Department> deptList = new DepartmentManager().GetAllDepartments();
            foreach (Department department in deptList)
            {
                SelectListItem dept = new SelectListItem();
                dept.Text = department.DeptName;
                dept.Value = department.Id.ToString();
                departmentLists.Add(dept);
            }

            return departmentLists;
        }
        public IEnumerable<SelectListItem> GetAllSemesterList()
        {
            List<SelectListItem> semesterLists = new List<SelectListItem>()
            {
                new SelectListItem(){Text = "---Select---",Value = ""},
            };
            List<Semester> semesters = new SemesterManager().GetAllSemesters();
            foreach (Semester semester in semesters)
            {
                SelectListItem semesterListItem = new SelectListItem();
                semesterListItem.Text = semester.SemesterName;
                semesterListItem.Value = semester.Id.ToString();
                semesterLists.Add(semesterListItem);
            }

            return semesterLists;
        }
        public JsonResult GetAllUnAssignCoursesByDeptId(int departmentId)
        {
            var courses = new CourseManager().GetAllUnAssignCoursesByDeptId(departmentId);

            return Json(courses);
        }
        public JsonResult GetAllCoursesByDeptId(int departmentId)
        {
            var courses = new CourseManager().GetAllCoursesByDeptId(departmentId);

            return Json(courses);
        }
        public JsonResult GetAllAssignCourses(int departmentId)
        {
            var courses = new ViewCourseAssignManager().GetAllAssignCourses(departmentId);
            return Json(courses);
        }
        public JsonResult GetCourseInformationByCourseId(int courseId)
        {
            var course = new CourseManager().GetCourseInformationByCourseId(courseId);
            return Json(course);
        }

        [HttpGet]
        public ActionResult CourseAssign()
        {
            ViewBag.Departments = GetAllDepartmentList();
            return View();
        }

        public Course BlankCheck(Course course)
        {
            course.CourseCode = course.CourseCode.Trim();
            course.CourseName = course.CourseName.Trim();
            return course;
        }
    }
}