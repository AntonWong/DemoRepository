using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using ClientWeb.UploadFileService;
using Tools;

namespace ClientWeb.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        public ActionResult Index()
        {
            StreamByteHelper byteHelper = new StreamByteHelper();
            string strOut = "{FileName:'图片.png',Category:'彩图分类'}";
            UploadFileService.UploadServiceClient uploadService = new UploadServiceClient();
            byte[] fileBytes = byteHelper.FilteToBytes(Server.MapPath(@"~/Content/Img/230651118403837.png"));
            byte[] stringBytes = Encoding.UTF8.GetBytes(strOut);
            int strLength = stringBytes.Length; //35
            int fileLegth = fileBytes.Length;   //42426
            var strArray = new byte[strLength + fileLegth];
            //复制两个二进制流 到 strArray中
            Array.Copy(stringBytes, 0, strArray, 0, strLength);
            Array.Copy(fileBytes,   0, strArray, strLength, fileLegth);

            var byteArray = new byte[47];
            Array.Copy(strArray, 0, byteArray, 0, 47);
            string strJson = Encoding.UTF8.GetString(byteArray);

            var fileArray = new byte[42426];
            Array.Copy(strArray, 47, fileArray, 0, 42426);

            /*
                //System.arraycopy(lengthb, 0, bytes, 0, 1);   
                System.arraycopy(jsonb, 0, bytes, 1, jsonb.length);   
                System.arraycopy(image, 0, bytes, 1+jsonb.length, image.length);  
             */

         //   byteHelper.StreamToFile(fileArray, Server.MapPath(@"~/Content/Img/123.png"));
            string result = uploadService.UploadPhotoInfo(byteHelper.BytesToStream(strArray));
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        
	}
}