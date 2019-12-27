using SyncfusionDocConverterPortal.Utils;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace SyncfusionDocConverterPortal
{
    public class MvcApplication : HttpApplication
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.MapRoute(
                name: "Conversion Document",
                url: "pdf-converter",
                defaults: new { controller = "Dashboard", action = "PdfConverter" }
             );

            routes.MapRoute(
                name: "Image Conversion",
                url: "image-converter",
                defaults: new { controller = "Dashboard", action = "ImageConverter" }
             );

            routes.MapRoute(
                name: "HTML Conversion",
                url: "html-converter",
                defaults: new { controller = "Dashboard", action = "HTMLConverter" }
             );

            routes.MapRoute(
               name: "RTF Conversion",
               url: "rtf-converter",
               defaults: new { controller = "Dashboard", action = "RTFConverter" }
            );

            routes.MapRoute(
              name: "EPUB Conversion",
              url: "epub-converter",
              defaults: new { controller = "Dashboard", action = "EPUBConverter" }
           );

            routes.MapRoute(
               name: "Text Conversion",
               url: "text-converter",
               defaults: new { controller = "Dashboard", action = "TextConverter" }
            );

            routes.MapRoute(
               name: "ODT Conversion",
               url: "odt-converter",
               defaults: new { controller = "Dashboard", action = "ODTConverter" }
            );

            routes.MapRoute(
               name: "upload Document",
               url: "file/upload/{file}",
               defaults: new { controller = "Dashboard", action = "PdfConvertFile", file = UrlParameter.Optional }
            );

            routes.MapRoute(
               name: "Image upload Document",
               url: "image/upload/{file}",
               defaults: new { controller = "Dashboard", action = "ImageConvertFile", file = UrlParameter.Optional }
            );

            routes.MapRoute(
               name: "Html upload Document",
               url: "html/upload/{file}",
               defaults: new { controller = "Dashboard", action = "HTMLConvertFile", file = UrlParameter.Optional }
            );

            routes.MapRoute(
               name: "RTF upload Document",
               url: "rtf/upload/{file}",
               defaults: new { controller = "Dashboard", action = "RTFConvertFile", file = UrlParameter.Optional }
            );

            routes.MapRoute(
               name: "EPUB upload Document",
               url: "epub/upload/{file}",
               defaults: new { controller = "Dashboard", action = "EPUBConvertFile", file = UrlParameter.Optional }
            );

            routes.MapRoute(
               name: "ODT upload Document",
               url: "odt/upload/{file}",
               defaults: new { controller = "Dashboard", action = "ODTConvertFile", file = UrlParameter.Optional }
            );

            routes.MapRoute(
               name: "Text upload Document",
               url: "text/upload/{file}",
               defaults: new { controller = "Dashboard", action = "TextConvertFile", file = UrlParameter.Optional }
            );

            routes.MapRoute(
               name: "Download Converted File",
               url: "download/result/{filename}",
               defaults: new { controller = "Dashboard", action = "DownloadFile", filename = UrlParameter.Optional });


            routes.MapRouteLowercase(
                  "Default",                                              // Route name
                  "{controller}/{action}/{id}",                           // URL with parameters
                  new { controller = "Dashboard", action = "Index", id = "" }  // Parameter defaults
              );

            
        }

        protected void Application_Start()
        {
            MvcHandler.DisableMvcResponseHeader = true;
            RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
     
    }
}