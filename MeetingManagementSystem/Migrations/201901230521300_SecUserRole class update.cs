namespace MeetingManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SecUserRoleclassupdate : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.SecUserRoles", "SecResourceId", "dbo.SecResources");
            DropIndex("dbo.SecUserRoles", new[] { "SecResourceId" });
            AddColumn("dbo.SecUserRoles", "SecUserId", c => c.Int(nullable: false));
            CreateIndex("dbo.SecUserRoles", "SecUserId");
            AddForeignKey("dbo.SecUserRoles", "SecUserId", "dbo.SecUsers", "SecUserId", cascadeDelete: true);
            DropColumn("dbo.SecUserRoles", "SecResourceId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SecUserRoles", "SecResourceId", c => c.Int(nullable: false));
            DropForeignKey("dbo.SecUserRoles", "SecUserId", "dbo.SecUsers");
            DropIndex("dbo.SecUserRoles", new[] { "SecUserId" });
            DropColumn("dbo.SecUserRoles", "SecUserId");
            CreateIndex("dbo.SecUserRoles", "SecResourceId");
            AddForeignKey("dbo.SecUserRoles", "SecResourceId", "dbo.SecResources", "SecResourceId", cascadeDelete: true);
        }
    }
}
