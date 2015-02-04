using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Web;
using WcfServiceWeb.Controllers;

namespace WcfServiceWeb.Services
{
    [ServiceBehavior(Name = "UploadService", Namespace = "http://HsutonWong.com", ConfigurationName = "UploadService", ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerSession)]
      
    public class UploadService : IUploadService
    {
        public string UploadPhotoInfo(Stream stream)
        {

            return "OK";
            
        }
    }
}