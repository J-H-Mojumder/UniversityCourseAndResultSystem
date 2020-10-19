using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UniversityCourseAndResultSystem.DAL;
using UniversityCourseAndResultSystem.Models;

namespace UniversityCourseAndResultSystem.BLL
{
    public class WeekDayManager
    {
        private WeekDayGateway aWeekDayGateway;

        public WeekDayManager()
        {
            aWeekDayGateway = new WeekDayGateway();
        }
        public List<WeekDay> GetAllDay()
        {
            return aWeekDayGateway.GetAllDay();
        }
    }
}