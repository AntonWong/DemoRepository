using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Migrations;
using System.Linq;
using MeetingRoomModel.Models;
using MeetingRoomModel.Models.Enum;

namespace MeetingRoomModel.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<MeetingDbContext>
    {
        public Configuration()
        {
            //�ر��Զ�����Ǩ�ƣ��ó���ֻ�������Լ����ɵ�Ǩ�ƣ�
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(MeetingDbContext context)
        {
            var newroom = new Room
            {
                Name = "��һ������",
                CreateTime = DateTimeOffset.Now,
                Deleted = false,
            };
            context.Room.AddOrUpdate(l => l.Name, newroom);
            context.SaveChanges();

            var newApplyMeetingRoom = new ApplyMeetingRoom
            {
                Applicant ="����",
                Master = "������",
                Subject = "�������������",
                Description="�������������",
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
                new Device { Name = "ͶӰ��", CreateTime = DateTime.Now, Deleted = false },
               // new Device { Name = "д�ְ�", CreateTime = DateTime.Now, Deleted = false },
                
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
