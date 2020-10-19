using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UniversityCourseAndResultSystem.DAL;
using UniversityCourseAndResultSystem.Models;

namespace UniversityCourseAndResultSystem.BLL
{
    public class EnrollCourseManager
    {
        private EnrollCourseGateway aEnrollCourseGateway;

        public EnrollCourseManager()
        {
            aEnrollCourseGateway = new EnrollCourseGateway();
        }

        public string AssignNewCourseToStudent(EnrollInCourse enrollInCourse)
        {
            if (aEnrollCourseGateway.IsCourseIdAndStudentIdExist(enrollInCourse))
            {
                return "This Course already Taken by this Student";
            }
            else
            {
                int rowAffected = aEnrollCourseGateway.EnrollNewCourseToStudent(enrollInCourse);
                return rowAffected > 0 ? "Course Successfully Enrolled" : "Failed";
            }
        }
    }
}