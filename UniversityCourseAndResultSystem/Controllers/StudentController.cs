using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.draw;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UniversityCourseAndResultSystem.BLL;
using UniversityCourseAndResultSystem.DAL;
using UniversityCourseAndResultSystem.Models;

namespace UniversityCourseAndResultSystem.Controllers
{
    public class StudentController : Controller
    {
        
        [HttpGet]
        public ActionResult Index()
        {
            ViewBag.Departments = new TeacherController().GetAllDepartmentList();
            ViewBag.Message = "";
            return View();
        }

        
        public static string Reverse(string s)
        {
            char[] charArray = s.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }

         
        [HttpGet]
        public ActionResult StudentEnrollInCourse()
        {
            ViewBag.Message = ""; 
            ViewBag.StudentsRegNo = GetAllStudentLists();
            return View();
        }

        [HttpPost]
        public ActionResult StudentEnrollInCourse(EnrollInCourse enrollCourse)
        {
            if (ModelState.IsValid)
            {
                ViewBag.StudentsRegNo = GetAllStudentLists();
                string result = new EnrollCourseManager().AssignNewCourseToStudent(enrollCourse);
                ViewBag.Message = result;
                ModelState.Clear();
                return View();
            }
            else
            {
                ViewBag.Message = "";
                ViewData["Message"] = "";
                ViewBag.StudentsRegNo = GetAllStudentLists();
                return View();
            }
        }

        
        [HttpGet]
        public ActionResult StudentResult()
        {
            ViewBag.Message = "";
            ViewData["Message"] = "";
            ViewBag.StudentsRegNo = GetAllStudentLists();
            ViewBag.Grades = GetAllGradeLists();
            return View();
        }

        [HttpPost]
        public ActionResult StudentResult(Result studentResultModel)
        {
            if (ModelState.IsValid)
            {
                ViewBag.StudentsRegNo = GetAllStudentLists();
                ViewBag.Grades = GetAllGradeLists();
                string result = new ResultManager().SaveAResult(studentResultModel);
                ViewBag.Message =  result;
                ModelState.Clear();
                return View();
            }
            else
            {
                ViewBag.Message = "";
                
                ViewBag.StudentsRegNo = GetAllStudentLists();
                ViewBag.Grades = GetAllGradeLists();
                return View();
            }

        }


        public IEnumerable<SelectListItem> GetAllGradeLists()
        {
            List<SelectListItem> gradeListItems = new List<SelectListItem>()
            {
                new SelectListItem(){Text = "---Select---",Value = ""},
            };
            List<Grade> gradeList = new ResultManager().GetAllGrades();
            foreach (Grade grade in gradeList)
            {
                SelectListItem gradeListItem = new SelectListItem();
                gradeListItem.Text = grade.GradeLetter;
                gradeListItem.Value = grade.Id.ToString();
                gradeListItems.Add(gradeListItem);
            }
            return gradeListItems;
        }

         
        public ActionResult ViewStudentResult()
        {
            ViewBag.StudentsRegNo = GetAllStudentLists();
            return View();
        }

        public IEnumerable<SelectListItem> GetAllStudentLists()
        {
            List<SelectListItem> studentListItems = new List<SelectListItem>()
            {
                new SelectListItem(){Text = "---Select---",Value = ""},
            };
            List<Student> students = new StudentManager().GetAllStudents();
            foreach (Student student in students)
            {
                SelectListItem studentListItem = new SelectListItem();
                studentListItem.Text = student.StudentRegNo;
                studentListItem.Value = student.Id.ToString();
                studentListItems.Add(studentListItem);
            }

            return studentListItems;
        }

