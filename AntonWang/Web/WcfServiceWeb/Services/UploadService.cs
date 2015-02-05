using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Web;
using Newtonsoft.Json.Linq;
using Tools;
using WcfServiceWeb.Contracts;
using WcfServiceWeb.Models;

namespace WcfServiceWeb.Services
{
    [ServiceBehavior(Name = "UploadService", Namespace = "http://HsutonWong.com", ConfigurationName = "UploadService", ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerSession)]
      
    public class UploadService : IUploadService
    {
        public string UploadPhotoInfo(Stream stream)
        {
            StreamByteHelper byteHelper = new StreamByteHelper();
            var streamByteHelper = new StreamByteHelper();
            byte[] bytes = streamByteHelper.GetByteArrayFromStream(stream);
            int totailLength = bytes.Length;
            //47 42426
            var strArray = new byte[47];
            var fileArray = new byte[42426];
            //从bytes中取出 json二进制和图片二进制
            Array.Copy(bytes, 0, strArray, 0, 47);
            Array.Copy(bytes, 47, fileArray, 0, 42426);
             string strJson = Encoding.UTF8.GetString(strArray);
            byteHelper.StreamToFile(fileArray,HttpContext.Current.Server.MapPath((@"~/Content/Img/123.png")));
            //var jsonModel = JObject.Parse(strContent);
            //int ss = jsonModel.Count;
            //string strFileByte = jsonModel.GetValue("StrFileByte").ToString();
            //var fileByte =  Encoding.UTF8.GetBytes(strFileByte);
           // var upoadModel = Newtonsoft.Json.JsonConvert.DeserializeObject<UploadModel>(strContent);
            return strJson + " " + HttpContext.Current.Server.MapPath((@"~/Content/Img/123.png"));

        }
    }
}