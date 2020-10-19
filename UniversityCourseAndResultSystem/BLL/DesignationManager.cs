using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UniversityCourseAndResultSystem.DAL;
using UniversityCourseAndResultSystem.Models;

namespace UniversityCourseAndResultSystem.BLL
{
    public class DesignationManager
    {
        private DesignationGateway aDesignationGateway;

        public DesignationManager()
        {
            aDesignationGateway = new DesignationGateway();
        }



        public List<Designation> GetAllDesignations()
        {
            return aDesignationGateway.GetAllDesignations();
        }
    }
}