using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using UniversityCourseAndResultSystem.Models;

namespace UniversityCourseAndResultSystem.DAL
{
    public class ResultGateway
    {
        private string conString = DBConnection.SQLDBConnString();
        private SqlConnection connection;
        private SqlDataReader sqlDataReader;

        private SqlCommand cmd;

        public ResultGateway()
        {
            connection = new SqlConnection(conString);
        }
        public int SaveAResult(Result result)
        {
            string query = @"UPDATE StudentAsign SET GradeId = '" + result.GradeId + "' where StudentID ='" + result.StudentId + "' AND CourseID='" + result.CourseId + "' AND Status ='0'";
            cmd = new SqlCommand(query, connection);
            connection.Open();
            int rowAffect = cmd.ExecuteNonQuery();
            connection.Close();
            return rowAffect;
        }
        

        public List<Result> GetAllResultForView(int studentId)
        {
            string query = @"SELECT c.CourseCode,c.CourseName,g.Grade from StudentAsign s Inner JOIN Course c ON c.id=s.CourseID  Inner JOIN Grade g On s.GradeID=g.id where s.StudentID='" + studentId + "' and s.Status=0";
            cmd = new SqlCommand(query, connection);
            connection.Open();
            sqlDataReader = cmd.ExecuteReader();
            List<Result> resultList = new List<Result>();
            while (sqlDataReader.Read())
            {
                Result aResult = new Result();
                aResult.CourseCode = sqlDataReader["CourseCode"].ToString();
                aResult.CourseName = sqlDataReader["CourseName"].ToString();
                aResult.Grade = sqlDataReader["Grade"].ToString();
                resultList.Add(aResult);

            }
            connection.Close();
            return resultList;
        }

        public DataTable GetResultView(string StudentId)
        { 
            SqlConnection sqlCon = new SqlConnection(conString);
            string query = @"SELECT c.CourseCode,c.CourseName,g.Grade from StudentAsign s Inner JOIN Course c ON c.id=s.CourseID Inner JOIN Grade g On s.GradeID=g.id where s.StudentID='" + StudentId + "' and s.Status=0";
            DataTable dt = DBConnection.ExecuteQuery(conString, query, "StudentAsign");
            return dt;
        }

        public List<Grade> GetAllGrades()
        {
            string query = @"SELECT * from Grade where id!=14 order  by id";
            cmd = new SqlCommand(query, connection);
            connection.Open();
            sqlDataReader = cmd.ExecuteReader();
            List<Grade> gradeList = new List<Grade>();
            while (sqlDataReader.Read())
            {
                Grade aGrade = new Grade();
                aGrade.Id = Convert.ToInt32(sqlDataReader["id"]);
                aGrade.GradeLetter = sqlDataReader["Grade"].ToString();
                gradeList.Add(aGrade);

            }
            connection.Close();
            return gradeList;
        }
        public List<Course> GetAllTakenCourses(int studentId)
        {
            string query = @"SELECT c.CourseCode,c.id from StudentAsign s Inner JOIN Course c ON c.id=s.CourseID where s.StudentID='" + studentId + "' and s.Status=0";
            cmd = new SqlCommand(query, connection);
            connection.Open();
            sqlDataReader = cmd.ExecuteReader();
            List<Course> resultList = new List<Course>();
            while (sqlDataReader.Read())
            {
                Course aCourse = new Course();
                aCourse.Id = Convert.ToInt32(sqlDataReader["id"]);
                aCourse.CourseCode = sqlDataReader["CourseCode"].ToString();
                resultList.Add(aCourse);

            }
            connection.Close();
            return resultList;
        }

        public DataTable GetResultViewStudentInfo(string StudentId)
        {

            SqlConnection sqlCon = new SqlConnection(conString);
            string query = @"SELECT c.CourseCode,c.CourseName,g.Grade,s.StudentRegNo,s.StudentName,s.StudentContactNo,s.StudentEmail from StudentAsign sa Inner JOIN Course c ON c.id=sa.CourseID   Inner join Students s on  s.id=sa.StudentID Inner JOIN Grade g On sa.GradeID=g.id   where sa.StudentID='" + StudentId + "' and sa.Status=0";
            DataTable dt = DBConnection.ExecuteQuery(conString, query, "StudentAsign");
            return dt;
        }

        public DataTable GetResultView()
        {

            SqlConnection sqlCon = new SqlConnection(conString);
            string query = @"SELECT c.CourseCode,c.CourseName,g.Grade from StudentAsign s Inner JOIN Course c ON c.id=s.CourseID  Inner JOIN Grade g On s.GradeID=g.id where   s.Status=0";
            DataTable dt = DBConnection.ExecuteQuery(conString, query, "StudentAsign");
            return dt;
        }
    }
}