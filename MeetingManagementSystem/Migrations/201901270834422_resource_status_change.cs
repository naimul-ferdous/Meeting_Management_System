namespace MeetingManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class resource_status_change : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.SecResources", "Status", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.SecResources", "Status", c => c.String());
        }
    }
}
