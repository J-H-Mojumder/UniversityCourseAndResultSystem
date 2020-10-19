using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using UniversityCourseAndResultSystem.Models;

namespace UniversityCourseAndResultSystem.DAL
{
    public class DepartmentGateway
    {
        private string conString = DBConnection.SQLDBConnString();
        private SqlConnection connection;
        private SqlDataReader sqlDataReader;
        private SqlCommand cmd;


        public DepartmentGateway()
        {
            connection = new SqlConnection(conString);
        }
        public int SaveDepartment(Department department)
        { 
            string query = @"insert into Department (DeptCode, DeptName) values(@DeptCode, @DeptName)";
            cmd = new SqlCommand(query, connection);
            //cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@DeptCode", department.DeptCode.Replace("'", "''"));
            cmd.Parameters.AddWithValue("@DeptName", department.DeptName.Replace("'", "''")); 
            connection.Open();
            int rowAffect = cmd.ExecuteNonQuery();
            connection.Close();
            return rowAffect;
        }
        
        public List<Department> GetAllDepartments()
        {
            string query = @"SELECT *from Department order by DeptCode";
            cmd = new SqlCommand(query, connection);
            connection.Open();
            sqlDataReader = cmd.ExecuteReader();
            List<Department> departmentList = new List<Department>();
            while (sqlDataReader.Read())
            {
                Department aDept = new Department();
                aDept.Id = Convert.ToInt32(sqlDataReader["id"]);
                aDept.DeptName = sqlDataReader["DeptName"].ToString();
                aDept.DeptCode = sqlDataReader["DeptCode"].ToString();
                departmentList.Add(aDept);

            }
            connection.Close();
            return departmentList;
        }
        public string GetDepartmentName(int deptId)
        {
            string query = @"SELECT DeptCode from Department where id='" + deptId + "'";
            cmd = new SqlCommand(query, connection);

            connection.Open();
            sqlDataReader = cmd.ExecuteReader();
            string deptCode = "";
            while (sqlDataReader.Read())
            {
                deptCode = sqlDataReader["DeptCode"].ToString();
                break;

            }
            connection.Close();
            return deptCode;
        }
        public bool IsCodeExist(Department department)
        {
            string query = @"SELECT * FROM Department WHERE DeptCode = @DeptCode";
            cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@DeptCode", department.DeptCode.Replace("'", "''"));
            cmd.Connection.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            bool isExist = reader.HasRows;
            reader.Close();
            connection.Close();
            return isExist;
        }
        public bool IsNameExist(Department department)
        {
            string query = @"SELECT * FROM Department WHERE DeptName = @DeptName";

            cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@DeptName", department.DeptName.Replace("'", "''"));
            cmd.Connection.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            bool isExist = reader.HasRows;
            reader.Close();
            connection.Close();
            return isExist;
        }
    }
}