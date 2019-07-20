namespace MeetingManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SecRoleAndSecUserClassadded : DbMigration
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
            
            DropTable("dbo.Roles");
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
            
            DropTable("dbo.SecUsers");
            DropTable("dbo.SecRoles");
        }
    }
}
