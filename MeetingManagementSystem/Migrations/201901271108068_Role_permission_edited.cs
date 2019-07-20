namespace MeetingManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Role_permission_edited : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.SecRolePermissions", "Read");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SecRolePermissions", "Read", c => c.Boolean(nullable: false));
        }
    }
}
