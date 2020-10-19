using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UniversityCourseAndResultSystem.BLL;
using UniversityCourseAndResultSystem.Models;

namespace UniversityCourseAndResultSystem.Controllers
{
    public class DepartmentController : Controller
    {        
        [HttpGet]
        public ActionResult Index()
        {
             
            ViewBag.Message = "";
            return View();
        }

        [HttpPost]
        public ActionResult Index(Department department)
        {
            if (ModelState.IsValid)
            {
                DepartmentManager departmentManager = new DepartmentManager();
                department.DeptCode = department.DeptCode.ToUpper();
                department = BlankCheck(department);
                string result = departmentManager.SaveDepartment(department);
                ViewBag.Message  = result;
                ModelState.Clear();
                return View();
            }
            else
            {
                ViewBag.Message = "";              
                return View();
            }

        }
        [HttpGet]
        public ActionResult ViewAllDepartment()
        {
            return View();
        }

        public Department BlankCheck(Department department)
        {
            department.DeptCode = department.DeptCode.Trim();
            department.DeptName = department.DeptName.Trim();
            return department;
        }
        public JsonResult GetAllDepartment()
        {
            var departments = new DepartmentManager().GetAllDepartments();
            return Json(departments.ToList());
        }
    }
}