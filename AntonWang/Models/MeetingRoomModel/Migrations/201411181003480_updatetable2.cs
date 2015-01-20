namespace MeetingRoomModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatetable2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ApplyLogs", "Master", c => c.String(maxLength: 50));
            AddColumn("dbo.ApplyLogs", "BeginTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.Rooms", "Name", c => c.String(nullable: false, maxLength: 50));
            AddColumn("dbo.ApplyMeetingRooms", "Master", c => c.String(maxLength: 50));
            AddColumn("dbo.ApplyMeetingRooms", "BeginTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.RoomDevices", "Count", c => c.Int(nullable: false));
            AddColumn("dbo.Devices", "CreatedBy", c => c.String());
            DropColumn("dbo.ApplyLogs", "QuestionMaster");
            DropColumn("dbo.ApplyLogs", "Status");
            DropColumn("dbo.Rooms", "RoomNumber");
            DropColumn("dbo.ApplyMeetingRooms", "QuestionMaster");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ApplyMeetingRooms", "QuestionMaster", c => c.String(maxLength: 50));
            AddColumn("dbo.Rooms", "RoomNumber", c => c.String(nullable: false, maxLength: 50));
            AddColumn("dbo.ApplyLogs", "Status", c => c.Int(nullable: false));
            AddColumn("dbo.ApplyLogs", "QuestionMaster", c => c.String(maxLength: 50));
            DropColumn("dbo.Devices", "CreatedBy");
            DropColumn("dbo.RoomDevices", "Count");
            DropColumn("dbo.ApplyMeetingRooms", "BeginTime");
            DropColumn("dbo.ApplyMeetingRooms", "Master");
            DropColumn("dbo.Rooms", "Name");
            DropColumn("dbo.ApplyLogs", "BeginTime");
            DropColumn("dbo.ApplyLogs", "Master");
        }
    }
}
