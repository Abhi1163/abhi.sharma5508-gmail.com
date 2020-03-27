using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EditPdfForm.Models;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System.IO;

namespace EditPdfForm.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {

            //string oldFile = @"Cardio ATCG Medicare (3).pdf";
            //string newFile = @"D:\FilledForm.pdf";            
            //// open the reader
            //PdfReader reader = new PdfReader(oldFile);
            //// AcroFields
            //AcroFields form = reader.AcroFields;
            //var fieldKeys = form.Fields.Keys;
            //if (!fieldKeys.IsReadOnly)
            //{
            //    foreach (string fieldKey in fieldKeys)
            //    {
            //        //Replace  Form field with my custom data              
            //        form.SetField(fieldKey, "MyCustomAddress");

            //    }
            //}            

            //Rectangle size = reader.GetPageSizeWithRotation(1);
            //Document document = new Document(size);

            //// open the writer
            //FileStream fs = new FileStream(newFile, FileMode.Create, FileAccess.Write);
            //PdfWriter writer = PdfWriter.GetInstance(document, fs);
            //document.Open();

            //// the pdf content
            //PdfContentByte cb = writer.DirectContent;

            //// select the font properties
            //BaseFont bf = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
            //cb.SetColorFill(BaseColor.DARK_GRAY);
            //cb.SetFontAndSize(bf, 11);

            //// write the text in the pdf content
            //cb.BeginText();
            //string text = "Abhishek";            
            //cb.ShowTextAligned(1, text, 120, 515, 0);
            //cb.EndText();


            //cb.BeginText();
            //text = "13584695";
            //// put the alignment and coordinates here
            //cb.ShowTextAligned(2, text, 150, 500, 0);
            //cb.EndText();


            //cb.BeginText();
            //text = "8377017377";
            //// put the alignment and coordinates here
            //cb.ShowTextAligned(2, text, 180, 485, 0);
            //cb.EndText();

            //// create the new page and add it to the pdf
            //PdfImportedPage page = writer.GetImportedPage(reader, 1);
            //cb.AddTemplate(page, 0, 0);

            //// close the streams and voilá the file should be changed :)
            //document.Close();
            //fs.Close();
            //writer.Close();
            //reader.Close();


            string newFile = @"D:\FilledForm.pdf";
            //Multi-Page
            using (var readerr = new PdfReader(@"E:\EditPdfForm\EditPdfForm\Cardio ATCG Medicare (3).pdf"))
            {
             
                using (var fileStream = new FileStream(newFile, FileMode.Create, FileAccess.Write))
                {

                    // AcroFields
                    PdfStamper pdfStamper = new PdfStamper(readerr, fileStream);
                    AcroFields pdfForm = pdfStamper.AcroFields;
                    var fieldKeys = pdfForm.Fields.Keys;
                    foreach (string fieldKey in fieldKeys)
                    {
                        //Replace  Form field with my custom data              
                        pdfForm.SetField(fieldKey, "MyCustomAddress");

                    }
                    pdfStamper.FormFlattening = true;

                    var documentt = new Document(readerr.GetPageSizeWithRotation(1));
                    var writerr = PdfWriter.GetInstance(documentt, fileStream);

                    documentt.Open();

                    for (var i = 1; i <= readerr.NumberOfPages; i++)
                    {
                        documentt.NewPage();

                        var baseFont = BaseFont.CreateFont(BaseFont.HELVETICA_BOLD, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                        var importedPage = writerr.GetImportedPage(readerr, i);

                        var contentByte = writerr.DirectContent;
                        contentByte.SetColorFill(BaseColor.DARK_GRAY);
                        contentByte.SetFontAndSize(baseFont, 12);
                        if (i == 1)
                        {
                            contentByte.BeginText();
                            string text = "Abhishek";
                            contentByte.ShowTextAligned(1, text, 120, 515, 0);
                            contentByte.EndText();


                            contentByte.BeginText();
                            text = "13584695";
                            // put the alignment and coordinates here
                            contentByte.ShowTextAligned(2, text, 150, 500, 0);
                            contentByte.EndText();


                            contentByte.BeginText();
                            text = "8377017377";
                            // put the alignment and coordinates here
                            contentByte.ShowTextAligned(2, text, 182, 485, 0);
                            contentByte.EndText();
                        }
                      
                        
                        contentByte.AddTemplate(importedPage, 0, 0);
                        
                    }

                    
                    
                    // close the pdf  
                    pdfStamper.Close();

                    documentt.Close();
                    writerr.Close();
                }
            }
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
