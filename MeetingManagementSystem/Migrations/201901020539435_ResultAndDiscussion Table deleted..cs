namespace MeetingManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ResultAndDiscussionTabledeleted : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ResultAndDiscussions", "EmployeeId", "dbo.Employees");
            DropIndex("dbo.ResultAndDiscussions", new[] { "EmployeeId" });
            DropTable("dbo.ResultAndDiscussions");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ResultAndDiscussions",
                c => new
                    {
                        ResultId = c.Int(nullable: false, identity: true),
                        Announcement = c.String(),
                        Result = c.String(),
                        Status = c.String(),
                        EmployeeId = c.Int(nullable: false),
                        MeetingId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ResultId);
            
            CreateIndex("dbo.ResultAndDiscussions", "EmployeeId");
            AddForeignKey("dbo.ResultAndDiscussions", "EmployeeId", "dbo.Employees", "EmployeeId", cascadeDelete: true);
        }
    }
}
