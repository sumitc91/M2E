using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;

namespace M2E.Controllers
{
    public class UploadController : Controller
    {
        //
        // GET: /Upload/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult UploadMultipleImages(IEnumerable<HttpPostedFileBase> files)
        {



            String rootfolder = @"~/Upload/Images/";
            string startingDir = rootfolder;//@"c:\Temp";

            foreach (HttpPostedFileBase file in files)
            {
                string filePath = Path.Combine(startingDir, file.FileName);
                long totalbytes = 0;

                System.IO.File.WriteAllBytes(Server.MapPath(filePath), ReadData(file.InputStream));
            }

            return Json("All files have been successfully stored.");
        }
        private byte[] ReadData(Stream stream)
        {
            byte[] buffer = new byte[16 * 1024];

            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = stream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }

                return ms.ToArray();
            }
        }

    }
}
