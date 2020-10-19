using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UniversityCourseAndResultSystem.DAL;
using UniversityCourseAndResultSystem.Models;

namespace UniversityCourseAndResultSystem.BLL
{
    public class SemesterManager
    {
        private SemesterGateway aSemesterGateway;

        public SemesterManager()
        {
            aSemesterGateway = new SemesterGateway();
        }



        public List<Semester> GetAllSemesters()
        {
            return aSemesterGateway.GetAllSemesters();
        }
    }
}