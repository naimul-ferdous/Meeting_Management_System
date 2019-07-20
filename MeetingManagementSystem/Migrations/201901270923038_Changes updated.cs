namespace MeetingManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Changesupdated : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.SecUserRoles", "SecRoleId", "dbo.SecRoles");
            DropForeignKey("dbo.SecUserRoles", "SecUserId", "dbo.SecUsers");
            DropIndex("dbo.SecUserRoles", new[] { "SecRoleId" });
            DropIndex("dbo.SecUserRoles", new[] { "SecUserId" });
            AlterColumn("dbo.SecRoles", "CreatedBy", c => c.String());
            AlterColumn("dbo.SecRoles", "ModifiedBy", c => c.String());
            AlterColumn("dbo.SecRoles", "ModifiedDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.SecUsers", "CreatedBy", c => c.String());
            AlterColumn("dbo.SecUsers", "ModifiedBy", c => c.String());
            AlterColumn("dbo.SecUsers", "ModifiedDate", c => c.DateTime(nullable: false));
            DropTable("dbo.SecUserRoles");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.SecUserRoles",
                c => new
                    {
                        SecUserRoleId = c.Int(nullable: false, identity: true),
                        SecRoleId = c.Int(nullable: false),
                        SecUserId = c.Int(nullable: false),
                        CreatedBy = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedBy = c.Int(),
                        ModifiedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.SecUserRoleId);
            
            AlterColumn("dbo.SecUsers", "ModifiedDate", c => c.DateTime());
            AlterColumn("dbo.SecUsers", "ModifiedBy", c => c.Int());
            AlterColumn("dbo.SecUsers", "CreatedBy", c => c.Int(nullable: false));
            AlterColumn("dbo.SecRoles", "ModifiedDate", c => c.DateTime());
            AlterColumn("dbo.SecRoles", "ModifiedBy", c => c.Int());
            AlterColumn("dbo.SecRoles", "CreatedBy", c => c.Int(nullable: false));
            CreateIndex("dbo.SecUserRoles", "SecUserId");
            CreateIndex("dbo.SecUserRoles", "SecRoleId");
            AddForeignKey("dbo.SecUserRoles", "SecUserId", "dbo.SecUsers", "SecUserId", cascadeDelete: true);
            AddForeignKey("dbo.SecUserRoles", "SecRoleId", "dbo.SecRoles", "SecRoleId", cascadeDelete: true);
        }
    }
}
