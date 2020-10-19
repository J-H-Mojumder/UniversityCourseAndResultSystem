using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using UniversityCourseAndResultSystem.Models;

namespace UniversityCourseAndResultSystem.DAL
{
    public class StudentGateway
    {
        private string conString = DBConnection.SQLDBConnString();
        private SqlConnection connection;
        private SqlDataReader sqlDataReader;
        private SqlCommand cmd;

        public StudentGateway()
        {
            connection = new SqlConnection(conString);
        }
        public int InsertNewStudent(Student student)
        {
            string query = @"insert into Students (StudentName, StudentEmail,StudentContactNo,StudentAddDate,StudentAddress,StudentRegNo,DepartmentId) values(@StudentName, @StudentEmail,@StudentContactNo,@StudentAddDate,@StudentAddress,@StudentRegNo,@DepartmentId)";
            cmd = new SqlCommand(query, connection);
            //cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@StudentName", student.StudentName);
            cmd.Parameters.AddWithValue("@StudentEmail", student.StudentEmail);
            cmd.Parameters.AddWithValue("@StudentContactNo", student.StudentContactNo);
            cmd.Parameters.AddWithValue("@StudentAddDate", student.StudentAddDate);
            if (String.IsNullOrEmpty(student.StudentAddress))
            {
                cmd.Parameters.AddWithValue("@StudentAddress", DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@StudentAddress", student.StudentAddress);
            } 
            cmd.Parameters.AddWithValue("@StudentRegNo", student.StudentRegNo);
            cmd.Parameters.AddWithValue("@DepartmentId", student.DepartmentId);
            connection.Open();
            int rowAffect = cmd.ExecuteNonQuery();
            connection.Close();
            return rowAffect;
 
        }

       
        public List<Student> GetAllStudents()
        {
            string query = @"select s.id,s.StudentRegNo, s.StudentName , s.StudentEmail,d.DeptCode from Students s  Join Department d on s.DepartmentID=d.id  order by StudentRegNo";
            cmd = new SqlCommand(query, connection);
            connection.Open();
            sqlDataReader = cmd.ExecuteReader();
            List<Student> studentList = new List<Student>();
            while (sqlDataReader.Read())
            {
                Student aStudent = new Student();
                aStudent.Id = Convert.ToInt32(sqlDataReader["id"]);
                aStudent.StudentRegNo = sqlDataReader["StudentRegno"].ToString();
                aStudent.StudentEmail = sqlDataReader["StudentEmail"].ToString();
                aStudent.DepartmentName = sqlDataReader["DeptCode"].ToString();
                aStudent.StudentName = sqlDataReader["StudentName"].ToString();
                studentList.Add(aStudent);
            }
            connection.Close();
            return studentList;
        }
        public Student GetStudentById(int studentId)
        {
            string query = @"select s.id,s.StudentRegNo, s.StudentName , s.StudentEmail,d.DeptCode from Students s Join Department d on s.DepartmentID=d.id where s.id='" + studentId + "' order by StudentRegNo";
            cmd = new SqlCommand(query, connection);
            connection.Open();
            sqlDataReader = cmd.ExecuteReader();
            Student aStudent = new Student();
            while (sqlDataReader.Read())
            {

                aStudent.Id = Convert.ToInt32(sqlDataReader["id"]);
                aStudent.StudentRegNo = sqlDataReader["StudentRegno"].ToString();
                aStudent.StudentEmail = sqlDataReader["StudentEmail"].ToString();
                aStudent.DepartmentName = sqlDataReader["DeptCode"].ToString();
                aStudent.StudentName = sqlDataReader["StudentName"].ToString();
                break;
            }

            connection.Close();
            return aStudent;
        }
        public string GetLastStudentRegNo(int deptId, string year)
        {
            string query = @"SELECT TOP 1 * FROM Students where DepartmentID='" + deptId + "' and StudentAddDate like '" + year + "%'  ORDER BY StudentRegNo DESC";
            cmd = new SqlCommand(query, connection);
            connection.Open();
            sqlDataReader = cmd.ExecuteReader();
            string lastRegNo = "";
            while (sqlDataReader.Read())
            {
                lastRegNo = sqlDataReader["StudentRegNo"].ToString();
            }
            connection.Close();
            return lastRegNo;
        }
        public bool IsEmailExist(Student student)
        {
            string query = @"SELECT * FROM Students WHERE StudentEmail = '" + student.StudentEmail + "'";
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