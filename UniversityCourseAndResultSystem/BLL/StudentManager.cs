using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UniversityCourseAndResultSystem.DAL;
using UniversityCourseAndResultSystem.Models;

namespace UniversityCourseAndResultSystem.BLL
{
    public class StudentManager
    {
        private StudentGateway aStudentGateway;

        public StudentManager()
        {
            aStudentGateway = new StudentGateway();
        }
       

        public List<Student> GetAllStudents()
        {
            return aStudentGateway.GetAllStudents();
        }

        public Student GetStudentById(int studentId)
        {
            return aStudentGateway.GetStudentById(studentId);
        }


        public string GetLastStudentRegNo(int deptId, string year)
        {
            return aStudentGateway.GetLastStudentRegNo(deptId, year);
        }
        public string InsertNewStudent(Student student)
        {
            if (aStudentGateway.IsEmailExist(student))
            {
                return "Yourr Email ID Already Exist!!";
            }
            else
            {
                int rowAffected = aStudentGateway.InsertNewStudent(student);
                return rowAffected > 0 ? "Registered Successfully!!" : "Saved Failed!!";
            }
        }

    }
}