using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UniversityCourseAndResultSystem.Models
{
    public class ViewAssignCourse
    {
        public string CourseCode { get; set; }
        public string CourseName { get; set; }
        public string CourseSemester { get; set; }
        public string AssignTeacherName { get; set; }
    }
}