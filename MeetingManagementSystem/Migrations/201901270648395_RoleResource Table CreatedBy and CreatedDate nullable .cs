namespace MeetingManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RoleResourceTableCreatedByandCreatedDatenullable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.SecUserRoles", "ModifiedBy", c => c.Int());
            AlterColumn("dbo.SecUserRoles", "ModifiedDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.SecUserRoles", "ModifiedDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.SecUserRoles", "ModifiedBy", c => c.Int(nullable: false));
        }
    }
}
