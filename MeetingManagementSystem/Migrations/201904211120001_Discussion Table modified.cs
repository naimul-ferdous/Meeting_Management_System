namespace MeetingManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DiscussionTablemodified : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Discussions", "EmployeeId", "dbo.Employees");
            DropIndex("dbo.Discussions", new[] { "EmployeeId" });
            RenameColumn(table: "dbo.Discussions", name: "EmployeeId", newName: "CreatedBy_EmployeeId");
            AddColumn("dbo.Discussions", "CreatedById", c => c.Int(nullable: false));
            AddColumn("dbo.Discussions", "CreatedDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Discussions", "ModifiedById", c => c.Int());
            AddColumn("dbo.Discussions", "ModifiedDate", c => c.DateTime());
            AddColumn("dbo.Discussions", "ModifiedBy_EmployeeId", c => c.Int());
            AlterColumn("dbo.Discussions", "CreatedBy_EmployeeId", c => c.Int());
            CreateIndex("dbo.Discussions", "CreatedBy_EmployeeId");
            CreateIndex("dbo.Discussions", "ModifiedBy_EmployeeId");
            AddForeignKey("dbo.Discussions", "ModifiedBy_EmployeeId", "dbo.Employees", "EmployeeId");
            AddForeignKey("dbo.Discussions", "CreatedBy_EmployeeId", "dbo.Employees", "EmployeeId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Discussions", "CreatedBy_EmployeeId", "dbo.Employees");
            DropForeignKey("dbo.Discussions", "ModifiedBy_EmployeeId", "dbo.Employees");
            DropIndex("dbo.Discussions", new[] { "ModifiedBy_EmployeeId" });
            DropIndex("dbo.Discussions", new[] { "CreatedBy_EmployeeId" });
            AlterColumn("dbo.Discussions", "CreatedBy_EmployeeId", c => c.Int(nullable: false));
            DropColumn("dbo.Discussions", "ModifiedBy_EmployeeId");
            DropColumn("dbo.Discussions", "ModifiedDate");
            DropColumn("dbo.Discussions", "ModifiedById");
            DropColumn("dbo.Discussions", "CreatedDate");
            DropColumn("dbo.Discussions", "CreatedById");
            RenameColumn(table: "dbo.Discussions", name: "CreatedBy_EmployeeId", newName: "EmployeeId");
            CreateIndex("dbo.Discussions", "EmployeeId");
            AddForeignKey("dbo.Discussions", "EmployeeId", "dbo.Employees", "EmployeeId", cascadeDelete: true);
        }
    }
}
