using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using UniversityCourseAndResultSystem.Models;

namespace UniversityCourseAndResultSystem.DAL
{
    public class CourseGateway
    {
        private string conString = DBConnection.SQLDBConnString();
        private SqlConnection connection;
        private SqlDataReader sqlDataReader;
        private SqlCommand cmd;



        public CourseGateway()
        {
            connection = new SqlConnection(conString);
        }
        public int SaveCourse(Course course)
        { 

            string query = @"insert into Course (CourseCode, CourseName,CourseCredit,CourseDescription,DepartmentId,SemesterId) values(@CourseCode, @CourseName,@CourseCredit,@CourseDescription,@DepartmentId,@SemesterId)";
              cmd = new SqlCommand(query, connection);
            //cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@CourseCode", course.CourseCode.Replace("'", "''"));
            cmd.Parameters.AddWithValue("@CourseName", course.CourseName.Replace("'", "''"));
            cmd.Parameters.AddWithValue("@CourseCredit", course.CourseCredit);
            if (String.IsNullOrEmpty(course.CourseDescription))
            {
                cmd.Parameters.AddWithValue("@CourseDescription", DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@CourseDescription", course.CourseDescription);
            }
            cmd.Parameters.AddWithValue("@DepartmentID", course.DepartmentId);
            cmd.Parameters.AddWithValue("@SemesterID", course.SemesterId);

            connection.Open();
            int rowAffect = cmd.ExecuteNonQuery();
            connection.Close();
            return rowAffect;
        }
       
        public List<Course> GetAllCourses()
        {
            string query = @"SELECT * from Course order by CourseCode";
            cmd = new SqlCommand(query, connection);
            connection.Open();
            sqlDataReader = cmd.ExecuteReader();
            List<Course> courseList = new List<Course>();
            while (sqlDataReader.Read())
            {
                Course aCourse = new Course();
                aCourse.Id = (int)sqlDataReader["id"];
                aCourse.CourseCode = sqlDataReader["CourseCode"].ToString();
                aCourse.CourseName = sqlDataReader["CourseName"].ToString();
                aCourse.CourseCredit = (decimal)sqlDataReader["CourseCredit"];
                aCourse.CourseDescription = sqlDataReader["CourseDescription"].ToString();
                aCourse.DepartmentId = (int)sqlDataReader["DepartmentID"];
                aCourse.SemesterId = (int)sqlDataReader["SemesterID"];
                courseList.Add(aCourse);

            }
            connection.Close();
            return courseList;
        }
        public List<Course> GetAllCoursesByDeptId(int deptId)
        {
            string query = @"SELECT *from Course where DepartmentID=" + deptId + " order by CourseCode";
            cmd = new SqlCommand(query, connection);
            connection.Open();
            sqlDataReader = cmd.ExecuteReader();
            List<Course> courseList = new List<Course>();
            while (sqlDataReader.Read())
            {
                Course aCourse = new Course();
                aCourse.Id = (int)sqlDataReader["id"];
                aCourse.CourseCode = sqlDataReader["CourseCode"].ToString();
                aCourse.CourseName = sqlDataReader["CourseName"].ToString();
                aCourse.CourseCredit = (decimal)sqlDataReader["CourseCredit"];
                aCourse.CourseDescription = sqlDataReader["CourseDescription"].ToString();
                aCourse.DepartmentId = (int)sqlDataReader["DepartmentID"];
                aCourse.SemesterId = (int)sqlDataReader["SemesterID"];
                courseList.Add(aCourse);

            }
            connection.Close();
            return courseList;
        }
        public List<Course> GetAllUnAssignCoursesByDeptId(int deptId)
        {
            string query = @"SELECT * from Course where Id not in(select CourseId from TeacherAsign where Status='Assign') and DepartmentID='" + deptId + "' order by CourseCode";
            cmd = new SqlCommand(query, connection);
            connection.Open();
            sqlDataReader = cmd.ExecuteReader();
            List<Course> courseList = new List<Course>();
            while (sqlDataReader.Read())
            {
                Course aCourse = new Course();
                aCourse.Id = (int)sqlDataReader["id"];
                aCourse.CourseCode = sqlDataReader["CourseCode"].ToString();
                aCourse.CourseName = sqlDataReader["CourseName"].ToString();
                aCourse.CourseCredit = (decimal)sqlDataReader["CourseCredit"];
                aCourse.CourseDescription = sqlDataReader["CourseDescription"].ToString();
                aCourse.DepartmentId = (int)sqlDataReader["DepartmentID"];
                aCourse.SemesterId = (int)sqlDataReader["SemesterID"];
                courseList.Add(aCourse);

            }
            connection.Close();
            return courseList;
        }
        public Course GetCourseInformationByCourseId(int courseId)
        {
            string query = @"SELECT *from Course where id=" + courseId + "";
            cmd = new SqlCommand(query, connection);
            connection.Open();
            sqlDataReader = cmd.ExecuteReader();
            Course aCourse = new Course();
            while (sqlDataReader.Read())
            {

                aCourse.Id = (int)sqlDataReader["id"];
                aCourse.CourseCode = sqlDataReader["CourseCode"].ToString();
                aCourse.CourseName = sqlDataReader["CourseName"].ToString();
                aCourse.CourseCredit = (decimal)sqlDataReader["CourseCredit"];
                aCourse.CourseDescription = sqlDataReader["CourseDescription"].ToString();
                aCourse.DepartmentId = (int)sqlDataReader["DepartmentID"];
                aCourse.SemesterId = (int)sqlDataReader["SemesterID"];
                break;

            }
            connection.Close();
            return aCourse;
        }
        public double GetCourseCreditByCourseId(int courseId)
        {
            string query = @"SELECT CourseCredit from Course where id=" + courseId + "";
            cmd = new SqlCommand(query, connection);
            connection.Open();
            sqlDataReader = cmd.ExecuteReader();
            double credit = 0;
            decimal cr = 0;
            while (sqlDataReader.Read())
            {
                cr = (decimal)sqlDataReader["CourseCredit"];
                credit = Convert.ToDouble(cr);
                break;

            }
            connection.Close();
            return credit;
        }
        public List<Course> GetCourseByStudentId(string studentId)
        {
            string query = @"SELECT Course.id,Course.CourseCode from Course 
                            Inner JOIN Department ON Course.DepartmentID=Department.Id 
                            Right JOIN Students ON Course.DepartmentID=Students.DepartmentID where Students.id='" + studentId + "' and Course.id is not null";
            cmd = new SqlCommand(query, connection);
            connection.Open();
            sqlDataReader = cmd.ExecuteReader();
            List<Course> courseList = new List<Course>();
            while (sqlDataReader.Read())
            {
                Course aCourse = new Course();
                aCourse.Id = Convert.ToInt32(sqlDataReader["id"]);
                aCourse.CourseCode = sqlDataReader["CourseCode"].ToString();
                courseList.Add(aCourse);
            }
            connection.Close();
            return courseList;
        }
        public bool IsCodeExist(Course course)
        {
            string query = @"SELECT * FROM Course WHERE CourseCode = @CourseCode";
            cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@CourseCode", course.CourseCode.Replace("'", "''"));
            cmd.Connection.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            bool isExist = reader.HasRows;
            reader.Close();
            connection.Close();
            return isExist;
        }
        public bool IsNameExist(Course course)
        {
            string query = @"SELECT * FROM Course WHERE CourseName = @CourseName";

            cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@CourseName", course.CourseName.Replace("'", "''"));
            cmd.Connection.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            bool isExist = reader.HasRows;
            reader.Close();
            connection.Close();
            return isExist;
        }
    }
}