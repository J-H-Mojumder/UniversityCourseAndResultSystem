﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UniversityCourseAndResultSystem.Models
{
    public class Result
    {
        public int Id { get; set; }
        [Required]
        public int StudentId { get; set; }
        [Required]
        public int CourseId { get; set; }
        [Required]
        public int GradeId { get; set; }

        public string CourseName { get; set; }
        public string CourseCode { get; set; }
        public string Grade { get; set; }
    }
}