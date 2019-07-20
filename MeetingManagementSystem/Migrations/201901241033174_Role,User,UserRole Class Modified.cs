namespace MeetingManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RoleUserUserRoleClassModified : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.SecRoles", "CreatedBy", c => c.Int(nullable: false));
            AlterColumn("dbo.SecRoles", "ModifiedBy", c => c.Int(nullable: false));
            AlterColumn("dbo.SecUserRoles", "CreatedBy", c => c.Int(nullable: false));
            AlterColumn("dbo.SecUserRoles", "ModifiedBy", c => c.Int(nullable: false));
            AlterColumn("dbo.SecUsers", "CreatedBy", c => c.Int(nullable: false));
            AlterColumn("dbo.SecUsers", "ModifiedBy", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.SecUsers", "ModifiedBy", c => c.String());
            AlterColumn("dbo.SecUsers", "CreatedBy", c => c.String());
            AlterColumn("dbo.SecUserRoles", "ModifiedBy", c => c.String());
            AlterColumn("dbo.SecUserRoles", "CreatedBy", c => c.String());
            AlterColumn("dbo.SecRoles", "ModifiedBy", c => c.String());
            AlterColumn("dbo.SecRoles", "CreatedBy", c => c.String());
        }
    }
}
