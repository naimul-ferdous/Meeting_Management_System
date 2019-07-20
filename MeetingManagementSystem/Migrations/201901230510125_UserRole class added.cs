namespace MeetingManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class UserRoleclassadded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                    "dbo.SecUserRoles",
                    c => new
                    {
                        SecUserRoleId = c.Int(nullable: false, identity: true),
                        SecRoleId = c.Int(nullable: false),
                        SecResourceId = c.Int(nullable: false),
                        CreatedBy = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedBy = c.String(),
                        ModifiedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.SecUserRoleId)
                .ForeignKey("dbo.SecResources", t => t.SecResourceId, cascadeDelete: true)
                .ForeignKey("dbo.SecRoles", t => t.SecRoleId, cascadeDelete: true)
                .Index(t => t.SecRoleId)
            .Index(t => t.SecResourceId);

        }

        public override void Down()
        {
            DropForeignKey("dbo.SecUserRoles", "SecRoleId", "dbo.SecRoles");
            DropForeignKey("dbo.SecUserRoles", "SecResourceId", "dbo.SecResources");
            DropIndex("dbo.SecUserRoles", new[] { "SecResourceId" });
            DropIndex("dbo.SecUserRoles", new[] { "SecRoleId" });
            DropTable("dbo.SecUserRoles");
        }
    }
}
