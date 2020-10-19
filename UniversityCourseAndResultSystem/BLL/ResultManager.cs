using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using UniversityCourseAndResultSystem.DAL;
using UniversityCourseAndResultSystem.Models;

namespace UniversityCourseAndResultSystem.BLL
{
    public class ResultManager
    {
        private ResultGateway aResultGateway;

        public ResultManager()
        {
            aResultGateway = new ResultGateway();
        }

        public string SaveAResult(Result result)
        {
            if (aResultGateway.SaveAResult(result) == 1)
            {
                return "Result Saved Successfully!!";
            }
            else
            {
                return "Saved Failed!!";
            }
        }

        public List<Grade> GetAllGrades()
        {
            return aResultGateway.GetAllGrades();
        }

        public List<Course> GetAllTakenCourses(int studentId)
        {
            return aResultGateway.GetAllTakenCourses(studentId);
        }

        public List<Result> GetAllResultForView(int studentId)
        {
            return aResultGateway.GetAllResultForView(studentId);
        }

        public DataTable GetResultView(string StudentId)
        {
            DataTable table = aResultGateway.GetResultView(StudentId);
            return table;
        }
        public DataTable GetResultViewStudentInfo(string StudentId)
        {
            DataTable table = aResultGateway.GetResultViewStudentInfo(StudentId);
            return table;
        }

        public DataTable GetResultView()
        {
            DataTable table = aResultGateway.GetResultView();
            return table;
        }
    }
}