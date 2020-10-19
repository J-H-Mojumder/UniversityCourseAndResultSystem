using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UniversityCourseAndResultSystem.DAL;
using UniversityCourseAndResultSystem.Models;

namespace UniversityCourseAndResultSystem.BLL
{
    public class CourseManager
    {
        private CourseGateway aCoursesGateway;

        public CourseManager()
        {
            aCoursesGateway = new CourseGateway();
        }
         
        public List<Course> GetAllCourses()
        {
            return aCoursesGateway.GetAllCourses();
        } 
        public Course GetCourseInformationByCourseId(int courseId)
        {
            return aCoursesGateway.GetCourseInformationByCourseId(courseId);
        }

        public double GetCourseCreditByCourseId(int courseId)
        {
            return aCoursesGateway.GetCourseCreditByCourseId(courseId);
        }

        public List<Course> GetCourseByStudentId(String studentId)
        {
            return aCoursesGateway.GetCourseByStudentId(studentId);
        }

        public List<Course> GetAllUnAssignCoursesByDeptId(int deptId)
        {
            return aCoursesGateway.GetAllUnAssignCoursesByDeptId(deptId);
        }
        public List<Course> GetAllCoursesByDeptId(int deptId)
        {
            return aCoursesGateway.GetAllCoursesByDeptId(deptId);
        }
        public string SaveCourse(Course course)
        {
            if (aCoursesGateway.IsCodeExist(course))
            {
                return "This Code Aalready Exist!!";
            }
            else if (aCoursesGateway.IsNameExist(course))
            {
                return "This Name Already Exist!!";
            }
            else
            {
                int rowAffected = aCoursesGateway.SaveCourse(course);

                if (rowAffected > 0)
                {
                    return "Course Saved Successfully!!";
                }
                else
                {
                    return "Saved Failed!!";
                }
            }
        }
    }
}