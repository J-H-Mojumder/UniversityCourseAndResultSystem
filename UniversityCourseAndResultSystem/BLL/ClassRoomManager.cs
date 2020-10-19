using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UniversityCourseAndResultSystem.DAL;
using UniversityCourseAndResultSystem.Models;

namespace UniversityCourseAndResultSystem.BLL
{
    public class ClassRoomManager
    {
        private ClassRoomGateway aClassRoomGateway;

        public ClassRoomManager()
        {
            aClassRoomGateway = new ClassRoomGateway();
        }
        public string AllocateNewClassRoom(ClassRoomAllocation classRoomAllocation)
        {

            if (!aClassRoomGateway.IsAllocationPossible(classRoomAllocation))
            {
                return "Not Possible";
            }
            else
            {

                int rowAffected = aClassRoomGateway.AllocateNewClassRoom(classRoomAllocation);
               
                if (rowAffected > 0)
                {
                    return "Class Room Allocated Successfully!!";
                }
                else
                {
                    return "Allocated Failed!!";
                }
            }

        }
        public List<ClassRoom> GetAllClassRooms()
        {
            return aClassRoomGateway.GetAllClassRooms();
        }
    }
}