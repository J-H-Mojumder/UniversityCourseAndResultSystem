using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UniversityCourseAndResultSystem.DAL;
using UniversityCourseAndResultSystem.Models;

namespace UniversityCourseAndResultSystem.BLL
{
    public class ClassScheduleManager
    {
        private ClassScheduleGateway aClassScheduleViewGateway;

        public ClassScheduleManager()
        {
            aClassScheduleViewGateway = new ClassScheduleGateway();
        }

        public List<ClassSchedule> GetAllClassScheduleViewsByDeptId(int deptId)
        {
            return aClassScheduleViewGateway.GetAllClassScheduleViewsByDeptId(deptId);
        }
        public string UnAllocateClassRoom()
        {
            int rowAffected = aClassScheduleViewGateway.UnAllocateClassRoom();
            if (rowAffected > 0)
            {
                return "Class Room Unallocated Successfully!!!";
            }
            else
            {
                return "Unallocated Failed!!";
            }
        }
    }
}