namespace MeetingManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Pending_changes : DbMigration
    {
        public override void Up()
        {
            //CreateTable(
            //    "dbo.SecUserRoles",
            //    c => new
            //        {
            //            SecUserRoleId = c.Int(nullable: false, identity: true),
            //            SecRoleId = c.Int(nullable: false),
            //            SecUserId = c.Int(nullable: false),
            //            CreatedBy = c.Int(nullable: false),
            //            CreatedDate = c.DateTime(nullable: false),
            //            ModifiedBy = c.Int(),
            //            ModifiedDate = c.DateTime(),
            //        })
              //  .PrimaryKey(t => t.SecUserRoleId)
               // .ForeignKey("dbo.SecRoles", t => t.SecRoleId, cascadeDelete: true)
               // .ForeignKey("dbo.SecUsers", t => t.SecUserId, cascadeDelete: true)
               // .Index(t => t.SecRoleId)
               // .Index(t => t.SecUserId);
            
            AlterColumn("dbo.SecRoles", "CreatedBy", c => c.Int(nullable: false));
            AlterColumn("dbo.SecRoles", "ModifiedBy", c => c.Int());
            AlterColumn("dbo.SecRoles", "ModifiedDate", c => c.DateTime());
            AlterColumn("dbo.SecUsers", "CreatedBy", c => c.Int(nullable: false));
            AlterColumn("dbo.SecUsers", "ModifiedBy", c => c.Int());
            AlterColumn("dbo.SecUsers", "ModifiedDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SecUserRoles", "SecUserId", "dbo.SecUsers");
            DropForeignKey("dbo.SecUserRoles", "SecRoleId", "dbo.SecRoles");
            DropIndex("dbo.SecUserRoles", new[] { "SecUserId" });
            DropIndex("dbo.SecUserRoles", new[] { "SecRoleId" });
            AlterColumn("dbo.SecUsers", "ModifiedDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.SecUsers", "ModifiedBy", c => c.String());
            AlterColumn("dbo.SecUsers", "CreatedBy", c => c.String());
            AlterColumn("dbo.SecRoles", "ModifiedDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.SecRoles", "ModifiedBy", c => c.String());
            AlterColumn("dbo.SecRoles", "CreatedBy", c => c.String());
            DropTable("dbo.SecUserRoles");
        }
    }
}
