using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using UniversityCourseAndResultSystem.Models;

namespace UniversityCourseAndResultSystem.DAL
{
    public class DesignationGateway
    {
        private string conString = DBConnection.SQLDBConnString();
        private SqlConnection connection;
        private SqlDataReader sqlDataReader;
        private SqlCommand cmd;


        public DesignationGateway()
        {
            connection = new SqlConnection(conString);
        }
        public List<Designation> GetAllDesignations()
        {
            string query = @"SELECT *from Designation order by DesignationName";
            cmd = new SqlCommand(query, connection);
            connection.Open();
            sqlDataReader = cmd.ExecuteReader();
            List<Designation> designationList = new List<Designation>();
            while (sqlDataReader.Read())
            {
                Designation aDesignation = new Designation();
                aDesignation.Id = Convert.ToInt32(sqlDataReader["id"]);
                aDesignation.DesignationId = Convert.ToInt32(sqlDataReader["DesignationID"]);
                aDesignation.DesignationName = sqlDataReader["DesignationName"].ToString();
                designationList.Add(aDesignation);

            }
            connection.Close();
            return designationList;
        }
    }
}