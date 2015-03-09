using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Be.MikeBevers.Logging;
using CSharp.Demo;
using MyConsole.Models;
using Tools;

namespace MyConsole
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            LogInfo();
        }

        public static void GetDelegateResut()
        {
            DelegateDemo.GetAction();
        }

        #region log4net

        public static void LogInfo()
        {
            ILogger loger = new Logger();
            loger.WriteLog(typeof (Program), "操作日志");
        }

        #endregion

        #region AutoMapper

        /// <summary>
        /// automapper
        /// </summary>
        public static void MapMain()
        {
            //两个类型之间的映射
            // var map = Mapper.CreateMap<AddressDto, Address>();
            /*
            AddressDto dto = new AddressDto
            {
                CountryName = "China",
                City = "ShangHai",
                Street = "JinZhong Street"
            };
          Mapper.CreateMap<AddressDto, Address>()
                .ForMember(d => d.Country, opt => opt.MapFrom(s => s.CountryName))
                .ForMember(d => d.Country, opt => opt.MapFrom(s => s.CountryName));
            Address address = Mapper.Map<AddressDto, Address>(dto);
            */
            /*
            Address model = new Address
            {
                Id = 212,
                City = "ShangHai",
                Country="China",
                Street = "JinZhong Street"
            };
            Mapper.CreateMap<Address, AddressDto>()
                .ForMember(d => d.CountryName, opt => opt.MapFrom(s => s.Country));
            var address = Mapper.Map<Address, AddressDto>(model);
            */
            //列表类型之间的映射
            /*
            List<Address> addressList = new List<Address>
            {
                new Address
                {
                    Id = 212,
                    City = "ShangHai",
                    Country = "China",
                    Street = "JinZhong Street"
                }
            };
            Mapper.CreateMap<Address, AddressDto>()
               .ForMember(d => d.CountryName, opt => opt.MapFrom(s => s.Country));
            var addressDtoList = AutoMapper.Mapper.Map<List<Address>, List<AddressDto>>(addressList);
            */

        }

        #endregion

        #region 压缩测试

        /// <summary>
        /// 压缩
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnCompress_Click(object sender, EventArgs e)
        {
            String path = ("Files/");
            // 检查文件是否存在
            if (File.Exists(path + "第2章 使用数据库.pdf"))
            {
                FileInfo fi1 = new FileInfo(path + "第2章 使用数据库.pdf");
                FileInfo fi2 = new FileInfo(path + "第3章 使用数据绑定和DataSet.pdf");
                FileInfo fi3 = new FileInfo(path + "第4章 SQL Server XML的功能.pdf");
                FileInfo fi4 = new FileInfo(path + "第5章 XML编程.pdf");
                List<FileInfo> fileList = new List<FileInfo>();
                fileList.Add(fi1);
                fileList.Add(fi2);
                fileList.Add(fi3);
                fileList.Add(fi4);
                //  调用方法
                string targetZipFilePath = ("~/ZipFile/") + "Book.zip"; // 扩展名可随意
                FileCompression.Compress(fileList, targetZipFilePath, 5, 5);
                Console.WriteLine("文件压缩成功，请在ZipFile文件夹查看。");
            }
            else
            {
                Console.WriteLine("被压缩文件不存在，请先解压文件。");
            }

        }

        /// <summary>
        /// 解压缩
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDecompress_Click(object sender, EventArgs e)
        {
            string zipFilePath = ("~/ZipFile/") + "Book.zip"; //
            if (File.Exists(zipFilePath))
            {
                string targetPath = ("~/Files/");
                FileCompression.Decompress(zipFilePath, targetPath);
                ////  解压后要删除zip文件
                //File.Delete(zipFilePath);
                Console.Write("文件解压成功，请在Files文件夹查看。");
            }
            else
            {
                Console.Write("压缩文件不存在，请先压缩文件。");
            }
        }

        #endregion
    }
}
