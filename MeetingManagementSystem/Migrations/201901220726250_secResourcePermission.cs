namespace MeetingManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class secResourcePermission : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SecRoles",
                c => new
                {
                    SecRoleId = c.Int(nullable: false, identity: true),
                    RoleName = c.String(nullable: false, maxLength: 50),
                    Status = c.Boolean(nullable: false),
                    CreatedBy = c.String(),
                    CreatedDate = c.DateTime(nullable: false),
                    ModifiedBy = c.String(),
                    ModifiedDate = c.DateTime(nullable: false),
                })
                .PrimaryKey(t => t.SecRoleId);

            CreateTable(
                    "dbo.SecUsers",
                    c => new
                    {
                        SecUserId = c.Int(nullable: false, identity: true),
                        LoginName = c.String(nullable: false, maxLength: 50),
                        Password = c.String(nullable: false),
                        Status = c.Boolean(nullable: false),
                        EmailId = c.String(nullable: false, maxLength: 254),
                        CreatedBy = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedBy = c.String(),
                        ModifiedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.SecUserId);

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
            
          
            
           /// DropTable("dbo.RoleEmployees");
//DropTable("dbo.RoleResourcePermissions");
  //          DropTable("dbo.Roles");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Roles",
                c => new
                {
                    RoleId = c.Int(nullable: false, identity: true),
                    RoleName = c.String(),
                    Status = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.RoleId);

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
            
            DropForeignKey("dbo.SecResourcePermissions", "SecRoleId", "dbo.SecRoles");
            DropForeignKey("dbo.SecResourcePermissions", "SecResourceId", "dbo.SecResources");
            DropIndex("dbo.SecResourcePermissions", new[] { "SecResourceId" });
            DropIndex("dbo.SecResourcePermissions", new[] { "SecRoleId" });
            DropTable("dbo.SecUsers");
            DropTable("dbo.SecResourcePermissions");
            DropTable("dbo.SecRoles");
        }
    }
}
