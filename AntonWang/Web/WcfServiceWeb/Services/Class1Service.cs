using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Web;

namespace WcfServiceWeb.Services
{
    public class Class1Service
    {
        public string StreamOperation(Stream stream)
        {
            System.Text.Encoding encoding = System.Text.Encoding.GetEncoding("utf-8");

            string name = "";
            int index = OperationContext.Current.IncomingMessageHeaders.FindHeader("filename", "http://tempuri.org");

            if (index >= 0)
            {
                name = OperationContext.Current.IncomingMessageHeaders.GetHeader<string>(index).ToString();
            }
            byte[] buffer = RetrieveBytesFromStream(stream);
            string text = System.Text.Encoding.UTF8.GetString(buffer);
            Console.WriteLine("哈哈哈,我已经收到文件名称:" + name + ",并且字节流中的数据为:" + text);
            return name;
        }


        public static byte[] RetrieveBytesFromStream(Stream stream)
        {
            List<byte> lst = new List<byte>();
            byte[] data = new byte[1024];
            int totalCount = 0;
            while (true)
            {
                int bytesRead = stream.Read(data, 0, data.Length);
                if (bytesRead == 0)
                {
                    break;
                }
                byte[] buffers = new byte[bytesRead];
                Array.Copy(data, buffers, bytesRead);
                lst.AddRange(buffers);
                totalCount += bytesRead;
            }
            return lst.ToArray();
        }


    }
}