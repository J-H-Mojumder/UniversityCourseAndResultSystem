using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace UniversityCourseAndResultSystem.DAL
{
    public class pdfPage : iTextSharp.text.pdf.PdfPageEventHelper
    {
        protected Font footer
        {
            get
            {
                // create a basecolor to use for the footer font, if needed.
                BaseColor grey = new BaseColor(128, 128, 128);
                Font font = FontFactory.GetFont("TIMES_ROMAN", 9, Font.NORMAL, grey);
                return font;
            }
        }
        //override the OnStartPage event handler to add our header
        public override void OnStartPage(PdfWriter writer, Document document)
        {
            Rectangle page = document.PageSize;
            PdfPTable head = new PdfPTable(1);
            head.TotalWidth = page.Width - 20; 
            Phrase phrase = new Phrase(DateTime.Now.ToString("dd/MM/yyyy"), new Font(Font.FontFamily.TIMES_ROMAN, 12));
            PdfPCell c = new PdfPCell(phrase);
            c.Border = Rectangle.NO_BORDER; 
            c.VerticalAlignment = Element.ALIGN_LEFT;
            c.HorizontalAlignment = Element.ALIGN_BOTTOM;
            head.AddCell(c);
            //head.WriteSelectedRows(0, -1, 0, page.Height - 10, writer.DirectContent); 
            head.WriteSelectedRows(0, -1, 0, 20, writer.DirectContent);
        }

        //override the OnPageEnd event handler to add our footer
        public override void OnEndPage(PdfWriter writer, Document document)
        {
            Rectangle page = document.PageSize;
            PdfPTable foot = new PdfPTable(1);
            foot.TotalWidth = page.Width - 20;
            int pa = writer.CurrentPageNumber;
            Phrase phrase = new Phrase(("Page "+ pa).ToString() + " of " + pa, new Font(Font.FontFamily.TIMES_ROMAN, 12));
            PdfPCell c = new PdfPCell(phrase);
            c.Border = Rectangle.NO_BORDER;
            c.VerticalAlignment = Element.ALIGN_BOTTOM;
            c.HorizontalAlignment = Element.ALIGN_RIGHT;
            foot.AddCell(c);
            foot.WriteSelectedRows(0, -1, 0, 20, writer.DirectContent);
        }
    }
}