        [HttpPost]
        public ActionResult Index(Student student)
        {
            if (ModelState.IsValid)
            {
                ViewBag.Departments = new TeacherController().GetAllDepartmentList();
                string deptCode = new DepartmentManager().GetDepartmentCode(student.DepartmentId);
                string holdYear = student.StudentAddDate.Substring(0, 4);
                string year = holdYear;
                string lastRegNo = new StudentManager().GetLastStudentRegNo(student.DepartmentId, year);
                if (lastRegNo == "")
                {
                    student.StudentRegNo = deptCode + "-" + year + "-" + "001";
                    ViewBag.Message = new StudentManager().InsertNewStudent(student) 
                        + "\n Student Name :" + student.StudentName + "'s Email ID : " + student.StudentEmail + "\n Contact No. : " + student.StudentContactNo  + " and  RegNo is " + student.StudentRegNo;
                }
                else
                {
                    string temporaryValue = "";
                    student.StudentRegNo = deptCode + "-" + year + "-";
                    for (int i = lastRegNo.Length - 1; i >= lastRegNo.Length - 1 - 2; i--)
                    {
                        temporaryValue += lastRegNo[i];
                    }
                    temporaryValue = Reverse(temporaryValue);
                    int temporaryRegNo = Convert.ToInt32(temporaryValue);
                    temporaryRegNo++;
                    temporaryValue = temporaryRegNo.ToString();
                    if (temporaryValue.Length == 1)
                    {
                        student.StudentRegNo += "00" + temporaryValue;
                    }
                    else if (temporaryValue.Length == 2)
                    {
                        student.StudentRegNo += "0" + temporaryValue;
                    }
                    else
                    {
                        student.StudentRegNo += temporaryValue;
                    }

                    ViewBag.Message = new StudentManager().InsertNewStudent(student);
                    if (ViewBag.Message.Equals("Registered Successfully!!"))
                    {
                        ViewBag.Message += "\n " + student.StudentName + "'s Email ID : " +student.StudentEmail +" Contact No. : "+ student.StudentContactNo  +" and RegNo is " + student.StudentRegNo;


                    }
                }
                ModelState.Clear();
                return View();
            }
            else
            {
                ViewBag.Message = "";
                return View();
            }
        } 

        public JsonResult GetStudentByStudentId(int studentId)
        {
            var students = new StudentManager().GetStudentById(studentId);
            return Json(students);
        }

        public JsonResult GetCourseByStudentId(string studentId)
        {
            var courses = new CourseManager().GetCourseByStudentId(studentId);
            return Json(courses);
        }

        public JsonResult GetTakenCourseByStudentId(int studentId)
        {
            var courses = new ResultManager().GetAllTakenCourses(studentId);
            return Json(courses);
        }
        public JsonResult GetAllCoursesByDeptId(int departmentId)
        {
            var courses = new CourseManager().GetAllCoursesByDeptId(departmentId);
            return Json(courses);
        }

        public JsonResult GetAllResultByStudentId(int studentId)
        { 
            var courses = new ResultManager().GetAllResultForView(studentId); 
            return Json(courses); 
        }
   
        public JsonResult GetAllResultByStudentIdWithparameer(string StudentId)
        {
             
            Response.Clear();
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment; filename='Result'.pdf");
            Document document = new Document();
            document = new Document(PageSize.A4, 50f, 50f, 30f, 30f);
            MemoryStream ms = new MemoryStream();
            PdfWriter writer = PdfWriter.GetInstance(document, Response.OutputStream);
            pdfPage page = new pdfPage();
            writer.PageEvent = page;
            document.Open();

            DataTable dt = new ResultManager().GetResultViewStudentInfo(StudentId);

            float[] titwidth = new float[2] { 5, 200 };
            PdfPCell cell;
            PdfPTable dth = new PdfPTable(titwidth);
            dth.WidthPercentage = 100;
            cell = new PdfPCell();
            cell.HorizontalAlignment = 1;
            cell.VerticalAlignment = 1;
            cell.Rowspan = 5;
            cell.BorderWidth = 0f;
            dth.AddCell(cell);
            cell = new PdfPCell(new Phrase("UNIVERSITY COURSE AND RESULT SYSTEM", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 18, iTextSharp.text.Font.BOLD)));
            cell.HorizontalAlignment = 1;
            cell.VerticalAlignment = 1;
            cell.BorderWidth = 0f; 
            dth.AddCell(cell);

