namespace MeetingManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SecRolePermission_edited : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.SecRolePermissions", "SecResourcePermissionId", "dbo.SecResourcePermissions");
            DropIndex("dbo.SecRolePermissions", new[] { "SecResourcePermissionId" });
            AddColumn("dbo.SecRolePermissions", "SecResourceId", c => c.Int(nullable: false));
            AddColumn("dbo.SecRolePermissions", "SecRoleId", c => c.Int(nullable: false));
            CreateIndex("dbo.SecRolePermissions", "SecResourceId");
            CreateIndex("dbo.SecRolePermissions", "SecRoleId");
            AddForeignKey("dbo.SecRolePermissions", "SecResourceId", "dbo.SecResources", "SecResourceId", cascadeDelete: true);
            AddForeignKey("dbo.SecRolePermissions", "SecRoleId", "dbo.SecRoles", "SecRoleId", cascadeDelete: true);
            DropColumn("dbo.SecRolePermissions", "SecResourcePermissionId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SecRolePermissions", "SecResourcePermissionId", c => c.Int(nullable: false));
            DropForeignKey("dbo.SecRolePermissions", "SecRoleId", "dbo.SecRoles");
            DropForeignKey("dbo.SecRolePermissions", "SecResourceId", "dbo.SecResources");
            DropIndex("dbo.SecRolePermissions", new[] { "SecRoleId" });
            DropIndex("dbo.SecRolePermissions", new[] { "SecResourceId" });
            DropColumn("dbo.SecRolePermissions", "SecRoleId");
            DropColumn("dbo.SecRolePermissions", "SecResourceId");
            CreateIndex("dbo.SecRolePermissions", "SecResourcePermissionId");
            AddForeignKey("dbo.SecRolePermissions", "SecResourcePermissionId", "dbo.SecResourcePermissions", "SecResourcePermissionId", cascadeDelete: true);
        }
    }
}
