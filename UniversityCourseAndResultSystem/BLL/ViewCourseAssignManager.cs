using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UniversityCourseAndResultSystem.DAL;
using UniversityCourseAndResultSystem.Models;

namespace UniversityCourseAndResultSystem.BLL
{
    public class ViewCourseAssignManager
    {
        private ViewAssignCoursesGateway aViewAssignCoursesGateway;
        public ViewCourseAssignManager()
        {
            aViewAssignCoursesGateway = new ViewAssignCoursesGateway();
        }
        public List<ViewAssignCourse> GetAllAssignCourses(int deptId)
        {
            return new ViewAssignCoursesGateway().GetAllAssignCourses(deptId);
        }
    }
}