            cell = new PdfPCell(new Phrase("FINAL PROJECT", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 16, iTextSharp.text.Font.BOLD)));
            cell.HorizontalAlignment = 1;
            cell.VerticalAlignment = 1;
            cell.BorderWidth = 0f; 
            dth.AddCell(cell);
            cell = new PdfPCell(new Phrase("RESULT", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 14, iTextSharp.text.Font.NORMAL)));
            cell.HorizontalAlignment = 1;
            cell.VerticalAlignment = 1;
            cell.BorderWidth = 0f; 
            dth.AddCell(cell); 
            document.Add(dth);

            LineSeparator line = new LineSeparator(0f, 100, null, Element.ALIGN_CENTER, -2);
            document.Add(line);
            PdfPTable dtempty = new PdfPTable(1);
            cell.BorderWidth = 0f;
            cell.FixedHeight = 5f;
            dtempty.AddCell(cell);
            document.Add(dtempty);


            float[] width1 = new float[3] { 20, 5, 45 };
            PdfPTable pdtc1 = new PdfPTable(width1);
            pdtc1.WidthPercentage = 100;
           
            if (dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];

                cell = new PdfPCell(FormatHeaderPhrase("Name "));
                cell.HorizontalAlignment = 0;
                cell.VerticalAlignment = 1;
                cell.Border = 0; 
                pdtc1.AddCell(cell);

                cell = new PdfPCell(FormatHeaderPhrase(":"));
                cell.HorizontalAlignment = 0;
                cell.VerticalAlignment = 1;
                cell.Border = 0; 
                pdtc1.AddCell(cell);

                cell = new PdfPCell(FormatPhrase(row["StudentName"].ToString()));
                cell.HorizontalAlignment = 0;
                cell.VerticalAlignment = 1;
                cell.Border = 0; 
                pdtc1.AddCell(cell);

                cell = new PdfPCell(FormatHeaderPhrase("Registration No."));
                cell.HorizontalAlignment = 0;
                cell.VerticalAlignment = 1;
                cell.Border = 0; 
                pdtc1.AddCell(cell);

                cell = new PdfPCell(FormatHeaderPhrase(":"));
                cell.HorizontalAlignment = 0;
                cell.VerticalAlignment = 1;
                cell.Border = 0; 
                pdtc1.AddCell(cell);

                cell = new PdfPCell(FormatPhrase(row["StudentRegNo"].ToString()));
                cell.HorizontalAlignment = 0;
                cell.VerticalAlignment = 1;
                cell.Border = 0; 
                pdtc1.AddCell(cell);


                cell = new PdfPCell(FormatHeaderPhrase("Email"));
                cell.HorizontalAlignment = 0;
                cell.VerticalAlignment = 1;
                cell.Border = 0; 
                pdtc1.AddCell(cell);

                cell = new PdfPCell(FormatHeaderPhrase(":"));
                cell.HorizontalAlignment = 0;
                cell.VerticalAlignment = 1;
                cell.Border = 0; 
                pdtc1.AddCell(cell);

                cell = new PdfPCell(FormatPhrase(row["StudentEmail"].ToString()));
                cell.HorizontalAlignment = 0;
                cell.VerticalAlignment = 1;
                cell.Border = 0; 
                pdtc1.AddCell(cell);

                cell = new PdfPCell(FormatHeaderPhrase(" "));
                cell.HorizontalAlignment = 1;
                cell.VerticalAlignment = 1;
                cell.Colspan = 3;
                cell.Border = 0; 
                pdtc1.AddCell(cell);

                document.Add(pdtc1);

            }


            float[] width = new float[3] { 25, 55, 20 };
            PdfPTable pdtc = new PdfPTable(width);
            pdtc.WidthPercentage = 100;
            //pdtc.HeaderRows =3;



            DataTable dtt = new ResultManager().GetResultView(StudentId);

            //DataTable dtt = aManager.GetViewSalesBetweenDate(txtStartDate.Text, txtEndDate.Text);


            cell = new PdfPCell(FormatHeaderPhrase("Course Code"));
            cell.HorizontalAlignment = 1;
            cell.VerticalAlignment = 1;
            cell.BorderColor = BaseColor.LIGHT_GRAY;
            pdtc.AddCell(cell);
            cell = new PdfPCell(FormatHeaderPhrase("Course Name"));
            cell.HorizontalAlignment = 1;
            cell.VerticalAlignment = 1;
            cell.BorderColor = BaseColor.LIGHT_GRAY;
            pdtc.AddCell(cell);
            cell = new PdfPCell(FormatHeaderPhrase("Grade"));
            cell.HorizontalAlignment = 1;
            cell.VerticalAlignment = 1;
            cell.BorderColor = BaseColor.LIGHT_GRAY;
            pdtc.AddCell(cell);
            foreach (DataRow dr in dtt.Rows)
            {
                if (dtt.Rows.Count > 0)
                {
                    cell = new PdfPCell(FormatPhrase(dr["CourseCode"].ToString()));
                    cell.HorizontalAlignment = 1;
                    cell.VerticalAlignment = 1;
                    cell.BorderColor = BaseColor.LIGHT_GRAY;
                    pdtc.AddCell(cell);

                    cell = new PdfPCell(FormatPhrase(dr["CourseName"].ToString()));
                    cell.HorizontalAlignment = 0;
                    cell.VerticalAlignment = 1;
                    cell.BorderColor = BaseColor.LIGHT_GRAY;
                    pdtc.AddCell(cell);
                    cell = new PdfPCell(FormatPhrase(dr["Grade"].ToString()));
                    cell.HorizontalAlignment = 1;
                    cell.VerticalAlignment = 1;
                    cell.BorderColor = BaseColor.LIGHT_GRAY;
                    pdtc.AddCell(cell);


                }
            }

            document.Add(pdtc);


            PdfPTable dtempty1 = new PdfPTable(1);
            dtempty1.WidthPercentage = 100;
            cell = new PdfPCell(FormatPhrase(""));
            cell.VerticalAlignment = 0;
            cell.HorizontalAlignment = 0;
            cell.BorderWidth = 0f;
            cell.FixedHeight = 70f;
            dtempty1.AddCell(cell);
            document.Add(dtempty1);

            float[] widthsig = new float[] { 20, 5, 20, 5, 20 };
            PdfPTable pdtsig = new PdfPTable(widthsig);
            pdtsig.WidthPercentage = 100;
            cell = new PdfPCell(FormatPhrase(""));
            cell.HorizontalAlignment = 1;
            cell.VerticalAlignment = 1;
            cell.Border = 0;
            pdtsig.AddCell(cell);
            cell = new PdfPCell(FormatPhrase(""));
            cell.HorizontalAlignment = 0;
            cell.VerticalAlignment = 1;
            cell.Border = 0;
            pdtsig.AddCell(cell);
            cell = new PdfPCell(FormatPhrase(" "));
            cell.HorizontalAlignment = 1;
            cell.VerticalAlignment = 1;
            cell.Border = 0;
            cell.FixedHeight = 18f;
            pdtsig.AddCell(cell);
            cell = new PdfPCell(FormatPhrase(""));
            cell.HorizontalAlignment = 0;
            cell.VerticalAlignment = 1;
            cell.Border = 0;
            cell.FixedHeight = 18f;
            pdtsig.AddCell(cell);
            cell = new PdfPCell(FormatPhrase("Authority of University"));
            cell.HorizontalAlignment = 1;
            cell.VerticalAlignment = 1;
            cell.Border = 1;
            pdtsig.AddCell(cell);
            document.Add(pdtsig);


            document.Close();
            Response.Flush();
            Response.End();
            //return View(dtt);
            return Json(dtt);
        }
         

        private static Phrase FormatPhrase(string value)
        {
            return new Phrase(value, FontFactory.GetFont(FontFactory.TIMES_ROMAN, 10));
        }
        private static Phrase FormatHeaderPhrase(string value)
        {
            return new Phrase(value, FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12, iTextSharp.text.Font.BOLD));
        }
        private static Phrase FormatHeaderPhrase1(string value)
        {
            return new Phrase(value, FontFactory.GetFont(FontFactory.TIMES_ROMAN, 14, iTextSharp.text.Font.BOLD));
        }
        private static Phrase FormatHeaderColorGreen(string value)
        {
            var TextColor = FontFactory.GetFont(FontFactory.TIMES_ROMAN, 13, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.RED);
            var titleChunk = new Chunk(value, TextColor);
            var phrase = new Phrase(titleChunk);
            return phrase;
        }
    }
}