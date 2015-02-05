﻿using System.IO;
using System.ServiceModel;

namespace WcfServiceWeb.Contracts
{
    [ServiceContract(ConfigurationName = "IUploadContract", Name = "IUploadService", Namespace = "http://HsutonWong.com")]
    public interface IUploadService
    {
        [OperationContract(Name = "UploadPhotoInfo")]
        string UploadPhotoInfo(Stream stream);

        [OperationContract(Name = "Decompress")]
        string Decompress(Stream stream);

    }

   

}