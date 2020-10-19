using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using UniversityCourseAndResultSystem.Models;

namespace UniversityCourseAndResultSystem.DAL
{
    public class TeacherGateway
    {
        private string conString = DBConnection.SQLDBConnString();
        private SqlConnection connection;
        private SqlDataReader sqlDataReader;
        private SqlCommand cmd;

        public TeacherGateway()
        {
            connection = new SqlConnection(conString);
        }
        public int SaveTeacher(Teacher teacher)
        {

            string query = @"insert into Teachers (TeacherName, TeacherAddress,TeacherEmail,TeacherContactNo,TeacherCredit,TeacherDesignationId,TeacherDepartmentId) values(@TeacherName, @TeacherAddress,@TeacherEmail,@TeacherContactNo,@TeacherCredit,@TeacherDesignationId,@TeacherDepartmentId)";
            cmd = new SqlCommand(query, connection);
            //cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@TeacherName", teacher.TeacherName);
            if (String.IsNullOrEmpty(teacher.TeacherAddress))
            {
                cmd.Parameters.AddWithValue("@TeacherAddress", DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@TeacherAddress", teacher.TeacherAddress);
            }
          
            cmd.Parameters.AddWithValue("@TeacherEmail", teacher.TeacherEmail);
            cmd.Parameters.AddWithValue("@TeacherContactNo", teacher.TeacherContactNo);
            cmd.Parameters.AddWithValue("@TeacherCredit", teacher.TeacherCredit);
            cmd.Parameters.AddWithValue("@TeacherDesignationId", teacher.TeacherDesignationId);
            cmd.Parameters.AddWithValue("@TeacherDepartmentId", teacher.TeacherDepartmentId);
            connection.Open();
            int rowAffect = cmd.ExecuteNonQuery();
            connection.Close();
            return rowAffect; 
        }
       
        public Teacher GetTeacherInformationById(int teacherId)
        {
            string query = @"SELECT *from Teachers where id=" + teacherId + "";
            cmd = new SqlCommand(query, connection);
            connection.Open();
            sqlDataReader = cmd.ExecuteReader();
            List<Teacher> teacherList = new List<Teacher>();
            Teacher aTeacher = new Teacher();
            while (sqlDataReader.Read())
            {
                aTeacher.Id = Convert.ToInt32(sqlDataReader["id"]);
                aTeacher.TeacherName = sqlDataReader["TeacherName"].ToString();
                aTeacher.TeacherCredit = Convert.ToDouble(sqlDataReader["TeacherCredit"]);
                break;
            }
            connection.Close();
            return aTeacher;
        }

        public double GetTotalTakenCourses(int teacherId)
        {
            string query = @"SELECT CourseCredit from TeacherAsign as AT inner join Course as C on AT.CourseID=C.id where TeacherID='" + teacherId + "' and Status='Assign'";
            cmd = new SqlCommand(query, connection);
            double totalCredit = 0.0;
            connection.Open();
            sqlDataReader = cmd.ExecuteReader();
            while (sqlDataReader.Read())
            {
                totalCredit += Convert.ToDouble(sqlDataReader["CourseCredit"]);
            }
            connection.Close();
            return totalCredit;
        }
        public bool IsEmailExist(Teacher teacher)
        {
            string query = @"SELECT * FROM Teachers WHERE TeacherEmail = '" + teacher.TeacherEmail + "'";
            cmd = new SqlCommand(query, connection);
            cmd.Connection.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            bool isExist = reader.HasRows;
            reader.Close();
            connection.Close();
            return isExist;
        }
        public List<Teacher> GetAllTeacherByDepartment(int departmentId)
        {
            string query = @"SELECT *from Teachers where TeacherDepartmentID='" + departmentId + "' order by TeacherName";
            cmd = new SqlCommand(query, connection);
            connection.Open();
            sqlDataReader = cmd.ExecuteReader();
            List<Teacher> teacherList = new List<Teacher>();
            while (sqlDataReader.Read())
            {
                Teacher aTeacher = new Teacher();
                aTeacher.Id = Convert.ToInt32(sqlDataReader["id"]);
                aTeacher.TeacherName = sqlDataReader["TeacherName"].ToString();
                aTeacher.TeacherCredit = Convert.ToDouble(sqlDataReader["TeacherCredit"]);
                teacherList.Add(aTeacher);
            }
            connection.Close();
            return teacherList;
        }

        public double GetTeacherCreditLimit(int teacherId)
        {
            string query = @"SELECT TeacherCredit from Teachers where id=" + teacherId + "";
            cmd = new SqlCommand(query, connection);
            connection.Open();
            sqlDataReader = cmd.ExecuteReader();
            double creditLimit = 0.0;
            while (sqlDataReader.Read())
            {
                creditLimit = Convert.ToDouble(sqlDataReader["TeacherCredit"]);
                break;
            }
            connection.Close();
            return creditLimit;
        }
    }
}