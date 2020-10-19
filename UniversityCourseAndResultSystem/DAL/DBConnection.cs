using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace UniversityCourseAndResultSystem.DAL
{
    public class DBConnection
    {
        public static string SQLDBConnString()
        {
            return string.Format("Server=.;Database=UniversityManagementDB;Integrated Security=True");
            //return String.Format("Server=.;Database=UniversityManagementDB;User ID=University;Password=Admin@DB;Trusted_Connection=False; Connection Timeout=3000;");
        }

        public static void ExecuteNonQuery(string ConnectionString, string query)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        public static DataTable ExecuteQuery(string ConnectionString, string query, string tableName)
        {
            DataTable table;
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                using (SqlDataAdapter adapter = new SqlDataAdapter(query, connection))
                {
                    DataSet dataSet = new DataSet();
                    adapter.Fill(dataSet, tableName);
                    dataSet.Tables[0].TableName = tableName;
                    table = dataSet.Tables[0];
                }
            }
            return table;
        }

    }
}