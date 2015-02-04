using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Web;

namespace WcfServiceWeb.Controllers
{
    [ServiceContract(ConfigurationName = "IUploadContract", Name = "IUploadService", Namespace = "http://HsutonWong.com")]
    public interface IUploadService
    {
    }
}