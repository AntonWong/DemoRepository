using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using ClientWeb.UploadFileService;
using Tools;

namespace ClientWeb.Controllers
{
    public class CompressController:Controller
    {
        public ActionResult Index()
        {
            StreamByteHelper streamByteHelper = new StreamByteHelper();

           string str2 = System.IO.File.ReadAllText(Server.MapPath(@"~/content/aaaa.txt"));
            var bytesTemp = Encoding.UTF8.GetBytes(str2);
            var compressBytes = Compress.Zip(bytesTemp);
           int a = bytesTemp.Length;
           int b = compressBytes.Length;
           var stream = streamByteHelper.BytesToStream(compressBytes);
           var uploadService = new UploadServiceClient();
           string result = uploadService.Decompress(stream);
           return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}