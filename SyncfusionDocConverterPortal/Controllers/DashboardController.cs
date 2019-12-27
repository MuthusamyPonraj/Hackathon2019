using Syncfusion.DocIO.DLS;
using Syncfusion.DocToPDFConverter;
using Syncfusion.ExcelToPdfConverter;
using Syncfusion.HtmlConverter;
using Syncfusion.Pdf;
using Syncfusion.Presentation;
using Syncfusion.PresentationToPdfConverter;
using Syncfusion.XlsIO;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.IO.Compression;

namespace SyncfusionDocConverterPortal.Controllers
{
    public class DashboardController : Controller
    {
        
        // GET: Dashboard
        public ActionResult Index()
        {
            return View();

        }

        public ActionResult PdfConverter()
        {
            return View("~/Views/Dashboard/PdfConverter.cshtml");
        }

        public ActionResult ImageConverter()
        {
            return View("~/Views/Dashboard/ImageConverter.cshtml");
        }

        public ActionResult HTMLConverter()
        {
            return View("~/Views/Dashboard/HTMLConverter.cshtml");
        }

        public ActionResult RTFConverter()
        {
            return View("~/Views/Dashboard/RTFConverter.cshtml");
        }

        public ActionResult EPUBConverter()
        {
            return View("~/Views/Dashboard/EPUBConverter.cshtml");
        }

        public ActionResult TextConverter()
        {
            return View("~/Views/Dashboard/TextConverter.cshtml");
        }

        public ActionResult ODTConverter()
        {
            return View("~/Views/Dashboard/ODTConverter.cshtml");
        }

