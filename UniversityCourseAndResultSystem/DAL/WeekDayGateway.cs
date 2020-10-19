using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using UniversityCourseAndResultSystem.Models;

namespace UniversityCourseAndResultSystem.DAL
{
    public class WeekDayGateway
    {
        private string conString = DBConnection.SQLDBConnString();
        private SqlConnection connection;
        private SqlDataReader sqlDataReader;
        private SqlCommand cmd;

        public WeekDayGateway()
        {
            connection = new SqlConnection(conString);
        }
        public List<WeekDay> GetAllDay()
        {
            string query = @"SELECT * from WeekDay order by id";
            cmd = new SqlCommand(query, connection);
            connection.Open();
            sqlDataReader = cmd.ExecuteReader();
            List<WeekDay> weekDayList = new List<WeekDay>();
            while (sqlDataReader.Read())
            {
                WeekDay aWeekDay = new WeekDay();
                aWeekDay.Id = Convert.ToInt32(sqlDataReader["id"]);
                aWeekDay.Day = sqlDataReader["Day"].ToString();
                weekDayList.Add(aWeekDay);

            }
            connection.Close();
            return weekDayList;
        }
    }
}