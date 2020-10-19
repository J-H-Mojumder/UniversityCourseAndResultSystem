using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UniversityCourseAndResultSystem.Models
{
    public class Teacher
    {
        public int Id { get; set; }
        [Required]
        //[RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Name cannot be a number")]

        [Display(Name = "Name")]
        public string TeacherName { get; set; }
        [Required]
        [Display(Name = "Address")]
        public string TeacherAddress { get; set; }
        [EmailAddress]
        [Required]
        [Display(Name = "Email")]
        public string TeacherEmail { get; set; }
       
        [Required(ErrorMessage = "Mobile Number is required.")]
        //[RegularExpression(@"^([0-9]{10})$", ErrorMessage = "Invalid Mobile Number.")] 
        //[StringLength(14, MinimumLength = 11)] 
        [Display(Name = "Contact No")]
        public string TeacherContactNo { get; set; }
        [Required]
        [Range(0.0, Double.MaxValue, ErrorMessage = "Credit must be a Postive Number")]
        [Display(Name = "Credit to be taken")]
        public double TeacherCredit { get; set; }
        [Required]
        [Display(Name = "Designation")]
        public int TeacherDesignationId { get; set; }
        [Required]
        [Display(Name = "Department")]
        public int TeacherDepartmentId { get; set; }

        public double CourseTaken { get; set; }
    }
}