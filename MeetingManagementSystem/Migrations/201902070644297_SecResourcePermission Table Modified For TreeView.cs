namespace MeetingManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SecResourcePermissionTableModifiedForTreeView : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SecResourcePermissions", "Parent_SecResourcePermissionId", c => c.Int());
            CreateIndex("dbo.SecResourcePermissions", "Parent_SecResourcePermissionId");
            AddForeignKey("dbo.SecResourcePermissions", "Parent_SecResourcePermissionId", "dbo.SecResourcePermissions", "SecResourcePermissionId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SecResourcePermissions", "Parent_SecResourcePermissionId", "dbo.SecResourcePermissions");
            DropIndex("dbo.SecResourcePermissions", new[] { "Parent_SecResourcePermissionId" });
            DropColumn("dbo.SecResourcePermissions", "Parent_SecResourcePermissionId");
        }
    }
}
