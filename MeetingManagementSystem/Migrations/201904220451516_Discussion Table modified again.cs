namespace MeetingManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DiscussionTablemodifiedagain : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Discussions", "CreatedBy_EmployeeId", "dbo.Employees");
            DropForeignKey("dbo.Discussions", "ModifiedBy_EmployeeId", "dbo.Employees");
            DropIndex("dbo.Discussions", new[] { "CreatedBy_EmployeeId" });
            DropIndex("dbo.Discussions", new[] { "ModifiedBy_EmployeeId" });
            DropColumn("dbo.Discussions", "CreatedBy_EmployeeId");
            DropColumn("dbo.Discussions", "ModifiedBy_EmployeeId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Discussions", "ModifiedBy_EmployeeId", c => c.Int());
            AddColumn("dbo.Discussions", "CreatedBy_EmployeeId", c => c.Int());
            CreateIndex("dbo.Discussions", "ModifiedBy_EmployeeId");
            CreateIndex("dbo.Discussions", "CreatedBy_EmployeeId");
            AddForeignKey("dbo.Discussions", "ModifiedBy_EmployeeId", "dbo.Employees", "EmployeeId");
            AddForeignKey("dbo.Discussions", "CreatedBy_EmployeeId", "dbo.Employees", "EmployeeId");
        }
    }
}
