using System;
using System.IO;
using System.IO.Compression;
using System.Text;

namespace Tools
{
    public class ZipHelper
    {
        //public static Stream Compress(byte[] bytes)
        //{
        //    MemoryStream ms = new MemoryStream();
        //    using (GZipStream zip = new GZipStream(ms, CompressionMode.Compress, true))
        //    {
        //        zip.Write(bytes, 0, bytes.Length);
        //    }
        //    ms.Position = 0;
        //    byte[] compressed = new byte[ms.Length];
        //    ms.Read(compressed, 0, compressed.Length);

        //    byte[] gzBuffer = new byte[compressed.Length + 4];
        //    Buffer.BlockCopy(compressed, 0, gzBuffer, 4, compressed.Length);
        //    Buffer.BlockCopy(BitConverter.GetBytes(bytes.Length), 0, gzBuffer, 0, 4);
        //    return new MemoryStream(gzBuffer); 
        //}

        public static byte[] Compress(string text)
        {
            byte[] buffer = Encoding.UTF8.GetBytes(text);
            MemoryStream ms = new MemoryStream();
            using (GZipStream zip = new GZipStream(ms, CompressionMode.Compress, true))
            {
                zip.Write(buffer, 0, buffer.Length);
            }
            ms.Position = 0;
            byte[] compressed = new byte[ms.Length];
            ms.Read(compressed, 0, compressed.Length);
            return compressed;
            /*
            byte[] gzBuffer = new byte[compressed.Length + 4];

            Buffer.BlockCopy(compressed, 0, gzBuffer, 4, compressed.Length);
            Buffer.BlockCopy(BitConverter.GetBytes(buffer.Length), 0, gzBuffer, 0, 4);
            return gzBuffer;
           */
        }

        public static byte[] Decompress(byte[] gzBuffer)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                int msgLength = BitConverter.ToInt32(gzBuffer, 0);
                ms.Write(gzBuffer, 0, gzBuffer.Length );

                byte[] buffer = new byte[msgLength];

                ms.Position = 0;
                using (GZipStream zip = new GZipStream(ms, CompressionMode.Decompress))
                {
                    zip.Read(buffer, 0, buffer.Length);
                }
                return buffer;
            }
        }
    }
}