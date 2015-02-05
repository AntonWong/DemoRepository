using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Web;

namespace WcfServiceWeb.Contracts
{
    [ServiceContract(ConfigurationName = "IUploadContract", Name = "IUploadService", Namespace = "http://HsutonWong.com")]
    public interface IUploadService
    {
        [OperationContract(Name = "UploadPhotoInfo")]
       string UploadPhotoInfo(Stream stream);
        [OperationContract(Name = "Decompress")]
        string Decompress(Stream stream);
        [OperationContract(Name = "MessageHeader")]
        string MessageHeader(Stream stream);

    }
}