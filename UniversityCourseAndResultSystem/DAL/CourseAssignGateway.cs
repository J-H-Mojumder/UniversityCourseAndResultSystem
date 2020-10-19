using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using UniversityCourseAndResultSystem.Models;

namespace UniversityCourseAndResultSystem.DAL
{
    public class CourseAssignGateway
    {
        private string conString = DBConnection.SQLDBConnString();
        private SqlConnection connection;
        private SqlDataReader sqlDataReader;
        private SqlCommand cmd;



        public CourseAssignGateway()
        {
            connection = new SqlConnection(conString);
        }
        public int AssignNewCourse(CourseAssign courseAssign)
        {
            string query = @"INSERT INTO TeacherAsign Values('Assign','" + courseAssign.TeacherId + "','" + courseAssign.CourseId + "')";
            cmd = new SqlCommand(query, connection);
            connection.Open();
            int rowAffect = cmd.ExecuteNonQuery();
            connection.Close();
            return rowAffect;
        }
        public int UnAssignCourses()
        {
            string query = @"UPDATE TeacherAsign SET Status = 'UnAssign' ";
            cmd = new SqlCommand(query, connection);
            connection.Open();
            int rowAffect = cmd.ExecuteNonQuery();
            connection.Close();
            return rowAffect;
        }
    }
}