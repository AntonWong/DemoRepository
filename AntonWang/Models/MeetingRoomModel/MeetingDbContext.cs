//======================================================================
//
//        Copyright (C) 2014-2016 公共服务组  
//        All rights reserved
//
//        filename :BlogDbContext
//        description :上下文
//
//        created by 王旭东 at  2014年11月17日 09:49:48
//
//======================================================================
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using MeetingRoomModel.Migrations;
using MeetingRoomModel.Models;

namespace MeetingRoomModel
{
    public class MeetingDbContext : DbContext
    {
        static MeetingDbContext()
        {
            //初始化时自动更新迁移到最新版本
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<MeetingDbContext, Configuration>());
        }
        /// <summary>
        /// 初始化DbContext
        /// </summary>
        public MeetingDbContext()
            : base("Name=meetingroom")
        {
            
        }

        public DbSet<Room> Room { get; set; }
        public DbSet<Device> Device { get; set; }
        public DbSet<ApplyMeetingRoom> ApplyMeetingRoom { get; set; }
        public DbSet<RoomDevice> RoomDevice { get; set; }
        public DbSet<ApplyLog> ApplyLog { get; set; }

        public static MeetingDbContext Create()
        {
            return new MeetingDbContext();
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ApplyMeetingRoom>()
                .HasRequired(s => s.Room)
                .WithMany(s => s.ApplyMeetingRooms)
                .HasForeignKey(s => s.RoomId);

            modelBuilder.Entity<RoomDevice>()
                .HasRequired(s => s.Room)
                .WithMany(s => s.RoomDevices)
                .HasForeignKey(s => s.RoomId);

            modelBuilder.Entity<RoomDevice>()
              .HasRequired(s => s.Device)
              .WithMany(s => s.RoomDevices)
              .HasForeignKey(s => s.DeviceId);

            modelBuilder.Entity<ApplyLog>()
              .HasRequired(s => s.Room)
              .WithMany(s => s.ApplyLogs)
              .HasForeignKey(s => s.RoomId);
          

        }
    }
}
