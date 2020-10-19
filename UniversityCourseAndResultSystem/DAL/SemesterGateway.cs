using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using UniversityCourseAndResultSystem.Models;

namespace UniversityCourseAndResultSystem.DAL
{
    public class SemesterGateway
    {
        private string conString = DBConnection.SQLDBConnString();
        private SqlConnection connection;
        private SqlDataReader sqlDataReader;
        private SqlCommand cmd;

        public SemesterGateway()
        {
            connection = new SqlConnection(conString);
        }
        public List<Semester> GetAllSemesters()
        {
            string query = @"SELECT *from Semester order by SemesterCode";
            cmd = new SqlCommand(query, connection);
            connection.Open();
            sqlDataReader = cmd.ExecuteReader();
            List<Semester> semesterList = new List<Semester>();
            while (sqlDataReader.Read())
            {
                Semester aSemester = new Semester();
                aSemester.Id = Convert.ToInt32(sqlDataReader["id"]);
                aSemester.SemesterCode = Convert.ToInt32(sqlDataReader["SemesterCode"]);
                aSemester.SemesterName = sqlDataReader["SemesterName"].ToString();
                semesterList.Add(aSemester);

            }
            connection.Close();
            return semesterList;
        }
    }
}