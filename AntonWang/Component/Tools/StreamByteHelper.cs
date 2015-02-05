using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tools
{
    public class StreamByteHelper
    {
        /* - - - - - - - - - - - - - - - - - - - - - - - - 
* Stream 和 byte[] 之间的转换 
* - - - - - - - - - - - - - - - - - - - - - - - */

        /// <summary> 
        /// 将 Stream 转成 byte[] 
        /// </summary> 
        public byte[] StreamToBytes(Stream stream)
        {
            long s = stream.Length;
            byte[] bytes = new byte[stream.Length];
            stream.Read(bytes, 0, bytes.Length);
            // 设置当前流的位置为流的开始 
            stream.Seek(0, SeekOrigin.Begin);
            return bytes;
        }
        /// <summary>
        /// 从数据流中获得二进制数组
        /// </summary>
        /// <param name="stream">数据流</param>
        /// <returns></returns>
        public  byte[] GetByteArrayFromStream(Stream stream)
        {
            if (stream == null)
                return null;
            int bI;
            try
            {
                using (MemoryStream memory = new MemoryStream())
                {
                    while ((bI = stream.ReadByte()) != -1)
                    {
                        memory.WriteByte(((byte)bI));
                    }
                    byte[] streamData = memory.ToArray();

                    return streamData;
                }
                //也可用如下方式
                //MemoryStream memory=new MemoryStream();
                //stream.CopyTo(memory);
                //return memory.ToArray();
            }
            catch (Exception)
            {

                return null;
            }


        }
        public byte[] FilteToBytes(string filePath)
        {
            byte[] arrFile; //先定义一个byte数组
            //将指定的文件数据读取到 数组中
            using (FileStream fs = new FileStream(filePath, FileMode.Open)) //path是文件的路径
            {
                arrFile = new byte[fs.Length];//定义这个byte[]数组的长度 为文件的length
                fs.Read(arrFile, 0, arrFile.Length);//把fs文件读入到arrFile数组中，0是指偏移量，从0开始读，arrFile.length是指需要读的长度，也就是整个文件的长度
            }
            return arrFile;
        }

        /// <summary> 
        /// 将 byte[] 转成 Stream 
        /// </summary> 
        public Stream BytesToStream(byte[] bytes)
        {
            Stream stream = new MemoryStream(bytes);
            return stream;
        }

        /* - - - - - - - - - - - - - - - - - - - - - - - - 
        * Stream 和 文件之间的转换 
        * - - - - - - - - - - - - - - - - - - - - - - - */

        /// <summary> 
        /// 将 Stream 写入文件 
        /// </summary> 
        public void StreamToFile(Stream stream, string fileName)
        {
            // 把 Stream 转换成 byte[] 
            byte[] bytes = new byte[stream.Length];
            stream.Read(bytes, 0, bytes.Length);
            // 设置当前流的位置为流的开始 
            stream.Seek(0, SeekOrigin.Begin);
            // 把 byte[] 写入文件 
            FileStream fs = new FileStream(fileName, FileMode.Create);
            BinaryWriter bw = new BinaryWriter(fs);
            bw.Write(bytes);
            bw.Close();
            fs.Close();
        }
        public void ByteToFile(byte[] bytes, string fileName)
        {
            MemoryStream ms = new MemoryStream(bytes);
            FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
            fs.Write(bytes, 0, bytes.Length);
             fs.Close();
             ms.Close();
             fs = null;


            //// 把 Stream 转换成 byte[] 
            //byte[] bytes = new byte[stream.Length];
            //stream.Read(bytes, 0, bytes.Length);
            //// 设置当前流的位置为流的开始 
            //stream.Seek(0, SeekOrigin.Begin);
            //// 把 byte[] 写入文件 
            //FileStream fs = new FileStream(fileName, FileMode.Create);
            //BinaryWriter bw = new BinaryWriter(fs);
            //bw.Write(bytes);
            //bw.Close();
            //fs.Close();
        }

        /// <summary> 
        /// 从文件读取 Stream 
        /// </summary> 
        public Stream FileToStream(string fileName)
        {
            // 打开文件 
            FileStream fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read);
            // 读取文件的 byte[] 
            byte[] bytes = new byte[fileStream.Length];
            fileStream.Read(bytes, 0, bytes.Length);
            fileStream.Close();
            // 把 byte[] 转换成 Stream 
            Stream stream = new MemoryStream(bytes);
            return stream;
        }

        public Stream StrToStream(string strInput)
        {
            byte[] array = Encoding.Default.GetBytes(strInput);
            return new MemoryStream(array); //convert stream 2 string      
        }
    }
}
