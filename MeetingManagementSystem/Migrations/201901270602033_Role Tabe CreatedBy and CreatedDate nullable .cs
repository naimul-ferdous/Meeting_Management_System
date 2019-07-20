namespace MeetingManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RoleTabeCreatedByandCreatedDatenullable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.SecRoles", "ModifiedBy", c => c.Int());
            AlterColumn("dbo.SecRoles", "ModifiedDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.SecRoles", "ModifiedDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.SecRoles", "ModifiedBy", c => c.Int(nullable: false));
        }
    }
}
