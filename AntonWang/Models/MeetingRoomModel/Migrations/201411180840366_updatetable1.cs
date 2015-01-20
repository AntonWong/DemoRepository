namespace MeetingRoomModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatetable1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ApplyLogs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Applicant = c.String(),
                        QuestionMaster = c.String(),
                        Status = c.Int(nullable: false),
                        RoomId = c.Int(nullable: false),
                        Subject = c.String(nullable: false),
                        Description = c.String(),
                        EndTime = c.DateTime(nullable: false),
                        Priority = c.Int(nullable: false),
                        CreateTime = c.DateTimeOffset(nullable: false, precision: 7),
                        UpdateTime = c.DateTimeOffset(precision: 7),
                        Deleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Rooms", t => t.RoomId)
                .Index(t => t.RoomId);
            
            CreateTable(
                "dbo.Rooms",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RoomNumber = c.String(nullable: false, maxLength: 50),
                        AreaSize = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Position = c.String(),
                        CreateTime = c.DateTimeOffset(nullable: false, precision: 7),
                        UpdateTime = c.DateTimeOffset(precision: 7),
                        Deleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ApplyMeetingRooms",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Applicant = c.String(),
                        QuestionMaster = c.String(),
                        Status = c.Int(nullable: false),
                        RoomId = c.Int(nullable: false),
                        Subject = c.String(nullable: false),
                        Description = c.String(),
                        EndTime = c.DateTime(nullable: false),
                        Priority = c.Int(nullable: false),
                        CreateTime = c.DateTimeOffset(nullable: false, precision: 7),
                        UpdateTime = c.DateTimeOffset(precision: 7),
                        Deleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Rooms", t => t.RoomId)
                .Index(t => t.RoomId);
            
            CreateTable(
                "dbo.RoomDevices",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RoomId = c.Int(nullable: false),
                        DeviceId = c.Int(nullable: false),
                        CreateTime = c.DateTimeOffset(nullable: false, precision: 7),
                        UpdateTime = c.DateTimeOffset(precision: 7),
                        Deleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Devices", t => t.DeviceId)
                .ForeignKey("dbo.Rooms", t => t.RoomId)
                .Index(t => t.RoomId)
                .Index(t => t.DeviceId);
            
            CreateTable(
                "dbo.Devices",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        CreateTime = c.DateTimeOffset(nullable: false, precision: 7),
                        UpdateTime = c.DateTimeOffset(precision: 7),
                        Deleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ApplyLogs", "RoomId", "dbo.Rooms");
            DropForeignKey("dbo.RoomDevices", "RoomId", "dbo.Rooms");
            DropForeignKey("dbo.RoomDevices", "DeviceId", "dbo.Devices");
            DropForeignKey("dbo.ApplyMeetingRooms", "RoomId", "dbo.Rooms");
            DropIndex("dbo.RoomDevices", new[] { "DeviceId" });
            DropIndex("dbo.RoomDevices", new[] { "RoomId" });
            DropIndex("dbo.ApplyMeetingRooms", new[] { "RoomId" });
            DropIndex("dbo.ApplyLogs", new[] { "RoomId" });
            DropTable("dbo.Devices");
            DropTable("dbo.RoomDevices");
            DropTable("dbo.ApplyMeetingRooms");
            DropTable("dbo.Rooms");
            DropTable("dbo.ApplyLogs");
        }
    }
}
