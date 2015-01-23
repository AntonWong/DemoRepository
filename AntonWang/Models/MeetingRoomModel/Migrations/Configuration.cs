using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Migrations;
using System.Linq;
using MeetingRoomModel.Models;
using MeetingRoomModel.Models.Enum;

namespace MeetingRoomModel.Migrations
{
    /*
     * 迁移命令：add-migration addmodels , update-database
     */
    internal sealed class Configuration : DbMigrationsConfiguration<MeetingDbContext>
    {
        public Configuration()
        {
            //关闭自动生成迁移（让程序只打我们自己生成的迁移）
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(MeetingDbContext context)
        {
            var newroom = new Room
            {
                Name = "第一会议室",
                CreateTime = DateTimeOffset.Now,
                Deleted = false,
            };
            context.Room.AddOrUpdate(l => l.Name, newroom);
            context.SaveChanges();

            var newApplyMeetingRoom = new ApplyMeetingRoom
            {
                Applicant ="五岳",
                Master = "主持人",
                Subject = "公共服务组会议",
                Description="公共服务组会议",
                Status = (int)RoomStatusEnum.Useing,
                CreateTime=DateTimeOffset.Now,
                Deleted = false,
                BeginTime=DateTime.Now,
                EndTime = DateTime.Now.AddHours(1),
                RoomId = newroom.Id,
                Priority = (int)PriorityEnum.Light,
            };
            context.ApplyMeetingRoom.AddOrUpdate(l => l.Applicant, newApplyMeetingRoom);
            context.SaveChanges();

            var newDevice = new List<Device>
            {
                new Device { Name = "投影仪", CreateTime = DateTime.Now, Deleted = false },
               // new Device { Name = "写字板", CreateTime = DateTime.Now, Deleted = false },
                
            };
            context.Device.AddOrUpdate(l => l.Name, newDevice.ToArray());
            context.SaveChanges();

            var newRoomDevice = new List<RoomDevice>
            {
                new RoomDevice{ RoomId = newroom.Id, DeviceId = newDevice[0].Id,CreateTime=DateTime.Now,Deleted=false},
             //  new RoomDevice{ RoomId = newroom.Id, DeviceId = newDevice[1].Id,CreateTime=DateTime.Now,Deleted=false}
            };
            context.RoomDevice.AddOrUpdate(l =>l.RoomId,newRoomDevice.ToArray());
            context.SaveChanges();
        }
    }
}
