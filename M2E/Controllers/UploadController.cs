﻿using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using System.IO;
using M2E.Service.UploadImages;

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
            const string rootfolder = @"~/Upload/Images/";
            const string startingDir = rootfolder; //@"c:\Temp";

            foreach (HttpPostedFileBase file in files)
            {
                string filePath = Path.Combine(startingDir, file.FileName);

                System.IO.File.WriteAllBytes(Server.MapPath(filePath), ReadData(file.InputStream));
            }

            return Json("All files have been successfully stored.");
        }

        [HttpPost]        
        public ActionResult UploadDropZoneFilesImgUr(IEnumerable<HttpPostedFileBase> files)
        {

            const string albumid = "Xlh72LgTBw6Tzs1";

            var imgurService = new imgurService();
            var uploadedImagesId = imgurService.UploadMultipleImagesToImgur(files, albumid);

            return Json(uploadedImagesId);
        }

        public ActionResult CreateImgurAlbum()
        {
            var imgurService = new imgurService();
            return Json(imgurService.CreateImgurAlbum(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetImgurAlbumDetails()
        {
            var albumId = Request.QueryString["albumId"];
            var imgurService = new imgurService();
            return Json(imgurService.GetImgurAlbumDetails(albumId), JsonRequestBehavior.AllowGet);
        }

        private byte[] ReadData(Stream stream)
        {
            var buffer = new byte[16 * 1024];

            using (var ms = new MemoryStream())
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
