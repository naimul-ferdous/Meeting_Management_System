namespace MeetingManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EmployeeTableModified : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Employees", "EmployeeOfficialId", c => c.String(maxLength: 10));
            AddColumn("dbo.Employees", "IsActive", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Employees", "IsActive");
            DropColumn("dbo.Employees", "EmployeeOfficialId");
        }
    }
}
