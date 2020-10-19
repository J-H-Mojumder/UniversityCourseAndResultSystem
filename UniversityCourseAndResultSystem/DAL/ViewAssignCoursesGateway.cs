using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using UniversityCourseAndResultSystem.Models;

namespace UniversityCourseAndResultSystem.DAL
{
    public class ViewAssignCoursesGateway
    {
        private string conString = DBConnection.SQLDBConnString();
        private SqlConnection connection;
        private SqlDataReader sqlDataReader;

        private SqlCommand cmd;

        public ViewAssignCoursesGateway()
        {
            connection = new SqlConnection(conString);
        }

        public List<ViewAssignCourse> GetAllAssignCourses(int deptId)
        {
            string query = @"SELECT c.CourseCode, c.CourseName,s.SemesterName,t.TeacherName FROM TeacherAsign ta  right JOIN Course c ON c.id=ta.CourseID and ta.Status !='UnAssign'  left JOIN Teachers t ON t.Id=ta.TeacherID Inner JOIN Semester s ON c.SemesterID=s.Id  where c.DepartmentID='" + deptId + "'  order by CourseCode";
            cmd = new SqlCommand(query, connection);
            connection.Open();
            sqlDataReader = cmd.ExecuteReader();
            List<ViewAssignCourse> courseList = new List<ViewAssignCourse>();
            while (sqlDataReader.Read())
            {
                ViewAssignCourse viewAssignCourse = new ViewAssignCourse();

                viewAssignCourse.CourseCode = sqlDataReader["CourseCode"].ToString();
                viewAssignCourse.CourseName = sqlDataReader["CourseName"].ToString();
                viewAssignCourse.CourseSemester = sqlDataReader["SemesterName"].ToString();
                viewAssignCourse.AssignTeacherName = sqlDataReader["TeacherName"].ToString();
                if (viewAssignCourse.AssignTeacherName == "")
                {
                    viewAssignCourse.AssignTeacherName = "Not Assigned Yet";
                }
                courseList.Add(viewAssignCourse);

            }
            connection.Close();
            return courseList;
        }
    }
}