using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UniversityCourseAndResultSystem.DAL;
using UniversityCourseAndResultSystem.Models;

namespace UniversityCourseAndResultSystem.BLL
{
    public class TeacherManager
    {
        private TeacherGateway aTeacherGateway;

        public TeacherManager()
        {
            aTeacherGateway = new TeacherGateway();
        }
         
        public List<Teacher> GetAllTeachersByDepartment(int departmentId)
        {
            return aTeacherGateway.GetAllTeacherByDepartment(departmentId);
        }

        public Teacher GetTeacherInformationById(int teacherId)
        {
            return aTeacherGateway.GetTeacherInformationById(teacherId);
        }

        public double GetTotalTakenCourses(int teacherId)
        {
            return aTeacherGateway.GetTotalTakenCourses(teacherId);
        }

        public double GetTeacherCreditLimit(int teacherId)
        {
            return aTeacherGateway.GetTeacherCreditLimit(teacherId);
        }
        public string SaveTeacher(Teacher teacher)
        {
            if (aTeacherGateway.IsEmailExist(teacher))
            {
                return "Your Email ID Already Exist!!";
            }
            else
            {
                int rowAffected = aTeacherGateway.SaveTeacher(teacher);
                return rowAffected > 0 ? "Teacher Saved Successfully!!" : "Saved Failed!!";
            }
        }
    }
}