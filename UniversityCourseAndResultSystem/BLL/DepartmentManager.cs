using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UniversityCourseAndResultSystem.DAL;
using UniversityCourseAndResultSystem.Models;

namespace UniversityCourseAndResultSystem.BLL
{
    public class DepartmentManager
    {
        private DepartmentGateway aDepartmentGateway;

        public DepartmentManager()
        {
            aDepartmentGateway = new DepartmentGateway();
        }

       
        public List<Department> GetAllDepartments()
        {
            return aDepartmentGateway.GetAllDepartments();
        }

        public string GetDepartmentCode(int deptId)
        {
            return aDepartmentGateway.GetDepartmentName(deptId);
        }
        public string SaveDepartment(Department department)
        {
            if (aDepartmentGateway.IsCodeExist(department))
            {
                return "This Department Code Already Exist!!";
            }
            else if (aDepartmentGateway.IsNameExist(department))
            {
                return "This Department Name Already Exist!!";
            }
            else
            {
                int rowAffected = aDepartmentGateway.SaveDepartment(department);
                if (rowAffected > 0)
                {
                    return "Department Successfully Saved!!";
                }
                else
                {
                    return "Saved Failed!!";
                }

            }
        }

    }
}