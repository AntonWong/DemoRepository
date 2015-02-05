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

namespace ClientWeb.Controllers
{
    public class MessageHeaderController:Controller
    {
        public ActionResult Index()
        {
            var uploadService = new UploadServiceClient();
            string strJson ="{Description:'描述信息',Operation:'修改'}";
            var bytes = Encoding.UTF8.GetBytes("ABCDFEG");
            var stream = new MemoryStream(bytes);
            string result = string.Empty;
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