namespace MeetingManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class resource_permission_statustype_Changed : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.SecResourcePermissions", "Status", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.SecResourcePermissions", "Status", c => c.String());
        }
    }
}
