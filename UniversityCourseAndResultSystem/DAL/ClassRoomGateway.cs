using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using UniversityCourseAndResultSystem.Models;

namespace UniversityCourseAndResultSystem.DAL
{
    public class ClassRoomGateway
    {
        private string conString = DBConnection.SQLDBConnString();
        private SqlConnection connection;
        private SqlDataReader sqlDataReader;

        private SqlCommand cmd;

        public ClassRoomGateway()
        {
            connection = new SqlConnection(conString);
        }
        public int AllocateNewClassRoom(ClassRoomAllocation classRoomAllocation)
        {
            
            string query = @"insert into ClassRoomAllocation (TimeFrom, TimeTo,Status,DepartmentId,CourseId,RoomNoId,SevenDayWeekId) values(@TimeFrom, @TimeTo,@Status,@DepartmentId,@CourseId,@RoomNoId,@SevenDayWeekId)";
            cmd = new SqlCommand(query, connection);
            //cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@TimeFrom", classRoomAllocation.TimeFrom);
            cmd.Parameters.AddWithValue("@TimeTo", classRoomAllocation.TimeTo);
            cmd.Parameters.AddWithValue("@Status", "0"); 
                cmd.Parameters.AddWithValue("@DepartmentId", classRoomAllocation.DepartmentId);
           
            cmd.Parameters.AddWithValue("@CourseId", classRoomAllocation.CourseId);
            cmd.Parameters.AddWithValue("@RoomNoId", classRoomAllocation.RoomNoId);
            cmd.Parameters.AddWithValue("@SevenDayWeekId", classRoomAllocation.SevenDayWeekId);

            connection.Open();
            int rowAffect = cmd.ExecuteNonQuery();
            connection.Close();
            return rowAffect; 
        }

       
        public List<ClassRoom> GetAllClassRooms()
        {
            string query = @"SELECT *from ClassRoom order by RoomNo";
            cmd = new SqlCommand(query, connection);
            connection.Open();
            sqlDataReader = cmd.ExecuteReader();
            List<ClassRoom> classRoomList = new List<ClassRoom>();
            while (sqlDataReader.Read())
            {
                ClassRoom aClassRoom = new ClassRoom();
                aClassRoom.Id = Convert.ToInt32(sqlDataReader["id"]);
                aClassRoom.RoomNo = sqlDataReader["RoomNo"].ToString();
                classRoomList.Add(aClassRoom);

            }
            connection.Close();
            return classRoomList;
        }
        public bool IsAllocationPossible(ClassRoomAllocation classRoomAllocation)
        {
            string query = @"SELECT * from ClassRoomAllocation where SevenDayWeekID ='" + classRoomAllocation.SevenDayWeekId + "' and RoomNoID ='" + classRoomAllocation.RoomNoId + "'  and Status=0 order by TimeFrom";
            cmd = new SqlCommand(query, connection);
            connection.Open();
            sqlDataReader = cmd.ExecuteReader();
            List<ClassRoomAllocation> classRoomAllocationList = new List<ClassRoomAllocation>();
            while (sqlDataReader.Read())
            {
                ClassRoomAllocation aClassRoomAllocation = new ClassRoomAllocation();
                aClassRoomAllocation.Id = Convert.ToInt32(sqlDataReader["id"]);
                aClassRoomAllocation.TimeFrom = sqlDataReader["TimeFrom"].ToString();
                aClassRoomAllocation.TimeTo = sqlDataReader["TimeTo"].ToString();
                classRoomAllocationList.Add(aClassRoomAllocation);
            }
            connection.Close();
            int[] tvalue = new int[3000];
            foreach (ClassRoomAllocation c in classRoomAllocationList)
            {
                for (int i = Convert.ToInt32(c.TimeFrom); i <= Convert.ToInt32(c.TimeTo); i++)
                {
                    tvalue[i] = 1;
                }
                tvalue[Convert.ToInt32(c.TimeTo)] = 0;
            }
            for (int i = Convert.ToInt32(classRoomAllocation.TimeFrom); i <= Convert.ToInt32(classRoomAllocation.TimeTo); i++)
            {
                if (tvalue[i] == 1)
                {
                    return false;
                }
            }
            return true;
        }
    }
}