using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UniversityCourseAndResultSystem.DAL;
using UniversityCourseAndResultSystem.Models;

namespace UniversityCourseAndResultSystem.BLL
{
    public class CourseAssignManager
    {
        private CourseAssignGateway aCourseAssignGateway;

        public CourseAssignManager()
        {
            aCourseAssignGateway = new CourseAssignGateway();
        }

        public string InsertNewCourse(CourseAssign courseAssign)
        {
            int rowAffected = aCourseAssignGateway.AssignNewCourse(courseAssign);
         
            if (rowAffected > 0)
            {
                return "Teacher Course Assign Successfully Done!!!";
            }
            else
            {
                return "Course Assign Failed!!";
            }
        }

        public string UnAssignCourses()
        {

            int rowAffected = aCourseAssignGateway.UnAssignCourses();
            int rowAffected2 = new EnrollCourseGateway().DisEnrollCourses();
            if (rowAffected2 > 0 && rowAffected > 0)
            {
                return "All Courses Unassign Successfully!!!";
            }
            else
            {
                return "Failed";
            }

        }
    }
}