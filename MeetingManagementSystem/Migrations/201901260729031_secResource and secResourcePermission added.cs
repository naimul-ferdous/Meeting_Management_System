namespace MeetingManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class secResourceandsecResourcePermissionadded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SecResourcePermissions",
                c => new
                {
                    SecResourcePermissionId = c.Int(nullable: false, identity: true),
                    SecRoleId = c.Int(nullable: false),
                    SecResourceId = c.Int(nullable: false),
                    FileName = c.String(),
                    MenuName = c.String(),
                    DisplayName = c.String(),
                    ModuleId = c.Int(nullable: false),
                    Order = c.Int(nullable: false),
                    Level = c.Int(nullable: false),
                    ActionUrl = c.String(),
                    Status = c.String(),
                    CreatedBy = c.Int(nullable: false),
                    CreationDateTime = c.DateTime(nullable: false),
                    ModifiedBy = c.Int(nullable: false),
                    ModificationDateTime = c.DateTime(nullable: false),
                })
                .PrimaryKey(t => t.SecResourcePermissionId)
                .ForeignKey("dbo.SecResources", t => t.SecResourceId, cascadeDelete: true)
                .ForeignKey("dbo.SecRoles", t => t.SecRoleId, cascadeDelete: true)
                .Index(t => t.SecRoleId)
                .Index(t => t.SecResourceId);

            CreateTable(
                "dbo.SecResources",
                c => new
                    {
                        SecResourceId = c.Int(nullable: false, identity: true),
                        FileName = c.String(),
                        MenuName = c.String(),
                        DisplayName = c.String(),
                        ModuleId = c.Int(nullable: false),
                        Order = c.Int(nullable: false),
                        Level = c.Int(nullable: false),
                        ActionUrl = c.String(),
                        Status = c.String(),
                        CreatedBy = c.Int(nullable: false),
                        CreationDateTime = c.DateTime(nullable: false),
                        ModifiedBy = c.Int(nullable: false),
                        ModificationDateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.SecResourceId);
            
            DropTable("dbo.Resources");
            DropTable("dbo.RoleEmployees");
            DropTable("dbo.RoleResourcePermissions");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.RoleResourcePermissions",
                c => new
                    {
                        RoleResourcePermissionId = c.Int(nullable: false, identity: true),
                        RoleId = c.Int(nullable: false),
                        ResourceId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.RoleResourcePermissionId);
            
            CreateTable(
                "dbo.RoleEmployees",
                c => new
                    {
                        RoleEmployeeId = c.Int(nullable: false, identity: true),
                        RoleId = c.Int(nullable: false),
                        EmployeeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.RoleEmployeeId);
            
            CreateTable(
                "dbo.Resources",
                c => new
                    {
                        ResourceId = c.Int(nullable: false, identity: true),
                        ResourceName = c.String(),
                        ResourcePath = c.String(),
                    })
                .PrimaryKey(t => t.ResourceId);
            
            DropForeignKey("dbo.SecResourcePermissions", "SecRoleId", "dbo.SecRoles");
            DropForeignKey("dbo.SecResourcePermissions", "SecResourceId", "dbo.SecResources");
            DropIndex("dbo.SecResourcePermissions", new[] { "SecResourceId" });
            DropIndex("dbo.SecResourcePermissions", new[] { "SecRoleId" });
            DropTable("dbo.SecResources");
            DropTable("dbo.SecResourcePermissions");
        }
    }
}