        public JsonResult PdfConvertFile(HttpPostedFileBase file)
        {
            bool result = false;
            string resultFileName = string.Empty;
            string validationMessage = string.Empty;
            string filename = string.Empty;
            try
            {
                if (Request.Files != null && Request.Files.Count > 0 &&
                    !String.IsNullOrEmpty(Request.Files[0].FileName) && Request.Files[0].ContentLength > 0)
                {
                    file = Request.Files[0];
                    var ext = System.IO.Path.GetExtension(Request.Files[0].FileName);
                    byte[] fileBytes = new byte[file.ContentLength];
                    var data = file.InputStream.Read(fileBytes, 0, Convert.ToInt32(file.ContentLength));
                    var stream = new MemoryStream(fileBytes);
                    var currpath = string.Empty;
                    switch (ext)
                    {
                        case ".xls":
                        case ".xlsx":
                            using (ExcelEngine excelEngine = new ExcelEngine())
                            {
                                IApplication application = excelEngine.Excel;
                                application.DefaultVersion = ExcelVersion.Excel2013;
                                IWorkbook workbook = application.Workbooks.Open(stream, ExcelOpenType.Automatic);
                                ExcelToPdfConverter excelPdfconverter = new ExcelToPdfConverter(workbook);
                                PdfDocument excelPdfDocument = new PdfDocument();
                                excelPdfDocument = excelPdfconverter.Convert();
                                filename = string.Concat(Path.GetFileNameWithoutExtension(Request.Files[0].FileName), DateTime.Now.ToString("yyyyMMddHHmmssfff", CultureInfo.CurrentCulture), ".pdf");
                                currpath = this.GetTempPathForFile(filename);
                                excelPdfDocument.Save(currpath);
                                result = true;
                            }
                            break;
                        case ".doc":
                        case ".docx":
                            DocToPDFConverter converter = new DocToPDFConverter();
                            PdfDocument pdfDocument = converter.ConvertToPDF(stream);
                            filename = string.Concat(Path.GetFileNameWithoutExtension(Request.Files[0].FileName), DateTime.Now.ToString("yyyyMMddHHmmssfff", CultureInfo.CurrentCulture), ".pdf");
                            currpath = this.GetTempPathForFile(filename);
                            pdfDocument.Save(currpath);
                            result = true;
                            break;
                        case ".html":
                            //Html to pdf
                            //filename = string.Concat(Path.GetFileNameWithoutExtension(Request.Files[0].FileName), DateTime.Now.ToString("yyyyMMddHHmmssfff", CultureInfo.CurrentCulture), ".html");
                            //filename1 = string.Concat(Path.GetFileNameWithoutExtension(Request.Files[0].FileName), DateTime.Now.ToString("yyyyMMddHHmmssfff", CultureInfo.CurrentCulture), ".pdf");
                            //currpath = this.GetTempPathForFile(filename1);
                            //filePath = this.GetTempPathForFile(filename);
                            //Thread t = new Thread(ConvertToPDF);
                            //t.SetApartmentState(ApartmentState.STA);
                            //t.Start();
                            //t.Join();
                            //result = true;
                            break;
                        case ".ppt":
                        case ".pptx":
                            Syncfusion.Presentation.IPresentation pptxDoc = Presentation.Open(stream);
                            pptxDoc.ChartToImageConverter = new Syncfusion.OfficeChartToImageConverter.ChartToImageConverter();
                            PdfDocument pptPdfDocument = PresentationToPdfConverter.Convert(pptxDoc);
                            filename = string.Concat(Path.GetFileNameWithoutExtension(Request.Files[0].FileName), DateTime.Now.ToString("yyyyMMddHHmmssfff", CultureInfo.CurrentCulture), ".pdf");
                            currpath = this.GetTempPathForFile(filename);
                            pptPdfDocument.Save(currpath);
                            pptxDoc.Close();
                            result = true;
                            break;
                        default:
                            validationMessage = "Please upload a valid file for conversion";
                            result = false;
                            break;
                    }
                    resultFileName = "download/result/" + filename;
                }
                else
                {
                    validationMessage = "Please upload a valid file for conversion";
                    result = false;
                }
            }
            catch (Exception ex)
            {
                return this.Json(new { data = "An eroor occured while processing. Please try again later", status = false, filename = string.Empty }, JsonRequestBehavior.AllowGet);
            }

            return this.Json(new { data = validationMessage, status = result, filename = resultFileName }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ImageConvertFile(HttpPostedFileBase file)
        {
            bool result = false;
            string resultFileName = string.Empty;
            string validationMessage = string.Empty;
            string filename = string.Empty;
            try
            {
                if (Request.Files != null && Request.Files.Count > 0 &&
                    !String.IsNullOrEmpty(Request.Files[0].FileName) && Request.Files[0].ContentLength > 0)
                {
                    file = Request.Files[0];
                    var ext = System.IO.Path.GetExtension(Request.Files[0].FileName);
                    byte[] fileBytes = new byte[file.ContentLength];
                    var data = file.InputStream.Read(fileBytes, 0, Convert.ToInt32(file.ContentLength));
                    var stream = new MemoryStream(fileBytes);
                    var currpath = string.Empty;
                    System.Drawing.Image[] images;
                    switch (ext)
                    {
                        case ".xls":
                        case ".xlsx":
                            using (ExcelEngine excelEngine = new ExcelEngine())
                            {
                                IApplication application = excelEngine.Excel;
                                application.DefaultVersion = ExcelVersion.Excel2013;
                                IWorkbook workbook = application.Workbooks.Open(stream, ExcelOpenType.Automatic);
                                IWorksheet sheet = workbook.Worksheets[0];
                                System.Drawing.Image excelImage = sheet.ConvertToImage(1, 1, 10, 20);
                                filename = string.Concat(Path.GetFileNameWithoutExtension(Request.Files[0].FileName), DateTime.Now.ToString("yyyyMMddHHmmssfff", CultureInfo.CurrentCulture), ".png");
                                currpath = this.GetTempPathForFile(filename);
                                excelImage.Save(currpath, System.Drawing.Imaging.ImageFormat.Png);
                                excelEngine.ThrowNotSavedOnDestroy = false;
                                result = true;
                            }
                            break;
                        case ".doc":
                        case ".docx":
                            WordDocument wordDocument = new WordDocument(stream, Syncfusion.DocIO.FormatType.Docx);
                            wordDocument.ChartToImageConverter = new Syncfusion.OfficeChartToImageConverter.ChartToImageConverter();
                            wordDocument.ChartToImageConverter.ScalingMode = Syncfusion.OfficeChart.ScalingMode.Normal;
                            images = wordDocument.RenderAsImages(Syncfusion.DocIO.DLS.ImageType.Bitmap);
                            int i = 0;
                            if (!Directory.Exists(Environment.GetEnvironmentVariable("TEMP") + "\\" + Path.GetFileNameWithoutExtension(Request.Files[0].FileName)))
                            {
                                Directory.CreateDirectory(Environment.GetEnvironmentVariable("TEMP") + "\\" + Path.GetFileNameWithoutExtension(Request.Files[0].FileName));
                            }
                            else
                            {
                               foreach(var imageFile in Directory.GetFiles(Environment.GetEnvironmentVariable("TEMP") + "\\" + Path.GetFileNameWithoutExtension(Request.Files[0].FileName)))
                                {
                                    System.IO.File.Delete(imageFile);
                                }
                            }
                            foreach (System.Drawing.Image image3 in images)
                            {
                                filename = string.Concat(Path.GetFileNameWithoutExtension(Request.Files[0].FileName) + i, DateTime.Now.ToString("yyyyMMddHHmmssfff", CultureInfo.CurrentCulture), ".png");
                                currpath = Path.Combine(Environment.GetEnvironmentVariable("TEMP") + "\\" + Path.GetFileNameWithoutExtension(Request.Files[0].FileName), filename);
                                image3.Save(currpath, System.Drawing.Imaging.ImageFormat.Png);
                               
                                i++;
                            }
                            var zipFile = Environment.GetEnvironmentVariable("TEMP") + "\\" + Path.GetFileNameWithoutExtension(Request.Files[0].FileName) + ".zip";
                            var files = Directory.GetFiles(Environment.GetEnvironmentVariable("TEMP") + "\\" + Path.GetFileNameWithoutExtension(Request.Files[0].FileName));

                            using (var archive = ZipFile.Open(zipFile, ZipArchiveMode.Create))
                            {
                                foreach (var fPath in files)
                                {
                                    archive.CreateEntryFromFile(fPath, Path.GetFileName(fPath));
                                }
                            }
                            result = true;
                            wordDocument.Close();
                            filename =  Path.GetFileNameWithoutExtension(Request.Files[0].FileName) + ".zip";
                            break;
                        case ".ppt":
                        case ".pptx":
                            IPresentation pptxDoc = Presentation.Open(stream);
                            pptxDoc.ChartToImageConverter = new Syncfusion.OfficeChartToImageConverter.ChartToImageConverter();
                            pptxDoc.ChartToImageConverter.ScalingMode = Syncfusion.OfficeChart.ScalingMode.Best;
                            int count = 0;
                            foreach (var slide in pptxDoc.Slides)
                            {
                                System.Drawing.Image pptImage = slide.ConvertToImage(Syncfusion.Drawing.ImageType.Metafile);
                                filename = string.Concat(Path.GetFileNameWithoutExtension(Request.Files[0].FileName) + count, DateTime.Now.ToString("yyyyMMddHHmmssfff", CultureInfo.CurrentCulture), ".png");
                                currpath = Path.Combine(Environment.GetEnvironmentVariable("TEMP") + "\\" + Path.GetFileNameWithoutExtension(Request.Files[0].FileName), filename);
                                pptImage.Save(currpath);
                                pptImage.Dispose();
                                count++;
                            }

                            var pptzipFile = Environment.GetEnvironmentVariable("TEMP") + "\\" + Path.GetFileNameWithoutExtension(Request.Files[0].FileName) + ".zip";
                            var pptfiles = Directory.GetFiles(Environment.GetEnvironmentVariable("TEMP") + "\\" + Path.GetFileNameWithoutExtension(Request.Files[0].FileName));

                            using (var archive = ZipFile.Open(pptzipFile, ZipArchiveMode.Create))
                            {
                                foreach (var fPath in pptfiles)
                                {
                                    archive.CreateEntryFromFile(fPath, Path.GetFileName(fPath));
                                }
                            }
                            pptxDoc.Close();
                            result = true;
                            filename = Path.GetFileNameWithoutExtension(Request.Files[0].FileName) + ".zip";
                            break;
                        default:
                            validationMessage = "Please upload a valid file for conversion";
                            result = false;
                            break;
                    }

                    resultFileName = "download/result/" + filename;
                }
                else
                {
                    validationMessage = "Please upload a valid file for conversion";
                    result = false;
                }
            }
            catch (Exception ex)
            {
                return this.Json(new { data = "An eroor occured while processing. Please try again later", status = false, filename = string.Empty }, JsonRequestBehavior.AllowGet);
            }

            return this.Json(new { data = validationMessage, status = result, filename = resultFileName }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult HTMLConvertFile(HttpPostedFileBase file)
        {
            bool result = false;
            string resultFileName = string.Empty;
            string validationMessage = string.Empty;
            string filename = string.Empty;
            try
            {
                if (Request.Files != null && Request.Files.Count > 0 &&
                    !String.IsNullOrEmpty(Request.Files[0].FileName) && Request.Files[0].ContentLength > 0)
                {
                    file = Request.Files[0];
                    var ext = System.IO.Path.GetExtension(Request.Files[0].FileName);
                    byte[] fileBytes = new byte[file.ContentLength];
                    var data = file.InputStream.Read(fileBytes, 0, Convert.ToInt32(file.ContentLength));
                    var stream = new MemoryStream(fileBytes);
                    var currpath = string.Empty;

                    switch (ext)
                    {
                        case ".doc":
                        case ".docx":
                            WordDocument wordDocument = new WordDocument(stream, Syncfusion.DocIO.FormatType.Docx);
                            filename = string.Concat(Path.GetFileNameWithoutExtension(Request.Files[0].FileName), DateTime.Now.ToString("yyyyMMddHHmmssfff", CultureInfo.CurrentCulture), ".html");
                            currpath = this.GetTempPathForFile(filename);
                            wordDocument.Save(currpath, Syncfusion.DocIO.FormatType.Html);
                            result = true;
                            break;
                        default:
                            validationMessage = "Please upload a valid file for conversion";
                            result = false;
                            break;
                    }
                    resultFileName = "download/result/" + filename;
                }
                else
                {
                    validationMessage = "Please upload a valid file for conversion";
                    result = false;
                }
            }
            catch (Exception ex)
            {
                return this.Json(new { data = "An eroor occured while processing. Please try again later", status = false, filename = string.Empty }, JsonRequestBehavior.AllowGet);
            }

            return this.Json(new { data = validationMessage, status = result, filename = resultFileName }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult RTFConvertFile(HttpPostedFileBase file)
        {
            bool result = false;
            string resultFileName = string.Empty;
            string validationMessage = string.Empty;
            string filename = string.Empty;
            try
            {
                if (Request.Files != null && Request.Files.Count > 0 &&
                    !String.IsNullOrEmpty(Request.Files[0].FileName) && Request.Files[0].ContentLength > 0)
                {
                    file = Request.Files[0];
                    var ext = System.IO.Path.GetExtension(Request.Files[0].FileName);
                    byte[] fileBytes = new byte[file.ContentLength];
                    var data = file.InputStream.Read(fileBytes, 0, Convert.ToInt32(file.ContentLength));
                    var stream = new MemoryStream(fileBytes);
                    var currpath = string.Empty;
                    switch (ext)
                    {
                        case ".doc":
                        case ".docx":
                            WordDocument wordDocument = new WordDocument(stream, Syncfusion.DocIO.FormatType.Docx);
                            filename = string.Concat(Path.GetFileNameWithoutExtension(Request.Files[0].FileName), DateTime.Now.ToString("yyyyMMddHHmmssfff", CultureInfo.CurrentCulture), ".rtf");
                            currpath = this.GetTempPathForFile(filename);
                            wordDocument.Save(currpath, Syncfusion.DocIO.FormatType.Rtf);
                            result = true;
                            break;
                        default:
                            validationMessage = "Please upload a valid file for conversion";
                            result = false;
                            break;
                    }

                    resultFileName = "download/result/" + filename;
                }
                else
                {
                    validationMessage = "Please upload a valid file for conversion";
                    result = false;
                }
            }
            catch (Exception ex)
            {
                return this.Json(new { data = "An eroor occured while processing. Please try again later", status = false, filename = string.Empty }, JsonRequestBehavior.AllowGet);
            }

            return this.Json(new { data = validationMessage, status = result, filename = resultFileName }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult EPUBConvertFile(HttpPostedFileBase file)
        {
            bool result = false;
            string resultFileName = string.Empty;
            string validationMessage = string.Empty;
            string filename = string.Empty;
            try
            {
                if (Request.Files != null && Request.Files.Count > 0 &&
                    !String.IsNullOrEmpty(Request.Files[0].FileName) && Request.Files[0].ContentLength > 0)
                {
                    file = Request.Files[0];
                    var ext = System.IO.Path.GetExtension(Request.Files[0].FileName);
                    byte[] fileBytes = new byte[file.ContentLength];
                    var data = file.InputStream.Read(fileBytes, 0, Convert.ToInt32(file.ContentLength));
                    var stream = new MemoryStream(fileBytes);
                    var currpath = string.Empty;
                    switch (ext)
                    {
                        case ".doc":
                        case ".docx":
                            WordDocument wordDocument = new WordDocument(stream, Syncfusion.DocIO.FormatType.Docx);
                            wordDocument.SaveOptions.EPubExportFont = true;
                            wordDocument.SaveOptions.HtmlExportHeadersFooters = true;
                            filename = string.Concat(Path.GetFileNameWithoutExtension(Request.Files[0].FileName), DateTime.Now.ToString("yyyyMMddHHmmssfff", CultureInfo.CurrentCulture), ".epub");
                            currpath = this.GetTempPathForFile(filename);
                            wordDocument.Save(currpath, Syncfusion.DocIO.FormatType.EPub);
                            result = true;
                            break;
                        default:
                            validationMessage = "Please upload a valid file for conversion";
                            result = false;
                            break;
                    }

                    resultFileName = "download/result/" + filename;
                }
                else
                {
                    validationMessage = "Please upload a valid file for conversion";
                    result = false;
                }
            }
            catch (Exception ex)
            {
                return this.Json(new { data = "An eroor occured while processing. Please try again later", status = false, filename = string.Empty }, JsonRequestBehavior.AllowGet);
            }

            return this.Json(new { data = validationMessage, status = result, filename = resultFileName }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ODTConvertFile(HttpPostedFileBase file)
        {
            bool result = false;
            string resultFileName = string.Empty;
            string validationMessage = string.Empty;
            string filename = string.Empty;
            try
            {
                if (Request.Files != null && Request.Files.Count > 0 &&
                    !String.IsNullOrEmpty(Request.Files[0].FileName) && Request.Files[0].ContentLength > 0)
                {
                    file = Request.Files[0];
                    var ext = System.IO.Path.GetExtension(Request.Files[0].FileName);
                    byte[] fileBytes = new byte[file.ContentLength];
                    var data = file.InputStream.Read(fileBytes, 0, Convert.ToInt32(file.ContentLength));
                    var stream = new MemoryStream(fileBytes);
                    var currpath = string.Empty;
                    switch (ext)
                    {
                        case ".doc":
                        case ".docx":
                            WordDocument wordDocument = new WordDocument(stream, Syncfusion.DocIO.FormatType.Docx);
                            filename = string.Concat(Path.GetFileNameWithoutExtension(Request.Files[0].FileName), DateTime.Now.ToString("yyyyMMddHHmmssfff", CultureInfo.CurrentCulture), ".odt");
                            currpath = this.GetTempPathForFile(filename);
                            wordDocument.Save(currpath, Syncfusion.DocIO.FormatType.Odt);
                            result = true;
                            break;
                        default:
                            validationMessage = "Please upload a valid file for conversion";
                            result = false;
                            break;
                    }

                    resultFileName = "download/result/" + filename;
                }
                else
                {
                    validationMessage = "Please upload a valid file for conversion";
                    result = false;
                }
            }
            catch (Exception ex)
            {
                return this.Json(new { data = "An eroor occured while processing. Please try again later", status = false, filename = string.Empty }, JsonRequestBehavior.AllowGet);
            }

            return this.Json(new { data = validationMessage, status = result, filename = resultFileName }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult TextConvertFile(HttpPostedFileBase file)
        {
            bool result = false;
            string resultFileName = string.Empty;
            string validationMessage = string.Empty;
            string filename = string.Empty;
            try
            {
                if (Request.Files != null && Request.Files.Count > 0 &&
                    !String.IsNullOrEmpty(Request.Files[0].FileName) && Request.Files[0].ContentLength > 0)
                {
                    file = Request.Files[0];
                    var ext = System.IO.Path.GetExtension(Request.Files[0].FileName);
                    byte[] fileBytes = new byte[file.ContentLength];
                    var data = file.InputStream.Read(fileBytes, 0, Convert.ToInt32(file.ContentLength));
                    var stream = new MemoryStream(fileBytes);
                    var currpath = string.Empty;
                    switch (ext)
                    {
                        case ".doc":
                        case ".docx":
                            WordDocument wordDocument = new WordDocument(stream, Syncfusion.DocIO.FormatType.Docx);
                            filename = string.Concat(Path.GetFileNameWithoutExtension(Request.Files[0].FileName), DateTime.Now.ToString("yyyyMMddHHmmssfff", CultureInfo.CurrentCulture), ".txt");
                            currpath = this.GetTempPathForFile(filename);
                            wordDocument.Save(currpath, Syncfusion.DocIO.FormatType.Txt);
                            result = true;
                            break;
                        default:
                            validationMessage = "Please upload a valid file for conversion";
                            result = false;
                            break;
                    }

                    resultFileName = "download/result/" + filename;
                }
                else
                {
                    validationMessage = "Please upload a valid file for conversion";
                    result = false;
                }
            }
            catch (Exception ex)
            {
                return this.Json(new { data = "An eroor occured while processing. Please try again later", status = false, filename = string.Empty }, JsonRequestBehavior.AllowGet);
            }

            return this.Json(new { data = validationMessage, status = result, filename = resultFileName }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ExcelTempResult DownloadFile(string fileName)
        {
            return new ExcelTempResult
            {
                Path = GetTempPathForFile(fileName)
            };
        }

        public string GetTempPathForFile(string fileName)
        {
            try
            {
                return Path.Combine(Environment.GetEnvironmentVariable("TEMP"), fileName);
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// HTML to PDF
        /// </summary>
        public static void ConvertToPDF()
        {
            //HtmlToPdfConverter htmlConverter = new HtmlToPdfConverter(HtmlRenderingEngine.IE);
            //IEConverterSettings settings = new IEConverterSettings();
            //htmlConverter.ConverterSettings = settings;
            //PdfDocument document = htmlConverter.Convert(filePath);
            //document.Save(currpath);
            //document.Close();
        }
    }

    public class ExcelTempResult : ActionResult
    {
        /// <summary>
        ///  Gets or sets File Name
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// Gets or sets Path
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// Execute Excel Result
        /// </summary>
        /// <param name="context">Controller Context</param>
        public override void ExecuteResult(ControllerContext context)
        {
            if (context != null)
            {
                context.HttpContext.Response.Buffer = true;
                context.HttpContext.Response.Clear();
                context.HttpContext.Response.AddHeader("content-disposition", "attachment; filename=" + this.FileName);
                context.HttpContext.Response.ContentType = "application/octet-stream";
                context.HttpContext.Response.WriteFile(this.Path);
            }
        }
    }
}