namespace MeetingManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MeetingMemberTableModified : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MeetingMembers", "BeginningTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.MeetingMembers", "EndTime", c => c.DateTime(nullable: false));
            CreateIndex("dbo.MeetingMembers", "MeetingId");
            CreateIndex("dbo.MeetingMembers", "EmployeeId");
            AddForeignKey("dbo.MeetingMembers", "EmployeeId", "dbo.Employees", "EmployeeId", cascadeDelete: false);
            AddForeignKey("dbo.MeetingMembers", "MeetingId", "dbo.Meetings", "MeetingId", cascadeDelete: false);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MeetingMembers", "MeetingId", "dbo.Meetings");
            DropForeignKey("dbo.MeetingMembers", "EmployeeId", "dbo.Employees");
            DropIndex("dbo.MeetingMembers", new[] { "EmployeeId" });
            DropIndex("dbo.MeetingMembers", new[] { "MeetingId" });
            DropColumn("dbo.MeetingMembers", "EndTime");
            DropColumn("dbo.MeetingMembers", "BeginningTime");
        }
    }
}
