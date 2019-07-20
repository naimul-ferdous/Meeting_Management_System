namespace MeetingManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DiscussionTableAdded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Discussions",
                c => new
                    {
                        DiscussionId = c.Int(nullable: false, identity: true),
                        DiscussionText = c.String(),
                        MeetingId = c.Int(nullable: false),
                        EmployeeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.DiscussionId)
                .ForeignKey("dbo.Employees", t => t.EmployeeId, cascadeDelete: false)
                .ForeignKey("dbo.Meetings", t => t.MeetingId, cascadeDelete: false)
                .Index(t => t.MeetingId)
                .Index(t => t.EmployeeId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Discussions", "MeetingId", "dbo.Meetings");
            DropForeignKey("dbo.Discussions", "EmployeeId", "dbo.Employees");
            DropIndex("dbo.Discussions", new[] { "EmployeeId" });
            DropIndex("dbo.Discussions", new[] { "MeetingId" });
            DropTable("dbo.Discussions");
        }
    }
}
