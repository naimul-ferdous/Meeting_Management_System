namespace MeetingManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class role_permission_added : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SecRolePermissions",
                c => new
                    {
                        SecRolePermissionId = c.Int(nullable: false, identity: true),
                        SecResourcePermissionId = c.Int(nullable: false),
                        Add = c.Boolean(nullable: false),
                        Edit = c.Boolean(nullable: false),
                        Delete = c.Boolean(nullable: false),
                        Read = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.SecRolePermissionId)
                .ForeignKey("dbo.SecResourcePermissions", t => t.SecResourcePermissionId, cascadeDelete: true)
                .Index(t => t.SecResourcePermissionId);
            
            DropColumn("dbo.SecResources", "CreatedBy");
            DropColumn("dbo.SecResources", "CreationDateTime");
            DropColumn("dbo.SecResources", "ModifiedBy");
            DropColumn("dbo.SecResources", "ModificationDateTime");
            DropColumn("dbo.SecResourcePermissions", "CreatedBy");
            DropColumn("dbo.SecResourcePermissions", "CreationDateTime");
            DropColumn("dbo.SecResourcePermissions", "ModifiedBy");
            DropColumn("dbo.SecResourcePermissions", "ModificationDateTime");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SecResourcePermissions", "ModificationDateTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.SecResourcePermissions", "ModifiedBy", c => c.Int(nullable: false));
            AddColumn("dbo.SecResourcePermissions", "CreationDateTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.SecResourcePermissions", "CreatedBy", c => c.Int(nullable: false));
            AddColumn("dbo.SecResources", "ModificationDateTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.SecResources", "ModifiedBy", c => c.Int(nullable: false));
            AddColumn("dbo.SecResources", "CreationDateTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.SecResources", "CreatedBy", c => c.Int(nullable: false));
            DropForeignKey("dbo.SecRolePermissions", "SecResourcePermissionId", "dbo.SecResourcePermissions");
            DropIndex("dbo.SecRolePermissions", new[] { "SecResourcePermissionId" });
            DropTable("dbo.SecRolePermissions");
        }
    }
}
