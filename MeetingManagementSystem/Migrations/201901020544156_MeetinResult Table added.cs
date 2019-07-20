namespace MeetingManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MeetinResultTableadded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MeetingResults",
                c => new
                    {
                        MeetingResultId = c.Int(nullable: false, identity: true),
                        Announcement = c.String(),
                        Result = c.String(),
                        Status = c.String(),
                        EmployeeId = c.Int(nullable: false),
                        MeetingId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.MeetingResultId)
                .ForeignKey("dbo.Employees", t => t.EmployeeId, cascadeDelete: false)
                .ForeignKey("dbo.Meetings", t => t.MeetingId, cascadeDelete: false)
                .Index(t => t.EmployeeId)
                .Index(t => t.MeetingId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MeetingResults", "MeetingId", "dbo.Meetings");
            DropForeignKey("dbo.MeetingResults", "EmployeeId", "dbo.Employees");
            DropIndex("dbo.MeetingResults", new[] { "MeetingId" });
            DropIndex("dbo.MeetingResults", new[] { "EmployeeId" });
            DropTable("dbo.MeetingResults");
        }
    }
}
