using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ClientWeb.UploadFileService;

namespace ClientWeb.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        public ActionResult Index()
        {
            UploadFileService.UploadServiceClient uploadService = new UploadServiceClient();
            string result = uploadService.UploadPhotoInfo(new MemoryStream());
            return Json(result, JsonRequestBehavior.AllowGet);
        }
	}
}