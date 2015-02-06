using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WcfServiceWeb.Models
{
    public class UploadModel
    {
        public string Description { get; set; }
        public string FileName { get; set; }
        /// <summary>
        /// 字节长度
        /// </summary>
        public int FileByteLength { get; set; }

    }
}