using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using UniversityCourseAndResultSystem.Models;

namespace UniversityCourseAndResultSystem.DAL
{
    public class EnrollCourseGateway
    {
        private string conString = DBConnection.SQLDBConnString();
        private SqlConnection connection;
        private SqlDataReader sqlDataReader;
        private SqlCommand cmd;

        public EnrollCourseGateway()
        {
            connection = new SqlConnection(conString);
        }

        public int EnrollNewCourseToStudent(EnrollInCourse enrollInCourse)
        {
            string query = @"insert into StudentAsign (Status, Date,StudentID,CourseID,GradeID) values(@Status,@Date, @StudentID,@CourseID,@GradeID )";
            cmd = new SqlCommand(query, connection);
            //cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Status", "0");
            cmd.Parameters.AddWithValue("@Date", enrollInCourse.Date); 
            cmd.Parameters.AddWithValue("@StudentID", enrollInCourse.StudentId); 
            cmd.Parameters.AddWithValue("@CourseID", enrollInCourse.CourseId);
            cmd.Parameters.AddWithValue("@GradeID", "14"); 
            connection.Open();
            int rowAffect = cmd.ExecuteNonQuery();
            connection.Close();
            return rowAffect; 
        }
        public int DisEnrollCourses()
        {
            string query = @"UPDATE StudentAsign SET Status = '1' ";
            cmd = new SqlCommand(query, connection);
            connection.Open();
            int rowAffect = cmd.ExecuteNonQuery();
            connection.Close();
            return rowAffect;
        }
        public bool IsCourseIdAndStudentIdExist(EnrollInCourse enrollInCourse)
        {
            string query = @"SELECT * FROM StudentAsign WHERE CourseID = '" + enrollInCourse.CourseId + "' And StudentID='" + enrollInCourse.StudentId + "' AND Status='0'";

            cmd = new SqlCommand(query, connection);
            cmd.Connection.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            bool isExist = reader.HasRows;
            reader.Close();
            connection.Close();
            return isExist;
        }
       
    }
}