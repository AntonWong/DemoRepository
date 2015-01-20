namespace MeetingRoomModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatetable11 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ApplyLogs", "Member", c => c.String(maxLength: 200));
            AddColumn("dbo.ApplyMeetingRooms", "Member", c => c.String(maxLength: 200));
            AlterColumn("dbo.ApplyLogs", "Applicant", c => c.String(maxLength: 50));
            AlterColumn("dbo.ApplyLogs", "QuestionMaster", c => c.String(maxLength: 50));
            AlterColumn("dbo.ApplyLogs", "Subject", c => c.String(nullable: false, maxLength: 200));
            AlterColumn("dbo.ApplyLogs", "Description", c => c.String(maxLength: 500));
            AlterColumn("dbo.Rooms", "Position", c => c.String(maxLength: 100));
            AlterColumn("dbo.ApplyMeetingRooms", "Applicant", c => c.String(maxLength: 50));
            AlterColumn("dbo.ApplyMeetingRooms", "QuestionMaster", c => c.String(maxLength: 50));
            AlterColumn("dbo.ApplyMeetingRooms", "Subject", c => c.String(nullable: false, maxLength: 200));
            AlterColumn("dbo.ApplyMeetingRooms", "Description", c => c.String(maxLength: 500));
            AlterColumn("dbo.Devices", "Name", c => c.String(nullable: false, maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Devices", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.ApplyMeetingRooms", "Description", c => c.String());
            AlterColumn("dbo.ApplyMeetingRooms", "Subject", c => c.String(nullable: false));
            AlterColumn("dbo.ApplyMeetingRooms", "QuestionMaster", c => c.String());
            AlterColumn("dbo.ApplyMeetingRooms", "Applicant", c => c.String());
            AlterColumn("dbo.Rooms", "Position", c => c.String());
            AlterColumn("dbo.ApplyLogs", "Description", c => c.String());
            AlterColumn("dbo.ApplyLogs", "Subject", c => c.String(nullable: false));
            AlterColumn("dbo.ApplyLogs", "QuestionMaster", c => c.String());
            AlterColumn("dbo.ApplyLogs", "Applicant", c => c.String());
            DropColumn("dbo.ApplyMeetingRooms", "Member");
            DropColumn("dbo.ApplyLogs", "Member");
        }
    }
}
