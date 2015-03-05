using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CSharp.Demo;
using MyConsole.Models;
using Tools;

namespace MyConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            MapMain();
            return;
         }

        public static void GetDelegateResut()
        {
            DelegateDemo.GetAction();
        }

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

    }

   

 

}
