using ClientWeb.UploadFileService;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Tools;

namespace ClientWeb.Controllers
{
    public class MessageHeaderController:Controller
    {
        public ActionResult Index()
        {
            StreamByteHelper byteHelper = new StreamByteHelper();
            var uploadService = new UploadServiceClient();
           
           
            byte[] fileABytes = byteHelper.FilteToBytes(Server.MapPath(@"~/Content/Img/A.jpg"));
            byte[] fileBBytes = byteHelper.FilteToBytes(Server.MapPath(@"~/Content/Img/B.png"));
            int fileAALength = fileABytes.Length;
            int fileBBLength = fileBBytes.Length;
            var fileArray = new byte[fileAALength + fileBBLength];
            Array.Copy(fileABytes, 0, fileArray, 0, fileAALength);
            Array.Copy(fileBBytes, 0, fileArray, fileAALength, fileBBLength);
            var compressBytes = Compress.Zip(fileArray);
            var stream = new MemoryStream(compressBytes);
            string result = string.Empty;
             string strJson = "["+
                                    "{Description:'画图.png',FileName:'aaa1.jpg',FileByteLength:" + fileAALength + "}," +
                                    "{Description:'颜色.png',FileName:'aaa2.png',FileByteLength:" + fileBBLength + "}" +
                               "]";
            using (OperationContextScope scope = new OperationContextScope(uploadService.InnerChannel))
            {

                MessageHeader header = MessageHeader.CreateHeader("JsonContent", "http://Hsuton.com", strJson);
                OperationContext.Current.OutgoingMessageHeaders.Add(header);
                result = uploadService.MessageHeader(stream);
            }
            return Json(result, JsonRequestBehavior.AllowGet); 
        }
    }
}