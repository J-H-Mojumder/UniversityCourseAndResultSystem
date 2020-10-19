using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using UniversityCourseAndResultSystem.Models;

namespace UniversityCourseAndResultSystem.DAL
{
    public class ClassScheduleGateway
    {
        private string conString = DBConnection.SQLDBConnString();
        private SqlConnection connection;
        private SqlDataReader sqlDataReader;
        private SqlCommand cmd;


        public ClassScheduleGateway()
        {
            connection = new SqlConnection(conString);
        }

        public List<ClassSchedule> GetAllClassScheduleViewsByDeptId(int deptId)
        {
            string query = @"SELECT c.CourseCode, c.CourseName,wd.DayShortName,ca.TimeFrom,
                 ca.TimeTo,ca.Status,cr.RoomNo
                 FROM  ClassRoomAllocation ca
                 Left join WeekDay wd ON wd.id=ca.SevenDayWeekID
                 Inner join ClassRoom cr ON cr.id=ca.RoomNoID
                 Right JOIN Course c ON c.id=ca.CourseID and ca.Status!=1
                 where c.DepartmentID='" + deptId + "' order by CourseCode";
            cmd = new SqlCommand(query, connection);
            connection.Open();
            sqlDataReader = cmd.ExecuteReader();
            List<ClassSchedule> classScheduleList = new List<ClassSchedule>();
            while (sqlDataReader.Read())
            {
                if (sqlDataReader["Status"].ToString() == "1")
                {
                    continue;
                }
                ClassSchedule aClassSchedule = new ClassSchedule();
                aClassSchedule.CourseCode = sqlDataReader["CourseCode"].ToString();
                aClassSchedule.CourseName = sqlDataReader["CourseName"].ToString();
                aClassSchedule.DayShortName = sqlDataReader["DayShortName"].ToString();
                aClassSchedule.TimeFrom = sqlDataReader["TimeFrom"].ToString();
                if (aClassSchedule.TimeFrom != "" && Convert.ToInt32(aClassSchedule.TimeFrom) >= 1200)
                {
                    int holdTime = Convert.ToInt32(aClassSchedule.TimeFrom) - 1200;
                    string timeFrom = "";
                    if (holdTime.ToString().Length == 1)
                    {
                        timeFrom = "12:00" + " PM";
                    }
                    else if (holdTime.ToString().Length == 2)
                    {
                        timeFrom = "12:" + holdTime + " PM";
                    }
                    else if (holdTime.ToString().Length == 3)
                    {
                        string timeString = holdTime.ToString();
                        char[] timeArray = timeString.ToCharArray();
                        timeFrom = "0" + timeArray[0] + ":" + timeArray[1] + timeArray[2] + " PM";
                    }
                    else
                    {
                        string timeString = holdTime.ToString();
                        char[] timeArray = timeString.ToCharArray();
                        timeFrom = "" + timeArray[0] + timeArray[1] + ":" + timeArray[2] + timeArray[3] + " PM";
                    }
                    aClassSchedule.TimeFrom = timeFrom;
                }
                else if (aClassSchedule.TimeFrom != "" && Convert.ToInt32(aClassSchedule.TimeFrom) < 1200)
                {
                    string timeFromString = aClassSchedule.TimeFrom.ToString();
                    char[] timeFromArray = timeFromString.ToCharArray();
                    aClassSchedule.TimeFrom = "" + timeFromArray[0] + timeFromArray[1] + ":" + timeFromArray[2] + timeFromArray[3] + " AM";
                }
                aClassSchedule.TimeTo = sqlDataReader["TimeTo"].ToString();
                if (aClassSchedule.TimeTo != "" && Convert.ToInt32(aClassSchedule.TimeTo) >= 1200)
                {
                    int holdTime1 = Convert.ToInt32(aClassSchedule.TimeTo) - 1200;
                    string timeTo = "";
                    if (holdTime1.ToString().Length == 1)
                    {
                        timeTo = "12:00" + " PM";
                    }
                    else if (holdTime1.ToString().Length == 2)
                    {
                        timeTo = "12:" + holdTime1 + " PM";
                    }
                    else if (holdTime1.ToString().Length == 3)
                    {
                        string holdTime = holdTime1.ToString();
                        char[] timeToArray = holdTime.ToCharArray();
                        timeTo = "0" + timeToArray[0] + ":" + timeToArray[1] + timeToArray[2] + " PM";
                    }
                    else
                    {
                        string holdTime = holdTime1.ToString();
                        char[] timeToArray = holdTime.ToCharArray();
                        timeTo = "" + timeToArray[0] + timeToArray[1] + ":" + timeToArray[2] + timeToArray[3] + " PM";
                    }
                    aClassSchedule.TimeTo = timeTo;
                }
                else if (aClassSchedule.TimeTo != "" && Convert.ToInt32(aClassSchedule.TimeTo) < 1200)
                {
                    string holdTime = aClassSchedule.TimeTo.ToString();
                    char[] timeToArray = holdTime.ToCharArray();
                    aClassSchedule.TimeTo = "" + timeToArray[0] + timeToArray[1] + ":" + timeToArray[2] + timeToArray[3] + " AM";
                }
                aClassSchedule.RoomName = sqlDataReader["RoomNo"].ToString();
                classScheduleList.Add(aClassSchedule);

            }
            connection.Close();
            return classScheduleList;
        }

        public int UnAllocateClassRoom()
        {
            string query = @"UPDATE ClassRoomAllocation SET Status = '1' ";
            cmd = new SqlCommand(query, connection);
            connection.Open();
            int rowAffect = cmd.ExecuteNonQuery();
            connection.Close();
            return rowAffect;
        }
    }
}